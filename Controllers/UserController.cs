using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using logincsharp.Models;
using Microsoft.AspNetCore.Cors;

namespace logincsharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly LoginContext _context;

        public UserController(LoginContext context)
        {
            _context = context;
        }

        // GET: api/User
        [EnableCors]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserData>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/5
        [EnableCors]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserData>> GetUserData(long id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var userData = await _context.Users.FindAsync(id);

            if (userData == null)
            {
                return NotFound();
            }

            return userData;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [EnableCors]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserData(long id, UserData userData)
        {
            if (id != userData.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDataExists(id))
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [EnableCors]
        [HttpPost]
        public async Task<ActionResult<UserData>> PostUserData(UserData userData)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'LoginContext.Users'  is null.");
          }
            _context.Users.Add(userData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserData", new { id = userData.UserId }, userData);
        }

        // DELETE: api/User/5
        [EnableCors]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserData(long id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var userData = await _context.Users.FindAsync(id);
            if (userData == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserDataExists(long id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
