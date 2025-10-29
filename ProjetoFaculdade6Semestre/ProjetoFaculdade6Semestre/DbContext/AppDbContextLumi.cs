using Microsoft.EntityFrameworkCore;
using ProjetoFaculdade6Semestre.Model.CadastroLumi;
using System;

namespace ProjetoFaculdade6Semestre
{
    public class AppDbContextLumi : DbContext
    {
        public AppDbContextLumi(DbContextOptions<AppDbContextLumi> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Cv> Cvs { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Candidatura> Candidaturas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

       
            //  USER - ROLE
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // ROLE-CV
            modelBuilder.Entity<Role>()
                .HasOne(r => r.Cv)
                .WithMany(c => c.Roles)
                .HasForeignKey(r => r.CvId)
                .OnDelete(DeleteBehavior.Restrict); 

            //  ROLE - OWNER (USER)
            modelBuilder.Entity<Role>()
                .HasOne(r => r.Owner)
                .WithMany()
                .HasForeignKey(r => r.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // CV - RESULT
            modelBuilder.Entity<Cv>()
                .HasMany(c => c.Results)
                .WithOne(r => r.Cv)
                .HasForeignKey(r => r.CvId)
                .OnDelete(DeleteBehavior.Cascade);

            // USER  - CANDIDATURA
            modelBuilder.Entity<Candidatura>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //  ROLE -  CANDIDATURA
            modelBuilder.Entity<Candidatura>()
                .HasOne(c => c.Role)
                .WithMany()
                .HasForeignKey(c => c.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            // CONFIGURACAO DATAS
            modelBuilder.Entity<User>()
                .Property<DateTime>("CreatedAt")
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Cv>()
                .Property<DateTime>("UploadDate")
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Role>()
                .Property<DateTime>("CreatedAt")
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Result>()
                .Property<DateTime>("CreatedAt")
                .HasDefaultValueSql("GETUTCDATE()");

            // TIPOS DE COLUNA ESPECÍFICOS
            modelBuilder.Entity<Result>()
                .Property(r => r.Percentual)
                .HasColumnType("decimal(5,2)");

            //  SEED DATA (ADMIN)
            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 1,
                UserName = "Admin",
                Email = "admin@teste.com",
                PasswordHash = "$2a$11$9lmR6gE7idQeC6pkwgwXduUnKU3E7ENtUo7UCCe9ZFdK9XHLqgFMi"
            });

            modelBuilder.Entity<Cv>().HasData(new Cv
            {
                CvId = 1,
                FileName = "cv_admin.pdf",
                FilePath = "/uploads/cv_admin.pdf",
                UserId = 1
            });
        }
    }
}
