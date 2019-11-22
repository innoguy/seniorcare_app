using Autofac;
using ServiceModule.Notifications;
using ServiceModule.Settings;
using ServiceModule.StorageService;
using ServiceModule.Thresholds.DataAccess;
using ServiceModule.Thresholds.DataService;

namespace Niko.IoC.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new SettingsService()).As<ISettingsService>().AutoActivate().SingleInstance();
            builder.Register(c => new CrossStorageService()).As<ICrossStorageService>().AutoActivate().SingleInstance();
            builder.Register(c => new NotificationsDataservice(c.Resolve<ISettingsService>())).As<INotificationsDataservice>().AutoActivate().SingleInstance();
            builder.Register(c => new ThresholdsDAL(c.Resolve<ISettingsService>(), c.Resolve<ICrossStorageService>())).As<IThresholdsDAL>().AutoActivate().SingleInstance();
            builder.Register(c => new ThresholdsDataService(c.Resolve<IThresholdsDAL>(), c.Resolve<ICrossStorageService>())).As<IThresholdsDataService>().AutoActivate().SingleInstance();
        }
    }
}
