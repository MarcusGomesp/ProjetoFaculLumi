using Microsoft.EntityFrameworkCore;
using ProjetoFaculdade6Semestre.Models;

namespace ProjetoFaculdade6Semestre
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Cadastro> Cadastros { get; set; }

        //public override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //}
    }
}
