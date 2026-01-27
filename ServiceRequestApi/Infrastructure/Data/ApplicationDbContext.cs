using Microsoft.EntityFrameworkCore;
using ServiceRequestApi.Models.Entities;

namespace ServiceRequestApi.Infrastructure.Data
{
    /// <summary>
    /// EF Core database context.
    /// Acts as the bridge between the domain entities and SQL Server.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        // =====================
        // STEP 1: Constructor
        // =====================
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // =====================
        // STEP 2: DbSet Declarations
        // =====================
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }

        // =====================
        // STEP 3: Model Configuration
        // =====================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // STEP 3.1: UserRole Composite Key
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            // STEP 3.2: User ↔ Department (Many-to-One)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Department)
                .WithMany(d => d.Users)
                .HasForeignKey(u => u.DepartmentId);

            // STEP 3.3: User ↔ Role (Many-to-Many via UserRole)
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // STEP 3.4: ServiceRequest Relationships
            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.User)
                .WithMany(u => u.ServiceRequests)
                .HasForeignKey(sr => sr.UserId)
                .OnDelete(DeleteBehavior.Restrict); // <- Disable cascade delete

            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Department)
                .WithMany(d => d.ServiceRequests)
                .HasForeignKey(sr => sr.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict); // <- Optional

            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Status)
                .WithMany(s => s.ServiceRequests)
                .HasForeignKey(sr => sr.StatusId)
                .OnDelete(DeleteBehavior.Restrict); // <- optional


           
        }
    }
}
