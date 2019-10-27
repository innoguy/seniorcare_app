using Autofac;
using ServiceModule.Notifications;
using ServiceModule.Settings;
using ServiceModule.Thresholds;

namespace Niko.IoC.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new SettingsService()).As<ISettingsService>().AutoActivate().SingleInstance();
            builder.Register(c => new NotificationsDataservice(c.Resolve<ISettingsService>())).As<INotificationsDataservice>().AutoActivate().SingleInstance();
            builder.Register(c => new ThresholdsDataservice(c.Resolve<ISettingsService>())).As<IThresholdsDataservice>().AutoActivate().SingleInstance();
            
        }
    }
}
