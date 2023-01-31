namespace AlumniMuctr.Services.SaveFileService
{
    public class SaveFileService : ISaveFileService
    {
        public async Task<string> SaveFile(string environmentPath, string filePath, IFormFile file)
        {
            var newDirName = Guid.NewGuid();

            string path = environmentPath + filePath + newDirName;

            Directory.CreateDirectory(path);

            using (var fileStream = new FileStream(path + "/" + file.FileName, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return filePath + "/" + newDirName + "/" + file.FileName;
        }
    }
}
