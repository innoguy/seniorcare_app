using Foundation;
using System;
using System.IO;
using XF.Infrastructure.Core.iOS.Common;

[assembly: Xamarin.Forms.Dependency(typeof(CommonFilesLocation))]
namespace XF.Infrastructure.Core.iOS.Common
{
    public class CommonFilesLocation : ICommonFilesLocation
    {
        public string SQLiteDbLocation
        {
            get
            {
                string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                return Path.Combine(docFolder,"Library");
            }
        }

        public string LogFilesLocation
        {
            get
            {
                string[] folders = NSSearchPath.GetDirectories(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User);
                var folderPath = Path.Combine(folders[0], "NIKO_LOGS");
                return Path.Combine(folderPath, "NIKO_LOG -{Date}.json");
            }
        }
    }
}