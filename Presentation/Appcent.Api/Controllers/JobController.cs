using System.Collections.Generic;

using Appcent.Api.Attributes;
using Appcent.Api.Models.Jobs;
using Appcent.Core.Domain;
using Appcent.Services.Jobs;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;

namespace Appcent.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;

        public JobController(IJobService jobService, IMapper mapper)
        {
            _jobService = jobService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<JobModel>>(_jobService.GetAll()));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_mapper.Map<JobModel>(_jobService.GetJobById(id)));
        }

        [HttpPost]
        public IActionResult Insert(JobModel model)
        {
            _jobService.InsertJob(_mapper.Map<Job>(model));
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(JobModel model)
        {
            var job = _jobService.GetJobById(model.Id);

            job.Description = model.Description;
            job.ApplicationUserId = model.ApplicationUserId;

            _jobService.UpdateJob(job);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _jobService.DeleteJob(_jobService.GetJobById(id));
            return NoContent();
        }
    }
}
