using System.Collections.Generic;

namespace ServiceRequestApi.Models.Entities
{
    /// <summary>
    /// Represents an organisational department (e.g. IT, HR, Facilities).
    /// Maps to the Departments table in SQL Server.
    /// </summary>
    public class Department
    {

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<ServiceRequest> ServiceRequests { get; set; }
    }
}
