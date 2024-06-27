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
    public class AdminItemsController : ControllerBase
    {
        private readonly AdminContext _context;

        public AdminItemsController(AdminContext context)
        {
            _context = context;
        }

        // GET: api/AdminItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminItem>>> GetAdminItems()
        {
            return await _context.AdminItems.ToListAsync();
        }

        // GET: api/AdminItems/{email}
        [HttpGet("{email}")]
        public async Task<ActionResult<AdminItem>> GetAdminItem(string email)
        {
            var adminItem = await _context.AdminItems.FindAsync(email);

            if (adminItem == null)
            {
                return NotFound();
            }

            return adminItem;
        }

        // POST: api/AdminItems/Login
        [HttpPost("Login")]
        public async Task<ActionResult<AdminItem>> LoginAdminItem(string email, string password)
        {
            var admin = await _context.AdminItems.FirstOrDefaultAsync(x => x.Email == email);
            if (admin == null)
            {
                return NotFound(new { message = "No user found with this email address." });
            }
            else
            {
                if (password == admin.Password)
                {
                    return Ok(admin);
                }
                else
                {
                    return Unauthorized(new { message = "Incorrect Password" });
                }

            }
        }

        // POST: api/AdminItems/SignUp
        [HttpPost("Signup")]
        public async Task<ActionResult<AdminItem>> SignupAdminItem(string email, string password, string name)
        {
            var admin = await _context.AdminItems.FirstOrDefaultAsync(x => x.Email == email);
            if (admin != null)
            {
                return Conflict(new { message = "Email is already in use." }); ;
            }
            else
            {
                var adminUser = new AdminItem
                {
                    Name = name,
                    Email = email,
                    Password = password
                };

                _context.AdminItems.Add(adminUser);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetAdminItem", new { email = adminUser.Email }, adminUser);

            }
        }
    }
}
