using XF.Infrastructure.Core.Droid.Renderers;

namespace SeniorCare.Droid
{
    public static class Bootstrap
    {
        public static void Init()
        {
            var x = typeof(XF.Infrastructure.Core.PageLocator);
            x = typeof(XF.Infrastructure.Core.NavigationService);
            x = typeof(ExtLabelRenderer);
            x = typeof(ExtEntryRenderer);
        }
    }
}