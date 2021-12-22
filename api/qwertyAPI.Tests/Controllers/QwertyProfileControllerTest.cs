using System;
using System.Linq;
using System.Threading.Tasks;
using QwertyAPI.Controllers;
using QwertyAPI.Models;
using QwertyAPI.ViewModels;
using QwertyAPI.Tests.Utils;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
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
                var result = (response as OkObjectResult).Value as QwertyProfileResponse;
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
    }
}