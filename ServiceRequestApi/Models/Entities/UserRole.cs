namespace ServiceRequestApi.Models.Entities
{
    /// <summary>
    /// Join entity representing the many-to-many relationship
    /// between Users and Roles.
    /// </summary>
    public class UserRole
    {
        // =====================
        // Composite Key
        // =====================
        public int UserId { get; set; }
        public int RoleId { get; set; }

        // =====================
        // Navigation Properties
        // =====================
        public User User { get; set; }
        public Role Role { get; set; }
    }
}