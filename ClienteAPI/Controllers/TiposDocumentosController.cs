using ClienteAPI.Data;
using ClienteAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClienteAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TiposDocumentosController : ControllerBase
{
    private readonly BdClientesContext _context;

    public TiposDocumentosController(BdClientesContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TiposDocumento>>> GetTiposDocumentos()
    {
        return await _context.TiposDocumentos.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TiposDocumento>> GetTiposDocumento(byte id)
    {
        var tiposDocumento = await _context.TiposDocumentos.FindAsync(id);

        if (tiposDocumento == null)
        {
            return NotFound();
        }

        return tiposDocumento;
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutTiposDocumento(byte id, TiposDocumento tiposDocumento)
    {
        if (id != tiposDocumento.IdTipoDocumento)
        {
            return BadRequest();
        }

        _context.Entry(tiposDocumento).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TiposDocumentoExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<TiposDocumento>> PostTiposDocumento(TiposDocumento tiposDocumento)
    {
        _context.TiposDocumentos.Add(tiposDocumento);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTiposDocumento), new { id = tiposDocumento.IdTipoDocumento }, tiposDocumento);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTiposDocumento(byte id)
    {
        var tiposDocumento = await _context.TiposDocumentos.FindAsync(id);
        if (tiposDocumento == null)
        {
            return NotFound();
        }

        _context.TiposDocumentos.Remove(tiposDocumento);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TiposDocumentoExists(byte id)
    {
        return _context.TiposDocumentos.Any(e => e.IdTipoDocumento == id);
    }
}
