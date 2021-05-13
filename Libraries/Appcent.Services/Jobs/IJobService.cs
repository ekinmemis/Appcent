using System.Collections.Generic;

using Appcent.Core.Domain;

namespace Appcent.Services.Jobs
{
    public interface IJobService
    {
        Job GetJobById(int jobId);

        IEnumerable<Job> GetAll();

        IList<Job> GetJobsByIds(int[] jobIds);

        void InsertJob(Job job);

        void UpdateJob(Job job);

        void DeleteJob(Job job);
    }
}