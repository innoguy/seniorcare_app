using Acr.UserDialogs;
using Autofac;
using XF.Infrastructure.Core;

namespace Niko.IoC.Modules
{
    public class XFCoreServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.Register(c => UserDialogs.Instance).As<IUserDialogs>().AutoActivate().SingleInstance();
            builder.Register(c => new PageLocator()).As<IPageLocator>().AutoActivate().SingleInstance();
            builder.Register(c => new NavigationService()).As<INavigationService>().AutoActivate().SingleInstance();
        }
    }
}
