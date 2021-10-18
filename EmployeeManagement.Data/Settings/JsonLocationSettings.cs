using Microsoft.Extensions.Configuration;

namespace EmployeeManagement.Data.Settings
{
    public class JsonLocationSettings
    {
        private IConfiguration _configuration;
        private string _employeesJsonPath;
        private string _employeeTasksJsonPath;


        public JsonLocationSettings(IConfiguration configuration)
        {
            _configuration = configuration;
            _employeesJsonPath = _configuration["EmployeesJsonPath"];
            _employeeTasksJsonPath = _configuration["EmployeeTasksJsonPath"];
        }

        public JsonLocationSettings()
        {
            _employeesJsonPath = "Employees.json";
            _employeeTasksJsonPath = "Tasks.json";
        }

        public string EmployeesJsonPath => _employeesJsonPath;

        public string EmployeeTasksJsonPath => _employeeTasksJsonPath;

    }
}
