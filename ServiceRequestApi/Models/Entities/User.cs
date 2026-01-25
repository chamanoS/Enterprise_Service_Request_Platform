using System.Collections.Generic;

namespace ServiceRequestApi.Models.Entities
{
  
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<ServiceRequest> ServiceRequests { get; set; }
    }
}
