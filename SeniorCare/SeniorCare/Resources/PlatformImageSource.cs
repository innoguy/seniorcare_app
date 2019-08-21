using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SeniorCare.Resources
{
    public class PlatformImageSource
    {
        private static Dictionary<string, ImageSource> _cache = new Dictionary<string, ImageSource>();
        public static ImageSource FromPlatformFile(string file)
        {
            if (!_cache.ContainsKey(file))
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                    case Device.macOS:
                        _cache[file] = ImageSource.FromFile(file);
                        break;
                    case Device.Android:
                        _cache[file] = ImageSource.FromFile(String.Format("Images/{0}", file));
                        break;
                    case Device.UWP:
                        _cache[file] = ImageSource.FromFile(String.Format("Assets/{0}", file));
                        break;
                }

            }
            return _cache[file];
        }
    }
}
