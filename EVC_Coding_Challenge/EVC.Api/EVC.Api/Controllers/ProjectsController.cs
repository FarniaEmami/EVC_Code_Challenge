using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EVC.Api.Data;
using EVC.Api.Models;

namespace EVC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly projectsContext _context;

        public ProjectsController(projectsContext context)
        {
            _context = context;
        }

        // GET: api/Projects
        [HttpGet]
        public IEnumerable<Projects> GetProjects()
        {
            return _context.Projects;
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjects([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projects = await _context.Projects.FindAsync(id);

            if (projects == null)
            {
                return NotFound();
            }

            return Ok(projects);
        }

        // PUT: api/Projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjects([FromRoute] int id, [FromBody] Projects projects)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projects.projectId)
            {
                return BadRequest();
            }

            _context.Entry(projects).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectsExists(id))
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

        // POST: api/Projects
        [HttpPost]
        public async Task<IActionResult> PostProjects([FromBody] Projects projects)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Projects.Add(projects);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjects", new { id = projects.projectId }, projects);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjects([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projects = await _context.Projects.FindAsync(id);
            if (projects == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(projects);
            await _context.SaveChangesAsync();

            return Ok(projects);
        }

        private bool ProjectsExists(int id)
        {
            return _context.Projects.Any(e => e.projectId == id);
        }
    }
}