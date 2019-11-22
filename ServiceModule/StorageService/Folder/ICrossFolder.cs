using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ServiceModule.StorageService.Common;
using ServiceModule.StorageService.File;

namespace ServiceModule.StorageService.Folder
{
    public interface ICrossFolder
    {
        /// <summary>
        /// The name of the folder.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The "full path" of the folder.
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Checks whether a folder or file exists at the given location.
        /// </summary>
        /// <param name="name">The name of the file or folder to check for.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task whose result is the result of the existence check.</returns>
        Task<CrossExistenceCheckResult> CheckExistsAsync(string name, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Creates a file in this folder
        /// </summary>
        /// <param name="fileName">The name of the file to create</param>
        /// <param name="fileExtension">The extension of the file to create</param>
        /// <param name="dataBytes">The content of the file to create</param>
        /// <param name="collisionOption">Specifies how to behave if the specified file already exists</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created file.</returns>
        Task<ICrossFile> CreateFileAsync(string fileName, string fileExtension, byte[] dataBytes, CollisionOption collisionOption = CollisionOption.FailIfExists, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Creates a text file in this folder.
        /// </summary>
        /// <param name="fileName">The name of the file to create.</param>
        /// <param name="content">The text content of the file to create.</param>
        /// <param name="collisionOption">Specifies how to behave if the specified file already exists.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created text file.</returns>
        Task<ICrossFile> WriteTextFileAsync(string fileName, string content, CollisionOption collisionOption = CollisionOption.ReplaceExisting, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Creates a subfolder in this folder.
        /// </summary>
        /// <param name="folderName">The name of the folder to create.</param>
        /// <param name="collisionOption">Specifies how to behave if the specified folder already exists.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created folder.</returns>
        Task<ICrossFolder> CreateFolderAsync(string folderName, CollisionOption collisionOption = CollisionOption.FailIfExists, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes this folder and all of its contents.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task which will complete after the folder is deleted.</returns>
        Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets a file in this folder.
        /// </summary>
        /// <param name="fileName">The name of the file to get.</param>
        /// <param name="fileExtension">The name of the file extension to get.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The requested file, or null if it does not exist.</returns>
        Task<ICrossFile> GetFileAsync(string fileName, string fileExtension, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets a list of the files in this folder.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of the files in the folder.</returns>
        Task<IList<ICrossFile>> GetFilesAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets a subfolder in this folder.
        /// </summary>
        /// <param name="folderName">The name of the folder to get.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The requested folder, or null if it does not exist.</returns>
        Task<ICrossFolder> GetFolderAsync(string folderName, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets a list of subfolders in this folder.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of subfolders in the folder.</returns>
        Task<IList<ICrossFolder>> GetFoldersAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
