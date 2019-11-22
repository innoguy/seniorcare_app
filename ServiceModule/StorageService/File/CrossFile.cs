using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServiceModule.StorageService.Common;

namespace ServiceModule.StorageService.File
{
    public class CrossFile : ICrossFile
    {
        public string Name { get; }
        public string Extension { get; }
        public string Path { get; private set; }

        public CrossFile(string name, string extension, string path)
        {
            Name = name;
            Extension = extension;
            Path = path;
        }

        public async Task<FileStream> OpenAsync(FileAccess fileAccess = FileAccess.Read, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Task.Run(() =>
            {
                var content = System.IO.File.Open(Path, FileMode.Open, fileAccess);
                return content;
            }, cancellationToken);
        }


        public async Task<string> ReadTextAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Task.Run(() =>
            {
                var content = System.IO.File.ReadAllText(Path, Encoding.UTF8);
                return content;
            }, cancellationToken);
        }

        public async Task WriteTextAsync(string content, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.Run(() => { System.IO.File.WriteAllText(Path, content, Encoding.UTF8); }, cancellationToken);
        }

        public async Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.Run(() => { System.IO.File.Delete(Path); }, cancellationToken);
        }

        public async Task MoveAsync(string newFilePath, CollisionOption collisionOption = CollisionOption.ReplaceExisting, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.Run(() => { System.IO.File.Move(Path, newFilePath); }, cancellationToken);
        }

        public async Task RenameAsync(string newFileName, CollisionOption collisionOption = CollisionOption.FailIfExists, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.Run(() =>
            {
                var currentFileName = System.IO.Path.GetFileName(Path);
                var newFilePath = Path.Replace(currentFileName, newFileName);

                System.IO.File.Move(Path, newFilePath);
                Path = newFilePath;
            }, cancellationToken);
        }

    }
}
