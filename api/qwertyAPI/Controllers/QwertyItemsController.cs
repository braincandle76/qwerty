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
    public class QwertyItemsController : ControllerBase
    {
        private readonly QwertyDbContext _context;

        public QwertyItemsController(QwertyDbContext context)
        {
            _context = context;
        }

        // GET: api/qwertyItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QwertyItemDTO>>> GetqwertyItems()
        {
            return await _context.QwertyItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/qwertyItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QwertyItemDTO>> GetQwertyItem(long id)
        {
            var qwertyItem = await _context.QwertyItems.FindAsync(id);

            if (qwertyItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(qwertyItem);
        }
        // PUT: api/qwertyItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQwertyItem(long id, QwertyItemDTO qwertyItemDTO)
        {
            if (id != qwertyItemDTO.Id)
            {
                return BadRequest();
            }

            var qwertyItem = await _context.QwertyItems.FindAsync(id);
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
        public async Task<ActionResult<QwertyItemDTO>> CreateQwertyItem(QwertyItemDTO qwertyItemDTO)
        {
            var qwertyItem = new QwertyItem
            {
                IsComplete = qwertyItemDTO.IsComplete,
                Name = qwertyItemDTO.Name
            };

            _context.QwertyItems.Add(qwertyItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetQwertyItem),
                new { id = qwertyItem.Id },
                ItemToDTO(qwertyItem));
        }

        // DELETE: api/qwertyItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQwertyItem(long id)
        {
            var qwertyItem = await _context.QwertyItems.FindAsync(id);

            if (qwertyItem == null)
            {
                return NotFound();
            }

            _context.QwertyItems.Remove(qwertyItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool qwertyItemExists(long id)
        {
            return _context.QwertyItems.Any(e => e.Id == id);
        }

        private static QwertyItemDTO ItemToDTO(QwertyItem qwertyItem) =>
            new QwertyItemDTO
            {
                Id = qwertyItem.Id,
                Name = qwertyItem.Name,
                IsComplete = qwertyItem.IsComplete
            };
    }
}