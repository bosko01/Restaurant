using Application.Interfaces;
using Common.Infrastructure.File;

namespace Infrastructure.Services.FileManager
{
    public class FileManager : IFileManager
    {
        public async Task SaveFileToFolderLocation(string fileLocation, FileForFileManager file)
        {
            if (file == null || file.Content == null || string.IsNullOrEmpty(file.FileName))
            {
                throw new ArgumentException("Invalid file data");
            }

            var directory = Path.GetDirectoryName(fileLocation);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory!);
            }

            using (var stream = new FileStream(fileLocation, FileMode.Create))
            {
                await stream.WriteAsync(file.Content, 0, file.Content.Length);
            }
        }

        public async Task<bool> DeleteEverythingFromFolder(string fileLocation)
        {
            var files = Directory.GetFiles(fileLocation);

            if (files.Length <= 0)
            {
                return true;
            }
            else
            {
                foreach (var file in files)
                {
                    File.Delete(file);
                }

                files = Directory.GetFiles(fileLocation);

                if (files.Length <= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}