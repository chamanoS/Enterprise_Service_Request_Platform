using System.Collections.Generic;

namespace ServiceRequestApi.Models.Entities
{
    /// <summary>
    /// Represents an application user.
    /// This entity maps directly to the Users table in SQL Server.
    /// </summary>
    public class User
    {
        // =====================
        // Primary Key
        // =====================
        public int UserId { get; set; }

        // =====================
        // Core User Fields
        // =====================
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }

        // =====================
        // Foreign Keys
        // =====================
        public int DepartmentId { get; set; }

        // =====================
        // Navigation Properties
        // =====================
        public Department Department { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<ServiceRequest> ServiceRequests { get; set; }
    }
}
