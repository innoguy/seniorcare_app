using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServiceModule.StorageService.Common;
using ServiceModule.StorageService.File;

namespace ServiceModule.StorageService.Folder
{
    public class CrossFolder : ICrossFolder
    {
        public string Name { get; }
        public string Path { get; }

        public CrossFolder(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public async Task<CrossExistenceCheckResult> CheckExistsAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Task.Run(() =>
            {
                if (Directory.Exists(System.IO.Path.Combine(Path, name)))
                    return CrossExistenceCheckResult.FolderExists;
                if (System.IO.File.Exists(System.IO.Path.Combine(Path, name)))
                    return CrossExistenceCheckResult.FileExists;
                return CrossExistenceCheckResult.NotFound;

            }, cancellationToken);
        }

        public async Task<ICrossFile> CreateFileAsync(string fileName, string fileExtension, byte[] dataBytes, CollisionOption collisionOption = CollisionOption.FailIfExists, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Task.Run(() =>
            {
                var newFilePath = System.IO.Path.Combine(Path, $"{fileName}.{fileExtension}");
                var exists = System.IO.File.Exists(newFilePath);

                if (exists && collisionOption == CollisionOption.FailIfExists)
                    return null;
                if (exists && collisionOption == CollisionOption.ReplaceExisting)
                    System.IO.File.Delete(newFilePath);

                System.IO.File.WriteAllBytes(newFilePath, dataBytes);

                return new CrossFile(fileName, fileExtension, newFilePath);
            }, cancellationToken);

        }

        public async Task<ICrossFile> WriteTextFileAsync(string fileName, string content, CollisionOption collisionOption = CollisionOption.ReplaceExisting, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Task.Run(() =>
            {
                var fileExtension = "txt";
                var newFilePath = System.IO.Path.Combine(Path, $"{fileName}.{fileExtension}");
                var exists = System.IO.File.Exists(newFilePath);

                if (exists && collisionOption == CollisionOption.FailIfExists)
                    return null;
                if (exists && collisionOption == CollisionOption.ReplaceExisting)
                    System.IO.File.Delete(newFilePath);

                System.IO.File.WriteAllText(newFilePath, content, Encoding.UTF8);

                return new CrossFile(fileName, fileExtension, newFilePath);
            }, cancellationToken);

        }

        public async Task<ICrossFolder> CreateFolderAsync(string folderName, CollisionOption collisionOption = CollisionOption.FailIfExists, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Task.Run(() =>
            {
                var newFolderPath = System.IO.Path.Combine(Path, folderName);
                var exists = Directory.Exists(newFolderPath);

                if (exists && collisionOption == CollisionOption.FailIfExists)
                    return null;
                if (exists && collisionOption == CollisionOption.ReplaceExisting)
                    Directory.Delete(newFolderPath, false);

                Directory.CreateDirectory(newFolderPath);

                return new CrossFolder(folderName, newFolderPath);
            }, cancellationToken);
        }

        public async Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.Run(() => { Directory.Delete(Path, true); }, cancellationToken);
        }

        public async Task<ICrossFolder> GetFolderAsync(string folderName, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Task.Run(() =>
            {
                var folderPath = System.IO.Path.Combine(Path, folderName);

                if (Directory.Exists(folderPath))
                    return new CrossFolder(folderName, folderPath);

                return null;

            }, cancellationToken);

        }

        public async Task<IList<ICrossFolder>> GetFoldersAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Task.Run(() =>
            {
                var directoryList = new List<ICrossFolder>();
                var directories = Directory.GetDirectories(Path, "*", SearchOption.TopDirectoryOnly);
                foreach (var directory in directories)
                {
                    var folderName = System.IO.Path.GetFileName(directory);
                    var folderPath = directory;
                    directoryList.Add(new CrossFolder(folderName, folderPath));
                }

                return directoryList;

            }, cancellationToken);


        }

        public async Task<ICrossFile> GetFileAsync(string fileName, string fileExtension, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                var filePath = System.IO.Path.Combine(Path, $"{fileName}.{fileExtension}");

                if (System.IO.File.Exists(filePath))
                    return new CrossFile(fileName, fileExtension, filePath);

                return null;

            }, cancellationToken);
        }

        public async Task<IList<ICrossFile>> GetFilesAsync(CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                var fileList = new List<ICrossFile>();
                var files = Directory.GetFiles(Path, "*", SearchOption.TopDirectoryOnly);
                foreach (var file in files)
                {
                    var fileName = System.IO.Path.GetFileName(file);
                    string fileExtension = System.IO.Path.GetExtension(file);
                    var filePath = file;
                    fileList.Add(new CrossFile(fileName, fileExtension, filePath));
                }

                return fileList;

            }, cancellationToken);
        }
    }
}
