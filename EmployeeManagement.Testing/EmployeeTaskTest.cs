using EmployeeManagement.Data.DataContext;
using EmployeeManagement.Data.Models;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EmployeeManagement.Testing
{
    public class EmployeeTaskTest
    {
        [Fact]
        public void Add()
        {
            var context = new EmployeeMgmtContext().EmployeeTasks;
            var countBeforeAdd = context.GetAll().Count;
            context.Add(new EmployeeTask
            {
                TaskName = "Test Task",
                StartTime = DateTime.Now.ToString("yyyy/MM/dd"),
                Deadline = DateTime.Now.AddDays(365).ToString("yyyy/MM/dd"),
            });
            context.SaveContext();
            var countAfterAdd = context.GetAll().Count;
            var diff = countAfterAdd - countBeforeAdd;
            diff.ShouldBe(1);
        }

        [Fact]
        public void Delete()
        {
            var context = new EmployeeMgmtContext().EmployeeTasks;
            var countBeforeDelete = context.GetAll().Count;
            var e = context.GetAll().Last();
            context.Delete(e);
            context.SaveContext();
            var countAfterDelete = context.GetAll().Count;
            var diff = countBeforeDelete - countAfterDelete;
            diff.ShouldBe(1);
        }

        [Fact]
        public void Get()
        {
            var context = new EmployeeMgmtContext().EmployeeTasks;
            var e = context.GetAll().FirstOrDefault();
            e.ShouldNotBe(null);
        }

        [Fact]
        public void Update()
        {
            var context = new EmployeeMgmtContext().EmployeeTasks;
            var before = context.GetAll().Where(x => x.Id == 1).FirstOrDefault();
            before.TaskName = "Bingnan Test Task";
            context.Update(before);
            context.SaveContext();
            var after = context.GetAll().Where(x => x.Id == 1).FirstOrDefault();
            after.TaskName.ShouldBe("Bingnan Test Task");
        }
    }
}
