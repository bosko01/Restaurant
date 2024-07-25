using Common.Infrastructure.File;

namespace Application.Interfaces
{
    public interface IFileManager
    {
        public Task SaveFileToFolderLocation(string fileLocation, FileForFileManager file);

        public Task<bool> DeleteEverythingFromFolder(string fileLocation);
    }
}