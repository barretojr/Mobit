using Microsoft.EntityFrameworkCore;
using Mobit.Models.Modelos;

namespace Mobit.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public AppDbContext() { }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Plano> Plano { get; set; }
        public DbSet<ClientePlano> ClientePlano { get; set; }
    }
}
