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

        public class Post : QwertyProfileControllerTest
        {
            [Fact]

            public async void WhenNewQwertyProfileIsAdded_ReturnsOkObject()
            {
                var color = db.QwertyFavColors.First(ofc => ofc.Color == TestData.FAVORITE_COLOR);
                var newQwertyProfile = new QwertyProfileRequest
                {
                    Name = "Amelia Earhart",
                    QwertyFavColorId = color.Id,
                };
                var response = await testObject.Post(newQwertyProfile);
                var result = (response as CreatedResult).Value as QwertyProfileResponse;
                result.Id.Should().BeGreaterThan(0);
                result.Name.Should().Be(newQwertyProfile.Name);
            }

        }
    }
}