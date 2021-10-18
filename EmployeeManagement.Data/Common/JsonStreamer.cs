using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EmployeeManagement.Data.Models;
using EmployeeManagement.Data.Settings;
using Newtonsoft.Json;

namespace EmployeeManagement.Data.Common
{
    public class JsonStreamer
    {
        private enum DataType { Employee, EmployeeTask };

        private readonly JsonLocationSettings _jsonLocationSettings;

        public JsonStreamer(JsonLocationSettings jsonLocationSettings)
        {
            _jsonLocationSettings = jsonLocationSettings;
        }

        public IEnumerable<Employee> ReadEmployees()
        {
            return Read<Employee>(DataType.Employee);
        }

        public IEnumerable<EmployeeTask> ReadTasks()
        {
            return Read<EmployeeTask>(DataType.EmployeeTask);
        }

        public void SaveEmployee(List<Employee> employees)
        {
            Write(DataType.Employee, employees);
        }

        public void SaveTask(List<EmployeeTask> tasks)
        {
            Write(DataType.EmployeeTask, tasks);
        }

        private IEnumerable<T> Read<T>(DataType dataType)
        {
            try
            {
                var path = string.Empty;
                switch (dataType)
                {
                    case DataType.Employee:
                        path = _jsonLocationSettings.EmployeesJsonPath;
                        break;
                    case DataType.EmployeeTask:
                        path = _jsonLocationSettings.EmployeeTasksJsonPath;
                        break;
                }

                var json = string.Empty;
                using (var reader = new StreamReader(path))
                {
                    json = reader.ReadToEnd();
                }
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when reading {dataType.ToString()}: \n\r {ex.ToString()}");
            }
            return null;
        }

        private void Write<T>(DataType dataType, IEnumerable<T> data)
        {
            try
            {
                var path = string.Empty;
                switch (dataType)
                {
                    case DataType.Employee:
                        path = _jsonLocationSettings.EmployeesJsonPath;
                        break;
                    case DataType.EmployeeTask:
                        path = _jsonLocationSettings.EmployeeTasksJsonPath;
                        break;
                }

                var json = JsonConvert.SerializeObject(data, Formatting.Indented);
                FileStream fs = null;
                fs = new FileStream(path, FileMode.Create);
                using (var writer = new StreamWriter(fs))
                {
                    writer.Write(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when writing {dataType.ToString()}: \n\r {ex.ToString()}");
            }
        }
    }
}
