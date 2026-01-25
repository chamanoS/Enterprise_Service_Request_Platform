using System;
using System.ComponentModel.DataAnnotations;
namespace ServiceRequestApi.Models.Entities
{
    /// <summary>
    /// Represents a service request submitted by a user
    /// (IT, HR, Facilities, etc.).
    /// This is the core transactional entity of the system.
    /// </summary>
    public class ServiceRequest
    {
         [Key]  // <-- This is required
        public int RequestId { get; set; }

        // =====================
        // Foreign Keys
        // =====================
        public int UserId { get; set; }
        public int DepartmentId { get; set; }
        public int StatusId { get; set; }

        // =====================
        // Core Fields
        // =====================
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        // =====================
        // Navigation Properties
        // =====================
        public User User { get; set; }
        public Department Department { get; set; }
        public RequestStatus Status { get; set; }
    }
}