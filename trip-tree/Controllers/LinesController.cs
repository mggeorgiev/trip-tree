using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trip_tree.Models;
using trip_tree.ViewModels;

namespace trip_tree.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinesController : ControllerBase
    {
        private readonly data_sciContext _context;

        public LinesController(data_sciContext context)
        {
            _context = context;
        }

        // GET: api/Lines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Line>>> GetLines()
        {
            return await _context.Lines.ToListAsync();
        }

        // GET: api/Lines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Line>> GetLine(int id)
        {
            var line = await _context.Lines.FindAsync(id);

            if (line == null)
            {
                return NotFound();
            }

            return line;
        }

        // GET: api/complete_graph
        [HttpGet("complete_graph")]
        public async Task<ActionResult<IEnumerable<v_complete_graph>>> GetCompleteGraph()
        {
            return await _context.V_Complete_Graphs.ToListAsync();
        }

        // GET: api/undirected_graph
        [HttpGet("undirected_graph")]
        public async Task<ActionResult<IEnumerable<v_undirected_graph>>> GetUndirectedGraph()
        {
            return await _context.V_Undirected_Graphs.ToListAsync();
        }

        //// PUT: api/Lines/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutLine(int id, Line line)
        //{
        //    if (id != line.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(line).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!LineExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Lines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Line>> PostLine(Line line)
        {
            _context.Lines.Add(line);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLine", new { id = line.Id }, line);
        }

        //// DELETE: api/Lines/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteLine(int id)
        //{
        //    var line = await _context.Lines.FindAsync(id);
        //    if (line == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Lines.Remove(line);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool LineExists(int id)
        {
            return _context.Lines.Any(e => e.Id == id);
        }
    }
}
