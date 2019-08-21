
using System;
using System.IO;
using XF.Infrastructure.Core.Droid.Common;

[assembly: Xamarin.Forms.Dependency(typeof(CommonFilesLocation))]
namespace XF.Infrastructure.Core.Droid.Common
{
    public class CommonFilesLocation : ICommonFilesLocation
    {

        public string SQLiteDbLocation
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            }
        }

        public string LogFilesLocation
        {
            get
            {
                var folderPath= Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).ToString(), "NIKO_LOGS");
                return Path.Combine(folderPath, "NIKO_LOG -{Date}.json");
            }
        }

       
    }
}