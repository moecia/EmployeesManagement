using EmployeeManagement.Data.DataContext;
using EmployeeManagement.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Endpoints.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {

        private readonly EmployeeMgmtContext _context;

        public EmployeesController(EmployeeMgmtContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/Employees/{e}")]
        public ActionResult Get(Employee e)
        {
            var result = _context.Employees.Get(e);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("/Employees")]
        public ActionResult Get()
        {
            return Ok(_context.Employees.GetAll());
        }

        [HttpPut]
        public ActionResult Put(Employee e)
        {
            _context.Employees.Update(e);
            _context.Employees.SaveContext();
            return Ok(e);
        }

        [HttpPost]
        public ActionResult Post(Employee e)
        {
            _context.Employees.Add(e);
            _context.Employees.SaveContext();
            return Ok(e);
        }

        [HttpDelete]
        public ActionResult Delete(Employee e)
        {
            _context.Employees.Delete(e);
            _context.Employees.SaveContext();
            return Ok(e);
        }
    }
}
