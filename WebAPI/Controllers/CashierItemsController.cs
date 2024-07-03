using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using POS_App;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashierItemsController : ControllerBase
    {
        private readonly DataContext _context;

        public CashierItemsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/CashierUsers
        [RequiredScope("Access pos-app-asjal")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CashierDTO>>> GetCashierUsers()
        {
            List<CashierEntity> users = _context.GetCashierUsers();

            List<CashierDTO> result = new List<CashierDTO>();

            foreach (var user in users)
            {
                result.Add(new CashierDTO { Name = user.Name, Email = user.Email });
            }

            return result;
        }

        //// GET: api/CashierItems/{email}
        //[HttpGet("{email}")]
        //public async Task<ActionResult<CashierItem>> GetCashierItem(string email)
        //{
        //    var cashierItem = await _context.CashierItems.FindAsync(email);

        //    if (cashierItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return cashierItem;
        //}

        // POST: api/CashierItems/Login
        [RequiredScope("Access pos-app-asjal")]
        [HttpPost("Login")]
        public async Task<ActionResult<CashierDTO>> LoginCashierUser(string email, string password)
        {
            string cashier = await _context.LoginCashierUser(email, password);

            if (cashier == "Incorrect password!") {
                return Conflict(new { message = "Incorrect password!" });
            }
            else if (cashier == "Login failed!")
            {
                return Conflict(new { message = "Login failed" });
            }
            else if (cashier == "Login successful!")
            {
                return Ok(new { message = "Login successful!" });
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/CashierItems/SignUp
        [RequiredScope("Access pos-app-asjal")]
        [HttpPost("Signup")]
        public async Task<ActionResult<CashierDTO>> SignupCashierItem(string email, string password, string name)
        {
            var cashierEntity = new CashierEntity(name, email, password);

            _context.AddCashierUser(cashierEntity);

            return Ok("Cashier user signed up!");   
        }
    }
}
