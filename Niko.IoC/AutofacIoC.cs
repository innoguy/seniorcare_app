using Autofac;
using Niko.IoC.Modules;

namespace Niko.IoC
{
    public static class AutofacIoC
    {
        private static readonly IContainer _container;

        static AutofacIoC()
        {
            ContainerBuilder builder = new ContainerBuilder();
            GenerateRootContainer(builder);
            _container = builder.Build();
        }

        private static void GenerateRootContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new XFCoreServicesModule());
            builder.RegisterModule(new ServicesModule());
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
