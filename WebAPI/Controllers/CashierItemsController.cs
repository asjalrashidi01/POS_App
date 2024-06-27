using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashierItemsController : ControllerBase
    {
        private readonly CashierContext _context;

        public CashierItemsController(CashierContext context)
        {
            _context = context;
        }

        // GET: api/CashierItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CashierItem>>> GetCashierItems()
        {
            return await _context.CashierItems.ToListAsync();
        }

        // GET: api/CashierItems/{email}
        [HttpGet("{email}")]
        public async Task<ActionResult<CashierItem>> GetCashierItem(string email)
        {
            var cashierItem = await _context.CashierItems.FindAsync(email);

            if (cashierItem == null)
            {
                return NotFound();
            }

            return cashierItem;
        }

        // POST: api/CashierItems/Login
        [HttpPost("Login")]
        public async Task<ActionResult<CashierItem>> LoginCashierItem(string email, string password)
        {
            var cashier = await _context.CashierItems.FirstOrDefaultAsync(x => x.Email == email);
            if (cashier == null)
            {
                return NotFound(new { message = "No user found with this email address." });
            }
            else
            {
                if (password == cashier.Password)
                {
                    return Ok(cashier);
                }
                else
                {
                    return Unauthorized(new { message = "Incorrect Password" });
                }

            }
        }

        // POST: api/CashierItems/SignUp
        [HttpPost("Signup")]
        public async Task<ActionResult<CashierItem>> SignupCashierItem(string email, string password, string name)
        {
            var cashier = await _context.CashierItems.FirstOrDefaultAsync(x => x.Email == email);
            if (cashier != null)
            {
                return Conflict(new { message = "Email is already in use." }); ;
            }
            else
            {
                var cashierUser = new CashierItem
                {
                    Name = name,
                    Email = email,
                    Password = password
                };

                _context.CashierItems.Add(cashierUser);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCashierItem", new { email = cashierUser.Email }, cashierUser);

            }
        }
    }
}
