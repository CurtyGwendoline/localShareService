using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using localShareService.Model;

[Route("api/[controller]")]
[ApiController]
public class OfferController : ControllerBase
{
    private readonly LocalShareCtx _context;

    public OfferController(LocalShareCtx context)
    {
        _context = context;
    }

    // GET: api/Offer
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Offer>>> GetOffers()
    {
        return await _context.Offers.Include(o => o.Suggestions).ToListAsync();
    }

    // GET: api/Offer/5
    [HttpGet("{offerid}")]
    public async Task<ActionResult<Offer>> GetOffer(int offerid)
    {
        var offer = await _context.Offers
            .Include(o => o.Suggestions)
            .FirstOrDefaultAsync(o => o.OfferId == offerid);

        if (offer == null)
        {
            return NotFound();
        }

        return offer;
    }

    // PUT: api/Offer/5
    [HttpPut("{offerid}")]
    public async Task<IActionResult> PutOffer(int offerid, Offer offer)
    {
        if (offerid != offer.OfferId)
        {
            return BadRequest();
        }

        _context.Entry(offer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!OfferExists(offerid))
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

    // POST: api/Offer
    [HttpPost]
    public async Task<ActionResult<Offer>> PostOffer(Offer offer)
    {
        if (offer == null)
        {
            return BadRequest("Offer data is missing.");
        }

        // Force OfferId to 0 so SQL Server auto-generates the identity key instead of throwing a key conflict error
        offer.OfferId = 0;

        try
        {
            _context.Offers.Add(offer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOffer), new { offerid = offer.OfferId }, offer);
        }
        catch (DbUpdateException ex)
        {
            // This catches Foreign Key errors (e.g., UserId doesn't exist in SQL Users table)
            return StatusCode(500, new { error = ex.InnerException?.Message ?? ex.Message });
        }
    }

    // DELETE: api/Offer/5
    [HttpDelete("{offerid}")]
    public async Task<IActionResult> DeleteOffer(int offerid)
    {
        var offer = await _context.Offers.FindAsync(offerid);
        if (offer == null)
        {
            return NotFound();
        }

        _context.Offers.Remove(offer);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool OfferExists(int offerid)
    {
        return _context.Offers.Any(e => e.OfferId == offerid);
    }
}