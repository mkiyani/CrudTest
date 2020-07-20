using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CrudTest.Application.Core.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Customers> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId);
                entity.Property(e => e.FirstName)
                .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth)
                .IsRequired();
                //.HasColumnType("datetime");
                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15);

                entity.Property(e => e.Email)
                    .HasMaxLength(20);


                entity.Property(e => e.BankAccountNumber)
                    .HasMaxLength(10);
            });
            modelBuilder.Entity<Customers>()
                .HasIndex(u => u.Email)
                .IsUnique();
            OnModelCreatingPartial(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        public override int SaveChanges()
        {
            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added
                                 || e.State == EntityState.Modified
                           select e.Entity;
            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(
                    entity,
                    validationContext,
                    validateAllProperties: true);
            }
            return base.SaveChanges();
        }
        
    }
}
