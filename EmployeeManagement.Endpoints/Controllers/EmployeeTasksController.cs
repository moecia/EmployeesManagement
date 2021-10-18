using EmployeeManagement.Data.DataContext;
using EmployeeManagement.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Endpoints.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeTasksController : Controller
    {
        private readonly EmployeeMgmtContext _context;

        public EmployeeTasksController(EmployeeMgmtContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/EmployeeTasks/{t}")]
        public ActionResult Get(EmployeeTask t)
        {
            var result = _context.EmployeeTasks.Get(t);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("/EmployeeTasks")]
        public ActionResult Get()
        {
            return Ok(_context.EmployeeTasks.GetAll());
        }

        [HttpPut]
        public ActionResult Put(EmployeeTask t)
        {
            _context.EmployeeTasks.Update(t);
            _context.EmployeeTasks.SaveContext();
            return Ok(t);
        }

        [HttpPost]
        public ActionResult Post(EmployeeTask t)
        {
            _context.EmployeeTasks.Add(t);
            _context.EmployeeTasks.SaveContext();
            return Ok(t);
        }

        [HttpDelete]
        public ActionResult Delete(EmployeeTask t)
        {
            _context.EmployeeTasks.Delete(t);
            _context.EmployeeTasks.SaveContext();
            return Ok(t);
        }
    }
}
