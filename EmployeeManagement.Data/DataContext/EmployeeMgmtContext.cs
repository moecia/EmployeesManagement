using EmployeeManagement.Data.Common;
using EmployeeManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeManagement.Data.DataContext
{
    public class EmployeeMgmtContext
    {
        public EmployeesContext Employees { get; set; }
        public EmployeeTasksContext EmployeeTasks { get; set; }

        public EmployeeMgmtContext()
        {
            Employees = new EmployeesContext(JsonStreamer.ReadEmployees().ToList()) ;
            EmployeeTasks = new EmployeeTasksContext(JsonStreamer.ReadTasks().ToList());
        }
    }
}
