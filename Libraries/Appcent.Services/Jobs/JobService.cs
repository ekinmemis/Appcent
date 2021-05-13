using System;
using System.Collections.Generic;
using System.Linq;

using Appcent.Core.Domain;
using Appcent.Data;

namespace Appcent.Services.Jobs
{
    public class JobService : IJobService
    {
        private readonly IRepository<Job> _jobRepository;

        public JobService(IRepository<Job> jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public Job GetJobById(int jobId)
        {
            if (jobId == 0)
                return null;

            return _jobRepository.GetById(jobId);
        }

        public IList<Job> GetJobsByIds(int[] jobIds)
        {
            if (jobIds == null || jobIds.Length == 0)
                return new List<Job>();

            var query = from c in _jobRepository.Table
                        where jobIds.Contains(c.Id) && !c.Deleted
                        select c;
            var jobs = query.ToList();
            var sortedJobs = new List<Job>();
            foreach (var id in jobIds)
            {
                var job = jobs.Find(x => x.Id == id);
                if (job != null)
                    sortedJobs.Add(job);
            }

            return sortedJobs;
        }

        public void InsertJob(Job job)
        {
            if (job == null)
                throw new ArgumentNullException(nameof(job));

            _jobRepository.Insert(job);
        }

        public void UpdateJob(Job job)
        {
            if (job == null)
                throw new ArgumentNullException(nameof(job));

            _jobRepository.Update(job);
        }

        public void DeleteJob(Job job)
        {
            if (job == null)
                throw new ArgumentNullException(nameof(job));

            job.Deleted = true;
            job.Description += "-DELETED";

            UpdateJob(job);
        }

        public IEnumerable<Job> GetAll()
        {
            return _jobRepository.GetAll();
        }
    }
}