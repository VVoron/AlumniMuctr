using AlumniMuctr.Data;
using AlumniMuctr.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;

namespace AlumniMuctr.Controllers
{
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _appEnvironment;

        List<string> _colomnsName = new List<string>() { "Id", "Заголовок", "Фото", "Краткое описание", "Полное описание", "Дата создания", "Категория" };
        string[,] _tableInfo;

        public NewsController(ApplicationDbContext db, IWebHostEnvironment appEnvironment)
        {
            _db = db;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<News> dbList = _db.News;
            return View(dbList);
        }

        public ActionResult ExportData()
        {
            IEnumerable<News> dbList = _db.News;

            List<Categories> dbCategories = _db.Categories.ToList();
            _tableInfo = new string[dbList.Count(), _colomnsName.Count()];

            int index = 0;
            foreach (News obj in dbList)
            {
                string[] row = obj.GetInfoForTable();
                for (int i = 0; i < row.Length - 1; i++)
                    _tableInfo[index, i] = row[i];
                _tableInfo[index, row.Length - 1] = dbCategories.ElementAt(Convert.ToInt32(row[row.Length - 1]) - 1).Name;
                index++;
            }

            using (XLWorkbook workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Новости");

                index = 1;
                for (int i = 0; i < _colomnsName.Count(); i++)
                    worksheet.Cell(index, i + 1).Value = _colomnsName[i];
                index++;

                for (int i = 0; i < _tableInfo.GetLength(0); i++)
                {
                    for (int j = 0; j < _tableInfo.GetLength(1); j++)
                        worksheet.Cell(index, j + 1).Value = _tableInfo[i, j];
                    index++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"Новости_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);


            var entity = new News(request);

            entity.Photo = String.Empty;
            
            if (request.Photo != null)
            {
                var newDirName = Guid.NewGuid();

                string path = _appEnvironment.WebRootPath + "/media/NewsMedia/" + newDirName;

                Directory.CreateDirectory(path);

                using (var fileStream = new FileStream(path + "/" + request.Photo.FileName, FileMode.Create))
                {
                    await request.Photo.CopyToAsync(fileStream);
                }

                entity.Photo = "/media/NewsMedia/" + newDirName + "/" + request.Photo.FileName;
            } else if (request.PhotoUrl != null)
            {
                entity.Photo = request.PhotoUrl;
            }

            _db.News.Add(entity);
            _db.SaveChanges();
            TempData["success"] = "Новость успешно создана";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var objFromDb = _db.News.Find(id);
            if (objFromDb == null)
            {
                return NotFound();
            }

            var entity = new NewsRequest(objFromDb);


            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NewsRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);



            var entity = _db.News.Find(request.Id);

            entity.Title = request.Title;
            entity.BriefDescription = request.BriefDescription;
            entity.Description = request.Description;

            if (request.Photo != null)
            {
                var newDirName = Guid.NewGuid();

                string path = _appEnvironment.WebRootPath + "/media/NewsMedia/" + newDirName;

                Directory.CreateDirectory(path);

                using (var fileStream = new FileStream(path + "/" + request.Photo.FileName, FileMode.Create))
                {
                    await request.Photo.CopyToAsync(fileStream);
                }

                entity.Photo = "/media/NewsMedia/" + newDirName + "/" + request.Photo.FileName;
            }
            else if (request.PhotoUrl != null)
            {
                entity.Photo = request.PhotoUrl;
            }

            _db.SaveChanges();
            TempData["success"] = "Новость успешно изменена";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            var obj = _db.News.Find(id);

            if (obj == null)
            {
                NotFound();
            }

            _db.News.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Новость успешно удалена";

            return RedirectToAction("Index");
        }

        public IActionResult GetAllNews()
        {
            return View();
        }
    }
}
