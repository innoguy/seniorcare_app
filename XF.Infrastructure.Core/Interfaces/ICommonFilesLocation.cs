namespace XF.Infrastructure.Core
{
    public interface ICommonFilesLocation
    {
        string SQLiteDbLocation { get; }
        string LogFilesLocation { get; }       
    }
}
