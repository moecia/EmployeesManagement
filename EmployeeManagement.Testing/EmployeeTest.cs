using EmployeeManagement.Data.DataContext;
using EmployeeManagement.Data.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using Shouldly;
using EmployeeManagement.Data.Common;
using EmployeeManagement.Data.Settings;

namespace EmployeeManagement.Testing
{
    public class EmployeeTest
    {
        private EmployeesRepository GetRepo()
        {
            var jsonStreamer = new JsonStreamer(new JsonLocationSettings());
            return new EmployeeMgmtContext(jsonStreamer).Employees;
        }

        [Fact]
        public void Add()
        {
            var repo = GetRepo();
            var countBeforeAdd = repo.GetAll().Count;
            repo.Add(new Employee
            {
                FirstName = "Test",
                LastName = "User",
                HiredDate = DateTime.Now.ToString("yyyy/MM/dd"),
                Tasks = new List<EmployeeTask>()
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
            before.FirstName = "Bingnan Test";
            repo.Update(before);
            repo.SaveContext();
            var after = repo.GetAll().Where(x => x.Id == 1).FirstOrDefault();
            after.FirstName.ShouldBe("Bingnan Test");
        }
    }
}
