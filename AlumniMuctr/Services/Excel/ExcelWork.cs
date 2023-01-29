using AlumniMuctr.Data;
using AlumniMuctr.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;

namespace AlumniMuctr.Services.Excel
{
    public class ExcelWork
    {
        private List<string> colomnsNews = new List<string>() { "Id", "Заголовок", "Фото", "Краткое описание", "Полное описание", "Дата создания", "Категория" };
        private List<string> colomnsHelper = new List<string>() { "Id", "Дата обращения", "Email", "Имя", "Содержание" };
        private List<string> colomnsRegForm = new List<string>() {
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
        private string[,] tableInfo;
        public FileContentResult ExportData(string tableName, ApplicationDbContext db) {

            List<string> currentColomns;
            string name;

            int index = 0;

            switch (tableName)
            {
                case "News":
                    {
                        currentColomns = colomnsNews;
                        name = "Новости";

                        IEnumerable<News> dbList = db.News;
                        List<Categories> dbCategories = db.Categories.ToList();
                        tableInfo = new string[dbList.Count(), currentColomns.Count()];
                        foreach (News obj in dbList)
                        {
                            string[] row = obj.GetInfoForTable();
                            for (int i = 0; i < row.Length - 1; i++)
                                tableInfo[index, i] = row[i];
                            tableInfo[index, row.Length - 1] = dbCategories.ElementAt(Convert.ToInt32(row[row.Length - 1]) - 1).Name;
                            index++;
                        }
                        break;
                    }
                case "Reg":
                    {
                        currentColomns = colomnsRegForm;
                        name = "Анкеты";

                        IEnumerable<RegistrationForm> regForm = db.RegistrationForm;
                        tableInfo = new string[regForm.Count(), currentColomns.Count()];
                        foreach (RegistrationForm obj in regForm)
                        {
                            string[] row = obj.GetInfoForTable();
                            for (int i = 0; i < row.Length; i++)
                                tableInfo[index, i] = row[i];
                            index++;
                        }
                        break;
                    }
                case "Supp":
                    {
                        currentColomns = colomnsHelper;
                        name = "Поддержка";

                        IEnumerable<Helper> dbList = db.Helper;
                        tableInfo = new string[dbList.Count(), currentColomns.Count()];
                        foreach (Helper obj in dbList)
                        {
                            string[] row = obj.GetInfoForTable();
                            for (int i = 0; i < row.Length; i++)
                                tableInfo[index, i] = row[i];
                            index++;
                        }
                        break;
                    }
                default:
                    {
                        return null;
                    }
            }

            return createExcelFile(name, currentColomns, tableInfo);

        }

        private FileContentResult createExcelFile (string name, List<string> colomnsName, string[,] tableInfo)
        {
            using (XLWorkbook workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(name);

                int index = 1;
                for (int i = 0; i < colomnsName.Count(); i++)
                    worksheet.Cell(index, i + 1).Value = colomnsName[i];
                index++;

                for (int i = 0; i < tableInfo.GetLength(0); i++)
                {
                    for (int j = 0; j < tableInfo.GetLength(1); j++)
                        worksheet.Cell(index, j + 1).Value = tableInfo[i, j];
                    index++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"{name}_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }
    }
}
