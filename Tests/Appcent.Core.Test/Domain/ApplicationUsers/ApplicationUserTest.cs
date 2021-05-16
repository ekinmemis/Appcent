
using System;
using System.Collections.Generic;

using Appcent.Core.Domain;
using Appcent.Tests;

using NUnit.Framework;

namespace Appcent.Core.Test.Domain.ApplicationUsers
{
    [TestFixture]
    public class ApplicationUserTest
    {
        [Test]
        public void Can_clone_user()
        {
            var jobs = new List<Job> { new Job { ApplicationUserId = 1, CreatedDate = DateTime.Now, Deleted = false, Description = "Hadi ellerimizi koda bulayalım", Id = 1, UpdatedDate = DateTime.Now } };
            var applicationUser = new ApplicationUser
            {
                Id = 1,
                FirstName = "FirstName 1",
                LastName = "LastName 1",
                Deleted = false,
                Password = "password",
                Username = "username",
                Jobs = jobs
            };

            var newApplicationUser = applicationUser.Clone() as ApplicationUser;

            newApplicationUser.ShouldNotBeNull();
            newApplicationUser.Id.ShouldEqual(1);
            newApplicationUser.FirstName.ShouldEqual("FirstName 1");
            newApplicationUser.LastName.ShouldEqual("LastName 1");
            newApplicationUser.Deleted.ShouldEqual(false);
            newApplicationUser.Username.ShouldEqual("username");
            newApplicationUser.Password.ShouldEqual("password");
            newApplicationUser.Jobs.ShouldEqual(jobs);
        }
    }
}