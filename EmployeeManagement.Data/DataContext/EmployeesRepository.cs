using EmployeeManagement.Data.Common;
using EmployeeManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeManagement.Data.DataContext
{
    public class EmployeesRepository
    {
        private List<Employee> _employees;
        private readonly JsonStreamer _jsonStreamer;
        public EmployeesRepository(List<Employee> employees,
            JsonStreamer jsonStreamer)
        {
            _employees = employees;
            _jsonStreamer = jsonStreamer;
        }

        public void Add(Employee e)
        {
            var newId = _employees.Max(x => x.Id) + 1;
            e.Id = newId;
            if (e.Tasks == null)
            {
                e.Tasks = new List<EmployeeTask>();
            }
            _employees.Add(e);
        }

        public void Delete(Employee e)
        {
            var dataToDelete = _employees.Where(x => x.Id == e.Id).FirstOrDefault();
            _employees.Remove(dataToDelete);
        }

        public Employee Get(Employee e)
        {
            return _employees.Where(x => x.Id == e.Id).FirstOrDefault();
        }

        public List<Employee> GetAll()
        {
            return _employees;
        }

        public void Update(Employee e)
        {
            var dataToEdit = _employees.Where(x => x.Id == e.Id).FirstOrDefault();           
            dataToEdit.FirstName = e.FirstName;
            dataToEdit.LastName = e.LastName;
            dataToEdit.HiredDate = e.HiredDate;
            dataToEdit.Tasks = e.Tasks;
        }

        public void SaveContext()
        {
            _jsonStreamer.SaveEmployee(_employees);
        }
    }
}
