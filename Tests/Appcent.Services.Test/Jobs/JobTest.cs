using System;
using System.Collections.Generic;
using System.Linq;

using Appcent.Core.Domain;
using Appcent.Data;
using Appcent.Services.Jobs;
using Appcent.Tests;

using Moq;

using NUnit.Framework;

namespace Appcent.Services.Test.Jobs
{
    public class JobTest
    {
        private List<Job> _jobs;
        private IRepository<Job> _jobRepository;
        private IJobService _jobService;

        private IRepository<Job> SetUpJobRepository()
        {
            var mockRepo = new Mock<IRepository<Job>>();

            mockRepo.Setup(p => p.GetAll()).Returns(_jobs);

            mockRepo.Setup(p => p.GetById(It.IsAny<int>())).Returns(new Func<int, Job>(id => _jobs.Find(p => p.Id.Equals(id))));

            mockRepo.Setup(p => p.Insert(It.IsAny<Job>())).Callback(new Action<Job>(newJob =>
            {
                dynamic maxJobID = _jobs.Last().Id; dynamic nextJobID = maxJobID + 1; newJob.Id = nextJobID; _jobs.Add(newJob);
            }));

            mockRepo.Setup(p => p.Update(It.IsAny<Job>())).Callback(new Action<Job>(job =>
            {
                var oldJob = _jobs.Find(a => a.Id == job.Id); oldJob = job;
            }));

            mockRepo.Setup(p => p.Delete(It.IsAny<Job>())).Callback(new Action<Job>(job =>
            {
                var jobToRemove = _jobs.Find(a => a.Id == job.Id); if (jobToRemove != null) _jobs.Remove(jobToRemove);
            }));

            return mockRepo.Object;
        }

        private static List<Job> SetUpJobs()
        {
            var now = DateTime.Now;
            var jobs = new List<Job>
            {
                new Job
                {
                    Id = 1,
                    Deleted = false,
                    Description = "Hadi ellerimizi koda bulayalım",
                    ApplicationUserId = 1,
                    CreatedDate= now,
                    UpdatedDate = now
                },
                new Job()
                {
                    Id = 2,
                    Deleted = false,
                    Description = "Hadi ellerimizi koda bulayalım - 2",
                    ApplicationUserId = 2,
                    CreatedDate= now,
                    UpdatedDate = now
                },
            };

            return jobs;
        }

        [SetUp]
        public void Setup()
        {
            _jobs = SetUpJobs();
            _jobRepository = SetUpJobRepository();
            _jobService = new JobService(_jobRepository);
        }

        [Test]
        public void can_get_all()
        {
            var jobs = _jobService.GetAll();

            jobs.ShouldNotBeNull();
        }

        [Test]
        public void can_add_job()
        {
            var now = DateTime.Now;
            var job = new Job()
            {
                Id = 3,
                Deleted = false,
                Description = "Hadi ellerimizi koda bulayalım - 3",
                ApplicationUserId = 2,
                CreatedDate = now,
                UpdatedDate = now
            };

            _jobRepository.Insert(job);

            var addedJob = _jobRepository.GetById(3);

            addedJob.ShouldNotBeNull();
        }

        [Test]
        public void can_update_job()
        {
            var job = _jobRepository.GetById(2);

            job.Description = "First Name -UPDATED";

            _jobRepository.Update(job);

            var updatedJob = _jobRepository.GetById(2);

            updatedJob.ShouldNotBeNull();
        }

        [Test]
        public void can_delete_job()
        {
            _jobRepository.Delete(_jobRepository.GetById(2));

            var deletedJob = _jobRepository.GetById(2);

            deletedJob.ShouldBeNull();
        }
    }
}
