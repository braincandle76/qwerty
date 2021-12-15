using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QwertyApi.Models;

namespace QwertyApi.Controllers
{
    [Route("api/QwertyProfiles")]
    [ApiController]
    public class QwertyProfilesController : ControllerBase
    {
        private readonly QwertyDbContext _context;

        public QwertyProfilesController(QwertyDbContext context)
        {
            _context = context;
        }

        // GET: api/QwertyProfiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QwertyProfileDTO>>> GetQwertyProfiles()
        {
            return await _context.QwertyProfiles
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/QwertyProfiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QwertyProfileDTO>> GetQwertyProfile(long id)
        {
            var QwertyProfile = await _context.QwertyProfiles.FindAsync(id);

            if (QwertyProfile == null)
            {
                return NotFound();
            }

            return ItemToDTO(QwertyProfile);
        }
        // PUT: api/QwertyProfiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQwertyProfile(long id, QwertyProfileDTO QwertyProfileDTO)
        {
            if (id != QwertyProfileDTO.Id)
            {
                return BadRequest();
            }

            var QwertyProfile = await _context.QwertyProfiles.FindAsync(id);
            if (QwertyProfile == null)
            {
                return NotFound();
            }

            QwertyProfile.Name = QwertyProfileDTO.Name;
            QwertyProfile.IsComplete = QwertyProfileDTO.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!QwertyProfileExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        // POST: api/QwertyProfiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QwertyProfileDTO>> CreateQwertyProfile(QwertyProfileDTO QwertyProfileDTO)
        {
            var QwertyProfile = new QwertyProfile
            {
                IsComplete = QwertyProfileDTO.IsComplete,
                Name = QwertyProfileDTO.Name
            };

            _context.QwertyProfiles.Add(QwertyProfile);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetQwertyProfile),
                new { id = QwertyProfile.Id },
                ItemToDTO(QwertyProfile));
        }

        // DELETE: api/QwertyProfiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQwertyProfile(long id)
        {
            var QwertyProfile = await _context.QwertyProfiles.FindAsync(id);

            if (QwertyProfile == null)
            {
                return NotFound();
            }

            _context.QwertyProfiles.Remove(QwertyProfile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QwertyProfileExists(long id)
        {
            return _context.QwertyProfiles.Any(e => e.Id == id);
        }

        private static QwertyProfileDTO ItemToDTO(QwertyProfile QwertyProfile) =>
            new QwertyProfileDTO
            {
                Id = QwertyProfile.Id,
                Name = QwertyProfile.Name,
                IsComplete = QwertyProfile.IsComplete
            };
    }
}