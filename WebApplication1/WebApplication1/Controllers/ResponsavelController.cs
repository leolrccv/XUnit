using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponsavelController : ControllerBase
    {
        private readonly ProjContext _context;

        public ResponsavelController(ProjContext context)
        {
            _context = context;
        }

        // GET: api/Responsavel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Responsavel>>> GetResponsavel()
        {
            return await _context.Responsavel.ToListAsync();
        }

        // GET: api/Responsavel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Responsavel>> GetResponsavel(int id)
        {
            var responsavel = await _context.Responsavel.FindAsync(id);

            if (responsavel == null)
            {
                return NotFound();
            }

            return responsavel;
        }

        // PUT: api/Responsavel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResponsavel(int id, Responsavel responsavel)
        {
            if (id != responsavel.Id)
            {
                return BadRequest();
            }

            _context.Entry(responsavel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResponsavelExists(id))
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

        // POST: api/Responsavel
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Responsavel>> PostResponsavel(Responsavel responsavel)
        {
            _context.Responsavel.Add(responsavel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResponsavel", new { id = responsavel.Id }, responsavel);
        }

        // DELETE: api/Responsavel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResponsavel(int id)
        {
            var responsavel = await _context.Responsavel.FindAsync(id);
            if (responsavel == null)
            {
                return NotFound();
            }

            _context.Responsavel.Remove(responsavel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResponsavelExists(int id)
        {
            return _context.Responsavel.Any(e => e.Id == id);
        }
    }
}
