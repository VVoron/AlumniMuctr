using AlumniMuctr.Data;
using AlumniMuctr.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlumniMuctr.Controllers
{
    [Authorize]
    public class HelperController : Controller
    {
        private readonly ApplicationDbContext _db;

        List<string> _colomnsName = new List<string>() { "Id", "Дата обращения", "Email", "Имя", "Содержание" };
        string[,] _tableInfo;
        public HelperController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Helper> helpers = _db.Helper.ToList();
            return View(helpers);
        }

        public ActionResult ExportData()
        {
            IEnumerable<Helper> dbList = _db.Helper;

            _tableInfo = new string[dbList.Count(), _colomnsName.Count()];

            int index = 0;
            foreach (Helper obj in dbList)
            {
                string[] row = obj.GetInfoForTable();
                for (int i = 0; i < row.Length; i++)
                    _tableInfo[index, i] = row[i];
            }

            using (XLWorkbook workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Поддержка");

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
                        FileDownloadName = $"Поддержка_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }

        public IActionResult WatchMore(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var objFromDb = _db.Helper.Find(id);
            if (objFromDb == null)
            {
                return NotFound();
            }

            return View(objFromDb);
        }

        public IActionResult Delete(int? id)
        {
            var obj = _db.Helper.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Helper.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Вопрос успешно удален";

            return RedirectToAction("Index");
        }
    }
}
