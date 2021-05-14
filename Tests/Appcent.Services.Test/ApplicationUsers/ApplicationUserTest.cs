using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Appcent.Core;
using Appcent.Core.Domain;
using Appcent.Data;
using Appcent.Services.ApplicationUsers;
using Appcent.Tests;

using Castle.Core.Resource;

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

using Moq;

using NUnit.Framework;

namespace Appcent.Services.Test.ApplicationUsers
{
    [TestFixture]
    public class ApplicationUserTest
    {
        private List<ApplicationUser> _applicationUsers;
        private IRepository<ApplicationUser> _applicationUserRepository;
        private IApplicationUserService _applicationUserService;

        private IRepository<ApplicationUser> SetUpApplicationUserRepository()
        {
            var mockRepo = new Mock<IRepository<ApplicationUser>>();

            mockRepo.Setup(p => p.GetAll()).Returns(_applicationUsers);

            mockRepo.Setup(p => p.GetById(It.IsAny<int>())).Returns(new Func<int, ApplicationUser>(id => _applicationUsers.Find(p => p.Id.Equals(id))));

            mockRepo.Setup(p => p.Insert(It.IsAny<ApplicationUser>())).Callback(new Action<ApplicationUser>(newApplicationUser =>
            {
                dynamic maxApplicationUserID = _applicationUsers.Last().Id; dynamic nextApplicationUserID = maxApplicationUserID + 1; newApplicationUser.Id = nextApplicationUserID; _applicationUsers.Add(newApplicationUser);
            }));

            mockRepo.Setup(p => p.Update(It.IsAny<ApplicationUser>())).Callback(new Action<ApplicationUser>(user =>
            {
                var oldApplicationUser = _applicationUsers.Find(a => a.Id == user.Id); oldApplicationUser = user;
            }));

            mockRepo.Setup(p => p.Delete(It.IsAny<ApplicationUser>())).Callback(new Action<ApplicationUser>(user =>
            {
                var applicationUserToRemove = _applicationUsers.Find(a => a.Id == user.Id); if (applicationUserToRemove != null) _applicationUsers.Remove(applicationUserToRemove);
            }));

            return mockRepo.Object;
        }

        private static List<ApplicationUser> SetUpApplicationUsers()
        {
            var jobs = new List<Job> { new Job { Id = 1, CreatedDate = DateTime.Now, Deleted = false, Description = "Hadi ellerimizi koda bulayalım", UpdatedDate = DateTime.Now } };
            var jobs1 = new List<Job> { new Job { Id = 2, CreatedDate = DateTime.Now, Deleted = false, Description = "Hadi ellerimizi koda bulayalım", UpdatedDate = DateTime.Now } };
            var users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = 1,
                    FirstName = "FirstName 1",
                    LastName = "LastName 1",
                    Deleted = false,
                    Password = "password",
                    Username = "username",
                    Jobs = jobs
                },
                new ApplicationUser()
                {
                    Id = 2,
                    FirstName = "FirstName 2",
                    LastName = "LastName 2",
                    Deleted = false,
                    Password = "password2",
                    Username = "username3",
                    Jobs = jobs1
                },
            };

            return users;
        }

        [SetUp]
        public void Setup()
        {
            _applicationUsers = SetUpApplicationUsers();
            _applicationUserRepository = SetUpApplicationUserRepository();
            _applicationUserService = new ApplicationUserService(_applicationUserRepository, null);
        }

        [Test]
        public void can_get_all()
        {
            var applicationUsers = _applicationUserService.GetAll();

            applicationUsers.ShouldNotBeNull();
        }

        [Test]
        public void can_add_application_user()
        {
            var appUser = new ApplicationUser()
            {
                Id = 3,
                Deleted = false,
                FirstName = "First Name 3",
                LastName = "Last Name 3",
                Jobs = new List<Job> { },
                Password = "Password 3",
                Username = "Username 3"
            };

            _applicationUserRepository.Insert(appUser);

            var addedUser = _applicationUserRepository.GetById(3);

            addedUser.ShouldNotBeNull();
        }

        [Test]
        public void can_update_application_user()
        {
            var user = _applicationUserRepository.GetById(2);

            user.FirstName = "First Name -UPDATED";

            _applicationUserRepository.Update(user);

            var updatedUser = _applicationUserRepository.GetById(2);

            updatedUser.ShouldNotBeNull();
        }

        [Test]
        public void can_delete_application_user()
        {
            _applicationUserRepository.Delete(_applicationUserRepository.GetById(2));
            
            var deletedUser = _applicationUserRepository.GetById(2);

            deletedUser.ShouldBeNull();
        }
    }
}
