using System.Collections.Generic;

namespace EmployeeManagement.Data.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HiredDate { get; set; }
        public IEnumerable<EmployeeTask> Tasks { get; set; }

    }
}
