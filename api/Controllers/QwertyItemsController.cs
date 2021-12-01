using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qwertyApi.Models;

namespace qwertyApi.Controllers
{
    [Route("api/qwertyItems")]
    [ApiController]
    public class qwertyItemsController : ControllerBase
    {
        private readonly qwertyContext _context;

        public qwertyItemsController(qwertyContext context)
        {
            _context = context;
        }

        // GET: api/qwertyItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<qwertyItemDTO>>> GetqwertyItems()
        {
            return await _context.qwertyItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/qwertyItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<qwertyItemDTO>> GetqwertyItem(long id)
        {
            var qwertyItem = await _context.qwertyItems.FindAsync(id);

            if (qwertyItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(qwertyItem);
        }
        // PUT: api/qwertyItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateqwertyItem(long id, qwertyItemDTO qwertyItemDTO)
        {
            if (id != qwertyItemDTO.Id)
            {
                return BadRequest();
            }

            var qwertyItem = await _context.qwertyItems.FindAsync(id);
            if (qwertyItem == null)
            {
                return NotFound();
            }

            qwertyItem.Name = qwertyItemDTO.Name;
            qwertyItem.IsComplete = qwertyItemDTO.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!qwertyItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        // POST: api/qwertyItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<qwertyItemDTO>> CreateqwertyItem(qwertyItemDTO qwertyItemDTO)
        {
            var qwertyItem = new qwertyItem
            {
                IsComplete = qwertyItemDTO.IsComplete,
                Name = qwertyItemDTO.Name
            };

            _context.qwertyItems.Add(qwertyItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetqwertyItem),
                new { id = qwertyItem.Id },
                ItemToDTO(qwertyItem));
        }

        // DELETE: api/qwertyItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteqwertyItem(long id)
        {
            var qwertyItem = await _context.qwertyItems.FindAsync(id);

            if (qwertyItem == null)
            {
                return NotFound();
            }

            _context.qwertyItems.Remove(qwertyItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool qwertyItemExists(long id)
        {
            return _context.qwertyItems.Any(e => e.Id == id);
        }

        private static qwertyItemDTO ItemToDTO(qwertyItem qwertyItem) =>
            new qwertyItemDTO
            {
                Id = qwertyItem.Id,
                Name = qwertyItem.Name,
                IsComplete = qwertyItem.IsComplete
            };
    }
}