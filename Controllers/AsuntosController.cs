using Microsoft.AspNetCore.Mvc;
using AsuntoService.Data;
using AsuntoService.Models;
using Microsoft.EntityFrameworkCore;

namespace AsuntoService.Controllers
{
    [Route("api/asuntos")]
    [ApiController]
    public class AsuntosController : ControllerBase
    {
        private readonly AsuntoContext _context;

        public AsuntosController(AsuntoContext context)
        {
            _context = context;
        }

        // GET: api/asuntos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asunto>>> GetAsuntos()
        {
            return await _context.Asuntos.ToListAsync();
        }

        // POST: api/asuntos
        [HttpPost]
        public async Task<ActionResult<Asunto>> PostAsunto(Asunto asunto)
        {
            _context.Asuntos.Add(asunto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAsuntos), new { id = asunto.Id }, asunto);
        }
    }
}
