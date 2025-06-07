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
    public class ServicePricesController : ControllerBase
    {
        private readonly GeneCareContext _context;

        public ServicePricesController(GeneCareContext context)
        {
            _context = context;
        }

        // GET: api/ServicePrices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicePrice>>> GetServicePrices()
        {
            return await _context.ServicePrices.ToListAsync();
        }

        // GET: api/ServicePrices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServicePrice>> GetServicePrice(int id)
        {
            var servicePrice = await _context.ServicePrices.FindAsync(id);

            if (servicePrice == null)
            {
                return NotFound();
            }

            return servicePrice;
        }

        // PUT: api/ServicePrices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServicePrice(int id, ServicePrice servicePrice)
        {
            if (id != servicePrice.PriceId)
            {
                return BadRequest();
            }

            _context.Entry(servicePrice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicePriceExists(id))
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

        // POST: api/ServicePrices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServicePrice>> PostServicePrice(ServicePrice servicePrice)
        {
            _context.ServicePrices.Add(servicePrice);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ServicePriceExists(servicePrice.PriceId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetServicePrice", new { id = servicePrice.PriceId }, servicePrice);
        }

        // DELETE: api/ServicePrices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServicePrice(int id)
        {
            var servicePrice = await _context.ServicePrices.FindAsync(id);
            if (servicePrice == null)
            {
                return NotFound();
            }

            _context.ServicePrices.Remove(servicePrice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServicePriceExists(int id)
        {
            return _context.ServicePrices.Any(e => e.PriceId == id);
        }
    }
}
