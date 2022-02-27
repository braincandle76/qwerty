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
    public class QwertyFavColorControllerTest : IAsyncLifetime
    {
        private QwertyFavColorController testObject;
        private QwertyDbContext db;

        public async Task InitializeAsync()
        {
            db = await TestUtils.GetTestDbContext();
            testObject = new QwertyFavColorController(db, new Mock<ILogger<QwertyFavColorController>>().Object);
        }

        public async Task DisposeAsync()
        {
            await db.DisposeAsync();
        }

        public class GetAll : QwertyFavColorControllerTest
        {
            [Fact]
            public async void WhenColorsExist_ReturnsOkObjectContainingColors()
            {
                var expectedColorCount = await db.QwertyFavColors.CountAsync();
                var expectedColorSingleResult = await db.QwertyFavColors.SingleAsync(c => c.Color == TestData.FAVORITE_COLOR);

                var response = await testObject.Get();

                response.Should().BeOfType<OkObjectResult>();
                var result = (response as OkObjectResult).Value as IEnumerable<QwertyFavColorResponse>;
                result.Count().Should().Be(expectedColorCount);
                result.Should().ContainSingle(c => c.Id == expectedColorSingleResult.Id && c.Name == expectedColorSingleResult.Color);
            }
        }
    }
}