using Appcent.Data;
using Appcent.Services.ApplicationUserService;

using Autofac;

namespace Appcent.Api.Configurations
{
    public class AutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationUserService>().As<IApplicationUserService>();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>));
        }
    }
}