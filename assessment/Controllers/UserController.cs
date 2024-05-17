using assessment.Data;
using assessment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _context;
        public UserController(UserDbContext context) {
            _context = context;
        }

        // get all users
        [HttpGet]
        // [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.User.ToListAsync();
        }

        [HttpGet("getuser/{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _context.User.FindAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        // create a new user
        [HttpPost("createuser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> addUser( User user )
        {
            await _context.User.AddAsync( user );
            await _context.SaveChangesAsync();
            // helps return response with status code and location in the header
            // take: methods to return single user. id of the user .. and the user itself
            return CreatedAtAction(nameof(GetById), new { id = user.ID }, user);
        }

        // updating user
        [HttpPut("updateuser/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.ID) return BadRequest();

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // delete a user by id
        [HttpDelete("deleteuser/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userToDelete = await _context.User.FindAsync(id);
            // when not found
            if(userToDelete == null) return NotFound();
            // delete when user is/was found
            _context.User.Remove(userToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
