using System;
using System.Linq;
using ServiceRequestApi.Models.Entities;

namespace ServiceRequestApi.Infrastructure.Data
{
    /// <summary>
    /// Seeds immutable reference data required for the system to function.
    /// This runs once at startup and is safe to re-run.
    /// </summary>
    public static class ReferenceDataSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // Ensure database exists
            context.Database.EnsureCreated();

            // ---------------------
            // Departments
            // ---------------------
            if (!context.Departments.Any())
            {
                context.Departments.AddRange(
                    new Department { DepartmentName = "IT" },
                    new Department { DepartmentName = "HR" },
                    new Department { DepartmentName = "Finance" }
                );
            }

            // ---------------------
            // Roles
            // ---------------------
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new Role { RoleName = "Admin" },
                    new Role { RoleName = "User" },
                    new Role { RoleName = "Manager" }
                );
            }

            // ---------------------
            // Request Statuses
            // ---------------------
            if (!context.RequestStatuses.Any())
            {
                context.RequestStatuses.AddRange(
                    new RequestStatus { StatusName = "New" },
                    new RequestStatus { StatusName = "In Progress" },
                    new RequestStatus { StatusName = "Closed" }
                );
            }

            context.SaveChanges();
        }
    }
}
