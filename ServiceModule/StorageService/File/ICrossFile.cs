using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ServiceModule.StorageService.Common;

namespace ServiceModule.StorageService.File
{
    /// <summary>
    /// Represents a file.
    /// </summary>
    public interface ICrossFile
    {
        /// <summary>
        /// The name of the file.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The "full path" of the file.
        /// </summary>
        string Path { get; }

        /// <summary>
        /// The extension of the file.
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// Opens the file.
        /// </summary>
        /// <param name="fileAccess">Specifies whether the file should be opened in read-only or read/write mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A System.IO.FileStream which can be used to read from or write to the file.</returns>
        Task<FileStream> OpenAsync(FileAccess fileAccess = FileAccess.Read, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Reads all text from the file.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A System.String of the file content.</returns>
        Task<string> ReadTextAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Writes the given text content into the file.
        /// </summary>
        /// <param name="content">The text that will be written into the file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task which will complete after the content is written into the file.</returns>
        Task WriteTextAsync(string content, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task which will complete after the file is deleted.</returns>
        Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Moves a file.
        /// </summary>
        /// <param name="newFilePath">The new full path of the file.</param>
        /// <param name="collisionOption">How to deal with collisions with existing files.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task which will complete after the file is moved.</returns>
        Task MoveAsync(string newFilePath, CollisionOption collisionOption = CollisionOption.ReplaceExisting, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Renames a file without changing its location.
        /// </summary>
        /// <param name="newFileName">The new name of the file.</param>
        /// <param name="collisionOption">How to deal with collisions with existing files.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task which will complete after the file is renamed.</returns>
        Task RenameAsync(string newFileName, CollisionOption collisionOption = CollisionOption.FailIfExists, CancellationToken cancellationToken = default(CancellationToken));
    }
}
