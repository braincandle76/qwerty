using System;
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
                var profile = await _db.QwertyProfiles.ToListAsync();
                return new OkObjectResult(profile);
            }

            catch (Exception e)
            {
                _logger.LogCritical($"SQL Read error. It is likely that there is no database connection established. ${e.Message}");
                throw;
            }
        }
    }
}
