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
    }
}
