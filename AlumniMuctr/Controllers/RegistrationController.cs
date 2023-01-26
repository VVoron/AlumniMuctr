using AlumniMuctr.Data;
using AlumniMuctr.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;

namespace AlumniMuctr.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ApplicationDbContext _db;
        List<string> _colomnsName = new List<string>() {
                "Id",
                "ФИО",
                "ФИО в период обучения ",
                "Пол",
                "Дата рождения",
                "Факультет/кафедра",
                "Научный руководитель",
                "Год окончания университета",
                "Место проживания в настоящее время",
                "Место работы в настоящее время",
                "Занимаемая должность",
                "Значимые научные/профессиональные достижения",
                "Есть ли в Вашей семье выпускники РХТУ - МХТИ?",
                "Хобби, увлечения",
                "Загрузите Ваше выпускное фото или актуальное фото (при желании)",
                "Адресс электронной почты",
                "Контактный телефон",
                "Подписаться на рассылку новостной информации",
                "Хочу активно участвовать в жизни ассоциации",
                "Хочу выступить на 'Нескучной субботе'",
                "Согласие на обработку персональных данных"
        };
        string[,] _tableInfo;

        public RegistrationController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<RegistrationForm> dbList = _db.RegistrationForm;
            return View(dbList);
        }

        public ActionResult ExportData()
        {
            IEnumerable<RegistrationForm> regForm = _db.RegistrationForm;
            _tableInfo = new string[regForm.Count(), _colomnsName.Count()];

            int index = 0;
            foreach (RegistrationForm obj in regForm)
            {
                string[] row = obj.GetInfoForTable();
                for (int i = 0; i < row.Length; i++)
                    _tableInfo[index, i] = row[i];
                index++;
            }

            using (XLWorkbook workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Анкеты");

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
                        FileDownloadName = $"Анкеты_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RegistrationForm obj)
        {
            if (ModelState.IsValid)
            {
                _db.RegistrationForm.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Заявка успешно создана";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var objFromDb = _db.RegistrationForm.Find(id);
            if (objFromDb == null)
            {
                NotFound();
            }
            return View(objFromDb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RegistrationForm obj)
        {
            if (ModelState.IsValid)
            {
                _db.RegistrationForm.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Заявка успешно изменена";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            var obj = _db.RegistrationForm.Find(id);
            if (obj == null)
            {
                NotFound();
            }
            _db.RegistrationForm.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Заявка успешно удалена";
            return RedirectToAction("Index");
        }
    }
}
