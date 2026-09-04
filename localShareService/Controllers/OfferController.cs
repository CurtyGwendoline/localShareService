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
    public async Task<ActionResult<IEnumerable<Offer>>> GetOffer()
    {
        return await _context.Offers.ToListAsync();
    }

    // GET: api/Offer/5
    [HttpGet("{offerid}")]
    public async Task<ActionResult<Offer>> GetOffer(int offerid)
    {
        var offer = await _context.Offers.FindAsync(offerid);

        if (offer == null)
        {
            return NotFound();
        }

        return offer;
    }

    // PUT: api/Offer/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{offerid}")]
    public async Task<IActionResult> PutOffer(int? offerid, Offer offer)
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
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Offer>> PostOffer(Offer offer)
    {
        _context.Offers.Add(offer);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetOffer", new { offerid = offer.OfferId }, offer);
    }

    // DELETE: api/Offer/5
    [HttpDelete("{offerid}")]
    public async Task<IActionResult> DeleteOffer(int? offerid)
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

    private bool OfferExists(int? offerid)
    {
        return _context.Offers.Any(e => e.OfferId == offerid);
    }
}