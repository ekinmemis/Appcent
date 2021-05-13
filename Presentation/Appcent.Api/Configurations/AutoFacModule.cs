using Appcent.Data;
using Appcent.Services.ApplicationUsers;
using Appcent.Services.Jobs;

using Autofac;

namespace Appcent.Api.Configurations
{
    public class AutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationUserService>().As<IApplicationUserService>();
            builder.RegisterType<JobService>().As<IJobService>();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>));
        }
    }
}