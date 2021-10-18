using EmployeeManagement.Data.Common;
using EmployeeManagement.Data.DataContext;
using EmployeeManagement.Data.Models;
using EmployeeManagement.Data.Settings;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EmployeeManagement.Testing
{
    public class EmployeeTaskTest
    {
        private EmployeeTasksRepository GetRepo() 
        {
            var jsonStreamer = new JsonStreamer(new JsonLocationSettings());
            return new EmployeeMgmtContext(jsonStreamer).EmployeeTasks;
        }

        [Fact]
        public void Add()
        {
            var repo = GetRepo();
            var countBeforeAdd = repo.GetAll().Count;
            repo.Add(new EmployeeTask
            {
                TaskName = "Test Task",
                StartTime = DateTime.Now.ToString("yyyy/MM/dd"),
                Deadline = DateTime.Now.AddDays(365).ToString("yyyy/MM/dd"),
            });
            repo.SaveContext();
            var countAfterAdd = repo.GetAll().Count;
            var diff = countAfterAdd - countBeforeAdd;
            diff.ShouldBe(1);
        }

        [Fact]
        public void Delete()
        {
            var repo = GetRepo();
            var countBeforeDelete = repo.GetAll().Count;
            var e = repo.GetAll().Last();
            repo.Delete(e);
            repo.SaveContext();
            var countAfterDelete = repo.GetAll().Count;
            var diff = countBeforeDelete - countAfterDelete;
            diff.ShouldBe(1);
        }

        [Fact]
        public void Get()
        {
            var repo = GetRepo();
            var e = repo.GetAll().FirstOrDefault();
            e.ShouldNotBe(null);
        }

        [Fact]
        public void Update()
        {
            var repo = GetRepo();
            var before = repo.GetAll().Where(x => x.Id == 1).FirstOrDefault();
            before.TaskName = "Bingnan Test Task";
            repo.Update(before);
            repo.SaveContext();
            var after = repo.GetAll().Where(x => x.Id == 1).FirstOrDefault();
            after.TaskName.ShouldBe("Bingnan Test Task");
        }
    }
}
