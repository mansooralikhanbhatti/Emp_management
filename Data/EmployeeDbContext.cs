using Microsoft.EntityFrameworkCore;

namespace Emp_management.Data
{
    public partial class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext()
        {
        }

        // Use dependency injection to pass the options, no need for OnConfiguring anymore.
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpId).HasName("PK__Employee__AF2DBB99665C66D6");

                entity.ToTable("Employee");

                entity.HasIndex(e => e.Email, "UQ__Employee__A9D10534B2E4AE78").IsUnique();

                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.Property(e => e.Name).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
