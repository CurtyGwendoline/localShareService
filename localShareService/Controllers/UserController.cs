using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using localShareService.Model;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly LocalShareCtx _context;
    public UserController(LocalShareCtx context)
    {
        _context = context;
    }

    // POST : api/User/sync
    [HttpPost("sync")]
    public async Task<ActionResult<User>> SyncUser([FromBody] User incomingUser)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.AzureId == incomingUser.AzureId);

        if (existingUser != null)
        {
            existingUser.Email = incomingUser.Email;
            existingUser.UserName = incomingUser.UserName;
            await _context.SaveChangesAsync();
            return Ok(existingUser);
        }

        _context.Users.Add(incomingUser);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { azureId = incomingUser.AzureId }, incomingUser);
    }

    // GET: api/User
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUser()
    {
        return await _context.Users.ToListAsync();
    }

    // GET: api/User/{azureId}
    [HttpGet("{azureId}")]
    public async Task<ActionResult<User>> GetUser(string azureId)
    {
        var user = await _context.Users.FindAsync(azureId);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    // PUT: api/User/{azureId}
    [HttpPut("{azureId}")]
    public async Task<IActionResult> PutUser(string azureId, User user)
    {
        if (azureId != user.AzureId)
        {
            return BadRequest();
        }

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(azureId))
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
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { azureId = user.AzureId }, user);
    }

    // DELETE: api/User/{azureId}
    [HttpDelete("{azureId}")]
    public async Task<IActionResult> DeleteUser(string azureId)
    {
        var user = await _context.Users.FindAsync(azureId);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserExists(string azureId)
    {
        return _context.Users.Any(e => e.AzureId == azureId);
    }
}