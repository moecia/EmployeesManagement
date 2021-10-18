using EmployeeManagement.Data.DataContext;
using EmployeeManagement.Data.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using Shouldly;

namespace EmployeeManagement.Testing
{
    public class EmployeeTest
    {
        [Fact]
        public void Add()
        {
            var context = new EmployeeMgmtContext().Employees;
            var countBeforeAdd = context.GetAll().Count;
            context.Add(new Employee
            {
                FirstName = "Test",
                LastName = "User",
                HiredDate = DateTime.Now.ToString("yyyy/MM/dd"),
                Tasks = new List<EmployeeTask>()
            });
            context.SaveContext();
            var countAfterAdd = context.GetAll().Count;
            var diff = countAfterAdd - countBeforeAdd;
            diff.ShouldBe(1);
        }

        [Fact]
        public void Delete()
        {
            var context = new EmployeeMgmtContext().Employees;
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
            var context = new EmployeeMgmtContext().Employees;
            var e = context.GetAll().FirstOrDefault();
            e.ShouldNotBe(null);
        }

        [Fact]
        public void Update()
        {
            var context = new EmployeeMgmtContext().Employees;
            var before = context.GetAll().Where(x => x.Id == 1).FirstOrDefault();
            before.FirstName = "Bingnan Test";
            context.Update(before);
            context.SaveContext();
            var after = context.GetAll().Where(x => x.Id == 1).FirstOrDefault();
            after.FirstName.ShouldBe("Bingnan Test");
        }
    }
}
