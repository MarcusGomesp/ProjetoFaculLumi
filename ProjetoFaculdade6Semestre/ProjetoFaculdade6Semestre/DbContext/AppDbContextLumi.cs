using Microsoft.EntityFrameworkCore;
using ProjetoFaculdade6Semestre.Model.CadastroLumi;
using System;

namespace ProjetoFaculdade6Semestre
{
    public class AppDbContextLumi : DbContext
    {

        public AppDbContextLumi(DbContextOptions<AppDbContextLumi> options) : base(options)
        { 
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Cv> Cvs { get; set; }
        public DbSet<Result> Results { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tabela Users
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Tabela Roles
            modelBuilder.Entity<Role>()
                .HasOne(r => r.Cv)
                .WithMany(c => c.Roles)
                .HasForeignKey(r => r.CvId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Role>()
                .HasOne(r => r.Owner)
                .WithMany()
                .HasForeignKey(r => r.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Tabela Cvs
            modelBuilder.Entity<Cv>()
                .HasMany(c => c.Results)
                .WithOne(r => r.Cv)
                .HasForeignKey(r => r.CvId)
                .OnDelete(DeleteBehavior.Cascade);

            // Result
            modelBuilder.Entity<Result>()
                .Property(r => r.Percentual)
                .HasColumnType("decimal(5,2)");
        }


    }
}
