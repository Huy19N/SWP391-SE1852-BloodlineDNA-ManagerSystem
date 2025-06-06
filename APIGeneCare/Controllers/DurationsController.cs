using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIGeneCare.Data;

namespace APIGeneCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DurationsController : ControllerBase
    {
        private readonly GeneCareContext _context;

        public DurationsController(GeneCareContext context)
        {
            _context = context;
        }

        // GET: api/Durations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Duration>>> GetDurations()
        {
            return await _context.Durations.ToListAsync();
        }

        // GET: api/Durations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Duration>> GetDuration(int id)
        {
            var duration = await _context.Durations.FindAsync(id);

            if (duration == null)
            {
                return NotFound();
            }

            return duration;
        }

        // PUT: api/Durations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDuration(int id, Duration duration)
        {
            if (id != duration.DurationId)
            {
                return BadRequest();
            }

            _context.Entry(duration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DurationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Durations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Duration>> PostDuration(Duration duration)
        {
            _context.Durations.Add(duration);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DurationExists(duration.DurationId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDuration", new { id = duration.DurationId }, duration);
        }

        // DELETE: api/Durations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDuration(int id)
        {
            var duration = await _context.Durations.FindAsync(id);
            if (duration == null)
            {
                return NotFound();
            }

            _context.Durations.Remove(duration);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DurationExists(int id)
        {
            return _context.Durations.Any(e => e.DurationId == id);
        }
    }
}
