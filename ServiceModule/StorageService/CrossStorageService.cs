using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ServiceModule.StorageService.Common;
using ServiceModule.StorageService.File;
using ServiceModule.StorageService.Folder;

namespace ServiceModule.StorageService
{
    public class CrossStorageService : ICrossStorageService
    {
        private static ICrossFolder _appLocalStorage;
        private static string _defaultFileExtension;

        public CrossStorageService()
        {
            _appLocalStorage = new CrossFolder("AppLocalStorage", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            _defaultFileExtension = "txt";
        }

        #region Files

        public async Task<IList<ICrossFile>> GetFilesAsync(ICrossFolder parentFolder = null)
        {
            ICrossFolder folder = parentFolder ?? _appLocalStorage;
            var files = await folder.GetFilesAsync();
            return files;
        }

        public async Task<ICrossFile> GetFileAsync(string fileName, string fileExtension = "txt", ICrossFolder parentFolder = null)
        {
            ICrossFolder folder = parentFolder ?? _appLocalStorage;
            var file = await folder.GetFileAsync(fileName, fileExtension);
            return file;
        }

        public async Task<bool> FileExistAsync(string fileName, string fileExtension, ICrossFolder parentFolder = null)
        {
            ICrossFolder folder = parentFolder ?? _appLocalStorage;
            CrossExistenceCheckResult fileExist = await folder.CheckExistsAsync($"{fileName}.{fileExtension}");
            return fileExist == CrossExistenceCheckResult.FileExists;
        }

        public async Task<string> ReadAllTextAsync(string fileName, ICrossFolder parentFolder = null)
        {
            string content = "";
            ICrossFolder folder = parentFolder ?? _appLocalStorage;
            ICrossFile file = await folder.GetFileAsync(fileName, _defaultFileExtension);
            if (file != null)
                content = await file.ReadTextAsync();

            return content;
        }

        public async Task<FileStream> OpenFileAsync(string filename, string fileExtension, ICrossFolder parentFolder = null)
        {
            ICrossFolder folder = parentFolder ?? _appLocalStorage;
            ICrossFile file = await folder.GetFileAsync(filename, fileExtension);
            var fileStream = await file.OpenAsync();
            return fileStream;
        }

        public async Task<ICrossFile> WriteTextAsync(string filename, string content = "", ICrossFolder parentFolder = null)
        {
            ICrossFolder folder = parentFolder ?? _appLocalStorage;
            ICrossFile file = await folder.WriteTextFileAsync(filename, content);
            return file;
        }

        public async Task DeleteFileAsync(string fileName, string fileExtension, ICrossFolder parentFolder = null)
        {
            ICrossFolder folder = parentFolder ?? _appLocalStorage;
            ICrossFile file = await folder.GetFileAsync(fileName, fileExtension);
            await file.DeleteAsync();
        }

        #endregion

        #region Folders
        public async Task<IList<ICrossFolder>> GetFoldersAsync(ICrossFolder parentFolder = null)
        {
            ICrossFolder folder = parentFolder ?? _appLocalStorage;
            var folders = await folder.GetFoldersAsync();
            return folders;
        }

        public async Task<ICrossFolder> GetFolderAsync(string folderName, ICrossFolder parentFolder = null)
        {
            ICrossFolder folder = parentFolder ?? _appLocalStorage;

            var retrievedFolder = await folder.GetFolderAsync(folderName);
            return retrievedFolder;
        }

        public async Task<bool> FolderExistAsync(string folderName, ICrossFolder parentFolder = null)
        {
            ICrossFolder folder = parentFolder ?? _appLocalStorage;
            var folderexist = await folder.CheckExistsAsync(folderName);
            return folderexist == CrossExistenceCheckResult.FolderExists;
        }

        public async Task<ICrossFolder> CreateFolderAsync(string folderName, ICrossFolder parentFolder = null)
        {
            ICrossFolder folder = parentFolder ?? _appLocalStorage;
            var createdFolder = await folder.CreateFolderAsync(folderName, CollisionOption.ReplaceExisting);
            return createdFolder;
        }

        public async Task DeleteFolderAsync(string folderName, ICrossFolder parentFolder = null)
        {
            ICrossFolder folder = parentFolder ?? _appLocalStorage;
            var requestedFolder = await folder.GetFolderAsync(folderName);
            if (requestedFolder != null)
                await requestedFolder.DeleteAsync();
        }

        #endregion

    }
}
