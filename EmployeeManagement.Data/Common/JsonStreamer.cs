using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EmployeeManagement.Data.Models;
using Newtonsoft.Json;

namespace EmployeeManagement.Data.Common
{
    public static class JsonStreamer
    {
        private const string EMPLOYEE_PATH = "Employees.json";
        private const string TASK_PATH = "Tasks.json";
        private enum DataType { Employee, EmployeeTask };

        public static IEnumerable<Employee> ReadEmployees()
        {
            return Read<Employee>(DataType.Employee);
        }

        public static IEnumerable<EmployeeTask> ReadTasks()
        {
            return Read<EmployeeTask>(DataType.EmployeeTask);
        }

        public static void SaveEmployee(List<Employee> employees)
        {
            Write(DataType.Employee, employees);
        }

        public static void SaveTask(List<EmployeeTask> tasks)
        {
            Write(DataType.EmployeeTask, tasks);
        }

        private static IEnumerable<T> Read<T>(DataType dataType)
        {
            try
            {
                var path = string.Empty;
                switch (dataType)
                {
                    case DataType.Employee:
                        path = EMPLOYEE_PATH;
                        break;
                    case DataType.EmployeeTask:
                        path = TASK_PATH;
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

        private static void Write<T>(DataType dataType, IEnumerable<T> data)
        {
            try
            {
                var path = string.Empty;
                switch (dataType)
                {
                    case DataType.Employee:
                        path = EMPLOYEE_PATH;
                        break;
                    case DataType.EmployeeTask:
                        path = TASK_PATH;
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
