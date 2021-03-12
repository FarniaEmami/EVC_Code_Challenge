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
    public class UserInvestmentsController : ControllerBase
    {
        private readonly projectsContext _context;

        public UserInvestmentsController(projectsContext context)
        {
            _context = context;
        }

        // GET: api/UserInvestments
        [HttpGet]
        public IEnumerable<UserInvestments> GetUserInvestments()
        {
            return _context.UserInvestments;
        }

        // GET: api/UserInvestments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserInvestments([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userInvestments = await _context.UserInvestments.FindAsync(id);

            if (userInvestments == null)
            {
                return NotFound();
            }

            return Ok(userInvestments);
        }

        // PUT: api/UserInvestments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserInvestments([FromRoute] int id, [FromBody] UserInvestments userInvestments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userInvestments.userInvestmentsId)
            {
                return BadRequest();
            }

            _context.Entry(userInvestments).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserInvestmentsExists(id))
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

        // POST: api/UserInvestments
        [HttpPost]
        public async Task<IActionResult> PostUserInvestments([FromBody] UserInvestments userInvestments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userInvestmentsRecord = await _context.UserInvestments
                                                        .FirstOrDefaultAsync(i => i.userID == userInvestments.userID && i.projectID == userInvestments.projectID);

            var proj = await _context.Projects
                                        .FirstOrDefaultAsync(i => i.projectId == userInvestments.projectID);

            if (userInvestmentsRecord != null)
            {
                return this.StatusCode(405, "You already invested in this project!");
            }
            else if (proj.remainingFund < userInvestments.investmentAmount)
            {
                return this.StatusCode(405, "The invested amount is more than remaining amount!");
            }

            

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.UserInvestments.Add(userInvestments);
                    await _context.SaveChangesAsync();

                    proj.remainingFund = proj.remainingFund - userInvestments.investmentAmount;

                    _context.Projects.Attach(proj);
                    _context.Entry(proj).Property(x => x.remainingFund).IsModified = true;
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }

            return CreatedAtAction("GetUserInvestments", new { id = userInvestments.userInvestmentsId }, userInvestments);
        }

        // DELETE: api/UserInvestments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserInvestments([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userInvestments = await _context.UserInvestments.FindAsync(id);
            if (userInvestments == null)
            {
                return NotFound();
            }

            _context.UserInvestments.Remove(userInvestments);
            await _context.SaveChangesAsync();

            return Ok(userInvestments);
        }

        private bool UserInvestmentsExists(int id)
        {
            return _context.UserInvestments.Any(e => e.userInvestmentsId == id);
        }
       
    }
}