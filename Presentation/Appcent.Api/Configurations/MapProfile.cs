using Appcent.Api.Models.ApplicationUserModels;
using Appcent.Api.Models.Jobs;
using Appcent.Core.Domain;

using AutoMapper;

namespace Appcent.Api.Configurations
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserModel>();
            CreateMap<ApplicationUserModel, ApplicationUser>();

            CreateMap<Job, JobModel>();
            CreateMap<JobModel, Job>();
        }
    }
}