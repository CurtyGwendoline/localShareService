using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using localShareService.Model;

[Route("api/[controller]")]
[ApiController]
public class SuggestionController : ControllerBase
{
    private readonly LocalShareCtx _context;
    public SuggestionController(LocalShareCtx context)
    {
        _context = context;
    }

    // GET: api/Suggestion
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Suggestion>>> GetSuggestion()
    {
        return await _context.Suggestions.ToListAsync();
    }

    // GET: api/Suggestion/5
    [HttpGet("{suggestionid}")]
    public async Task<ActionResult<Suggestion>> GetSuggestion(int suggestionid)
    {
        var suggestion = await _context.Suggestions.FindAsync(suggestionid);

        if (suggestion == null)
        {
            return NotFound();
        }

        return suggestion;
    }

    // PUT: api/Suggestion/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{suggestionid}")]
    public async Task<IActionResult> PutSuggestion(int? suggestionid, Suggestion suggestion)
    {
        if (suggestionid != suggestion.SuggestionId)
        {
            return BadRequest();
        }

        _context.Entry(suggestion).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SuggestionExists(suggestionid))
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

    // POST: api/Suggestion
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Suggestion>> PostSuggestion(Suggestion suggestion)
    {
        _context.Suggestions.Add(suggestion);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetSuggestion", new { suggestionid = suggestion.SuggestionId }, suggestion);
    }

    // DELETE: api/Suggestion/5
    [HttpDelete("{suggestionid}")]
    public async Task<IActionResult> DeleteSuggestion(int? suggestionid)
    {
        var suggestion = await _context.Suggestions.FindAsync(suggestionid);
        if (suggestion == null)
        {
            return NotFound();
        }

        _context.Suggestions.Remove(suggestion);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SuggestionExists(int? suggestionid)
    {
        return _context.Suggestions.Any(e => e.SuggestionId == suggestionid);
    }
}
