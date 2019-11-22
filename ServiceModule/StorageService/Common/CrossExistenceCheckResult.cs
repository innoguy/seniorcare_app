namespace ServiceModule.StorageService.Common
{
    public enum CrossExistenceCheckResult
    {
        /// <summary>
        /// No file system entity was found at the given path.
        /// </summary>
        NotFound = 0,

        /// <summary>
        /// A file was found at the given path.
        /// </summary>
        FileExists = 1,

        /// <summary>
        /// A folder was found at the given path.
        /// </summary>
        FolderExists = 2
    }
}
