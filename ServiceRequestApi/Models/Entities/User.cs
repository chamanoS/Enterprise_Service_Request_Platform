using System.Collections.Generic;

namespace ServiceRequestApi.Models.Entities
{
  
    public class User
    {
       // --------------------
        // Identity / Auth
        // --------------------
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }

        // --------------------
        // Profile Info
        // --------------------
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        // --------------------
        // Relationships
        // --------------------
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<ServiceRequest> ServiceRequests { get; set; }
    }
}
