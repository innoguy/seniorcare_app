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
            builder.Register(c => new NotificationsDataservice()).As<INotificationsDataservice>().AutoActivate().SingleInstance();
            builder.Register(c => new ThresholdsDataservice()).As<IThresholdsDataservice>().AutoActivate().SingleInstance();
            builder.Register(c => new SettingsService()).As<ISettingsService>().AutoActivate().SingleInstance();
        }
    }
}
