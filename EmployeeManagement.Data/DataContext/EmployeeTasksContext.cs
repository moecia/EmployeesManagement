using EmployeeManagement.Data.Common;
using EmployeeManagement.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Data.DataContext
{
    public class EmployeeTasksContext
    {
        private List<EmployeeTask> _employeeTasks;

        public EmployeeTasksContext(List<EmployeeTask> employeeTasks)
        {
            _employeeTasks = employeeTasks;
        }

        public void Add(EmployeeTask t)
        {
            var newId = _employeeTasks.Max(x => x.Id) + 1;
            t.Id = newId;
            _employeeTasks.Add(t);
        }

        public void Delete(EmployeeTask t)
        {
            var dataToDelete = _employeeTasks.Where(x => x.Id == t.Id).FirstOrDefault();
            _employeeTasks.Remove(dataToDelete);
        }

        public EmployeeTask Get(EmployeeTask t)
        {
            return _employeeTasks.Where(x => x.Id == t.Id).FirstOrDefault();
        }

        public List<EmployeeTask> GetAll()
        {
            return _employeeTasks;
        }

        public void Update(EmployeeTask t)
        {
            var dataToEdit = _employeeTasks.Where(x => x.Id == t.Id).FirstOrDefault();
            dataToEdit.TaskName = t.TaskName;
            dataToEdit.StartTime = t.StartTime;
            dataToEdit.Deadline = t.Deadline;
        }

        public void SaveContext()
        {
            JsonStreamer.SaveTask(_employeeTasks);
        }
    }
}
