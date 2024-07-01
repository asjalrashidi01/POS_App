using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS_App;
using WebAPI.Models.Users;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUsersController : ControllerBase
    {
        private readonly DataContext _context;

        public AdminUsersController(DataContext context)
        {
            _context = context;
        }


        // GET: api/AdminUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminDTO>>> GetAdminUsers()
        {
            
            List<AdminEntity> users = _context.GetAdminUsers();

            List<AdminDTO> result = new List<AdminDTO>();

            if (users == null)
            {
                return NotFound();
            }
            else
            {
                foreach (var user in users)
                {
                    result.Add(new AdminDTO { Name = user.Name, Email = user.Email });
                }


                return Ok(result);
            }
        }

        //// GET: api/AdminUser/{email}
        //[HttpGet("{email}")]
        //public async Task<ActionResult<AdminDTO>> GetAdminUser(string email)
        //{
        //    var adminItem = await _context.;

        //    if (adminItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return adminItem;
        //}

        // POST: api/AdminUsers/Login
        [HttpPost("Login")]
        public async Task<ActionResult<AdminDTO>> LoginAdminUser(string email, string password)
        {
            string admin = await _context.LoginAdminUser(email, password);

            if (admin == "Incorrect password!")
            {
                return Conflict(new { message = "Incorrect password!" });
            }
            else if (admin == "Login failed!")
            {
                return Conflict(new { message = "Login failed!" });
            }
            else if (admin == "Login successful!")
            {
                return Ok(new { message = "Login successful!"});
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/AdminItems/SignUp
        [HttpPost("Signup")]
        public async Task<ActionResult<AdminDTO>> SignupAdminUser(string email, string password, string name)
        {
            var adminEntity = new AdminEntity(name, email, password);
            
            _context.AddAdminUser(adminEntity);

            return Ok("Admin user signed up!");
        }
    }
}
