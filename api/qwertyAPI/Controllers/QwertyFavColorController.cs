using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QwertyAPI.Models;
using QwertyAPI.ViewModels;

namespace QwertyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QwertyFavColorController : ControllerBase
    {
        private readonly QwertyDbContext _db;
        private readonly ILogger<QwertyFavColorController> _logger;

        public QwertyFavColorController(QwertyDbContext db, ILogger<QwertyFavColorController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var colors = await _db.QwertyFavColors.ToListAsync();
                return new OkObjectResult(colors.Select(c => new QwertyFavColorResponse(c)));
            }

            catch (Exception e)
            {
                _logger.LogCritical($"SQL Read error. It is likely that there is no database connection established. ${e.Message}");
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(QwertyFavColorRequest favColorRequest)
        {
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(favColorRequest, new ValidationContext(favColorRequest), validationResults, true))
            {
                return new BadRequestObjectResult(validationResults);
            }

            var newQwertyFavColor = new QwertyFavColor
            {
                Color = favColorRequest.Color,
                Id = favColorRequest.QwertyFavColorId,
            };

            _db.QwertyFavColors.Add(newQwertyFavColor);
            await _db.SaveChangesAsync();
            var addedQwertyFavColor = await _db.QwertyFavColors
                .Include(c => c.Color)
                .SingleAsync(p => p.Id == newQwertyFavColor.Id);

            return new CreatedResult("api/QwertyFavColors/" + newQwertyFavColor.Id, new QwertyFavColorResponse(addedQwertyFavColor));
        }
    }
}
