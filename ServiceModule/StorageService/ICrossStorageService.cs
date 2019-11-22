using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ServiceModule.StorageService.File;
using ServiceModule.StorageService.Folder;

namespace ServiceModule.StorageService
{
    public interface ICrossStorageService
    {
        Task<IList<ICrossFile>> GetFilesAsync(ICrossFolder parentFolder = null);
        Task<ICrossFile> GetFileAsync(string fileName, string fileExtension = "txt", ICrossFolder parentFolder = null);
        Task<bool> FileExistAsync(string fileName, string fileExtension, ICrossFolder parentFolder = null);
        Task<string> ReadAllTextAsync(string fileName, ICrossFolder parentFolder = null);
        Task<FileStream> OpenFileAsync(string filename, string fileExtension, ICrossFolder parentFolder = null);
        Task<ICrossFile> WriteTextAsync(string filename, string content = "", ICrossFolder parentFolder = null);
        Task DeleteFileAsync(string fileName, string fileExtension, ICrossFolder parentFolder = null);

        Task<IList<ICrossFolder>> GetFoldersAsync(ICrossFolder parentFolder = null);
        Task<ICrossFolder> GetFolderAsync(string folderName, ICrossFolder parentFolder = null);
        Task<bool> FolderExistAsync(string folderName, ICrossFolder parentFolder = null);
        Task<ICrossFolder> CreateFolderAsync(string folderName, ICrossFolder parentFolder = null);
        Task DeleteFolderAsync(string folderName, ICrossFolder parentFolder = null);
    }
}
