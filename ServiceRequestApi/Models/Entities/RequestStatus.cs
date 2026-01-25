using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // needed for [Key]

namespace ServiceRequestApi.Models.Entities
{
    /// <summary>
    /// Represents the workflow status of a service request
    /// (New, In Progress, Closed, etc.).
    /// This is reference data used to drive request lifecycle.
    /// </summary>
    public class RequestStatus
    {
        [Key] 
        public int StatusId { get; set; }

        public string StatusName { get; set; }

        public ICollection<ServiceRequest> ServiceRequests { get; set; }
    }
}
