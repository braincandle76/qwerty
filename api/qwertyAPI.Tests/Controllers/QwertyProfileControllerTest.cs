using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using QwertyAPI.Controllers;
using QwertyAPI.Models;
using QwertyAPI.Tests.Utils;
using QwertyAPI.ViewModels;
using Xunit;

namespace QwertyAPI.Tests.Controllers
{
    public class QwertyProfileControllerTest : IAsyncLifetime
    {
        private QwertyProfileController testObject;
        private QwertyDbContext db;

        public async Task InitializeAsync()
        {
            db = await TestUtils.GetTestDbContext();
            testObject = new QwertyProfileController(db, new Mock<ILogger<QwertyProfileController>>().Object);
        }

        public async Task DisposeAsync()
        {
            await db.DisposeAsync();
        }

        public class GetProfile : QwertyProfileControllerTest
        {
            [Fact]
            public async void WhenProfileExists_ReturnsOkObjectContainingProfile()
            {
                var response = await testObject.Get();

                response.Should().BeOfType<OkObjectResult>();
                var result = (response as OkObjectResult).Value as QwertyProfileResponse; // this will be a list
                result.Id.Should().Be(db.QwertyProfiles.First(p => p.Name == TestUtils.PROFILE_NAME).Id);
                result.Name.Should().Be(TestUtils.PROFILE_NAME);
            }

            [Fact]
            public async void WhenNoProfile_ReturnsNotFound()
            {
                db.QwertyProfiles.RemoveRange(db.QwertyProfiles);
                await db.SaveChangesAsync();

                var response = await testObject.Get();

                response.Should().BeOfType<NotFoundResult>();
                // I think this will be empty list instead
            }

            [Fact]
            public async void WhenAnErrorOccursUsingDataBase_ThrowsError()
            {
                var mockDb = new Mock<QwertyDbContext>();
                mockDb.Setup(x => x.QwertyProfiles).Throws(new Exception("Something Broke"));
                var testObject = new QwertyProfileController(mockDb.Object, new Mock<ILogger<QwertyProfileController>>().Object);

                var exception = await Assert.ThrowsAsync<Exception>(() => testObject.Get());

                exception.Message.Should().Be("Something Broke");
            }
        }

        public class AddProfile : QwertyProfileControllerTest
        {
            [Fact]
            public async void WhenProfileNameChanges_ReturnsOkObject()
            {
                var NewQwertyProfile = new QwertyProfile(TestUtils.ADDED_NAME); // (arrange)Creates our request object

                var response = await testObject.Post(NewQwertyProfile); // (act)Calls test function

                response.Should().BeOfType<OkObjectResult>(); //(assert) result shoud be certain type-status 200 with a value

                var actualProfile = (response as OkObjectResult).Value as QwertyProfileResponse; // converting the value of our result to a QwertyProfileResponse

                var dbProfile = db.QwertyProfiles.FirstOrDefault(p => p.Name == TestUtils.ADDED_NAME); //retrieving the saved profile from the database based on the name of the request object that we passed into the test function
                dbProfile.Should().NotBeNull(); // asserting that the value we get back from the database is not null (confirms it was saved to the database)
                var expectedProfile = dbProfile; //variable rename for readability
                actualProfile.Id.Should().Be(expectedProfile.Id); //asserts that the Id on the response we got back matches the Id on the profile that was saved to the database
                actualProfile.Name.Should().Be(expectedProfile.Name); //asserts that the Name on the response we got back matches the Name on the profile that was saved to the database
            }
        }
    }
}