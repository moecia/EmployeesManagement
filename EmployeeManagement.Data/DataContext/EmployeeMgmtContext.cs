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
        public EmployeesRepository Employees { get; set; }
        public EmployeeTasksRepository EmployeeTasks { get; set; }

        private readonly JsonStreamer _jsonStreamer;

        public EmployeeMgmtContext(JsonStreamer jsonStreamer)
        {
            _jsonStreamer = jsonStreamer;
            Employees = new EmployeesRepository(jsonStreamer.ReadEmployees().ToList(), _jsonStreamer) ;
            EmployeeTasks = new EmployeeTasksRepository(jsonStreamer.ReadTasks().ToList(), _jsonStreamer);
        }
    }
}
