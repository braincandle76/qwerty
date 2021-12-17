using System;
using System.Linq;
using System.Threading.Tasks;
using QwertyApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace backend.Controllers
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
        }
    }
