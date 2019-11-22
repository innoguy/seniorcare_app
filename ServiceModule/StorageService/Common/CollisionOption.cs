namespace ServiceModule.StorageService.Common
{
    public enum CollisionOption
    {
        /// <summary>
        /// Return an error if another file or folder exists with the same name and abort the operation.
        /// </summary>
        FailIfExists = 0,

        /// <summary>
        /// Replace the existing file or folder. Your app must have permission to access the location that contains the existing file or folder.
        /// </summary>
        ReplaceExisting = 1,

        /// <summary>
        /// Automatically generate a unique name by appending a number to the name of the file or folder.
        /// </summary>
        GenerateUniqueName = 2
    }
}
