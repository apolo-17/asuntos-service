using Microsoft.EntityFrameworkCore;
using AsuntoService.Models;

namespace AsuntoService.Data
{
    public class AsuntoContext : DbContext
    {
        public AsuntoContext(DbContextOptions<AsuntoContext> options) : base(options) { }

        public DbSet<Asunto> Asuntos { get; set; }
    }
}
