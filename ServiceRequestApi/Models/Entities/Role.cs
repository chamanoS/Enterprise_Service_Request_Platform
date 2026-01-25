using System.Collections.Generic;

namespace ServiceRequestApi.Models.Entities
{
    /// <summary>
    /// Represents a security role in the system (Employee, Manager, Admin).
    /// Used for role-based authorization.
    /// </summary>
    public class Role
    {
    
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}