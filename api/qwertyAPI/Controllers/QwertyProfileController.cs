using System;
using System.Collections.Generic;
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
    public class QwertyProfileController : ControllerBase
    {
        private readonly QwertyDbContext _db;
        private readonly ILogger<QwertyProfileController> _logger;

        public QwertyProfileController(QwertyDbContext db, ILogger<QwertyProfileController> logger)
        {
            _db = db;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var profiles = await _db.QwertyProfiles
                .Include(p => p.FavColor)
                .ToListAsync();
                var qwertyProfileResponse = profiles.Select(p => new QwertyProfileResponse(p));
                return new OkObjectResult(qwertyProfileResponse);
            }

            catch (Exception e)
            {
                _logger.LogCritical($"SQL Read error. It is likely that there is no database connection established. ${e.Message}");
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(QwertyProfileRequest profileRequest)
        {
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(profileRequest, new ValidationContext(profileRequest), validationResults, true))
            {
                return new BadRequestObjectResult(validationResults);
            }

            var newQwertyProfile = new QwertyProfile
            {
                Name = profileRequest.Name,
                QwertyFavColorId = profileRequest.QwertyFavColorId,
            };

            if (_db.QwertyProfiles.Any(p => p.Name == newQwertyProfile.Name))
            {
                return new ConflictObjectResult("Profile creation failed due to duplicate Profile name");
            }

            _db.QwertyProfiles.Add(newQwertyProfile);
            await _db.SaveChangesAsync();
            var addedQwertyProfile = await _db.QwertyProfiles
                .Include(c => c.FavColor)
                .SingleAsync(p => p.Id == newQwertyProfile.Id);

            return new CreatedResult("api/QwertyProfiles/" + newQwertyProfile.Id, new QwertyProfileResponse(addedQwertyProfile));
        }
    }
}
