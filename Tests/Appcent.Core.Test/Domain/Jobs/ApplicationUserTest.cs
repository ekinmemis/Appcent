
using System;
using System.Collections.Generic;
using System.Net;

using Appcent.Core.Domain;
using Appcent.Tests;

using NUnit.Framework;

namespace Appcent.Core.Test.Domain.Jobs
{
    [TestFixture]
    public class JobTest
    {
        [Test]
        public void Can_clone_user()
        {
            var appUser = new ApplicationUser();
            var job = new Job
            {
                Id = 1,
                Description = "Description 1",
                Deleted = false,
                ApplicationUserId = 1,
                UpdatedDate = new DateTime(1999, 09, 26),
                CreatedDate = new DateTime(1999, 09, 26),
                ApplicationUser = appUser
            };

            var newJob = job.Clone() as Job;

            newJob.ShouldNotBeNull();
            newJob.Id.ShouldEqual(1);
            newJob.Description.ShouldEqual("Description 1");
            newJob.UpdatedDate.ShouldEqual(new DateTime(1999, 09, 26));
            newJob.CreatedDate.ShouldEqual(new DateTime(1999, 09, 26));
            newJob.ApplicationUser.ShouldEqual(appUser);
        }
    }
}