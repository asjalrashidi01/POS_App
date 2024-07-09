using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using POS_App;
using WebAPI.Models.Users;
using WebAPi.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUsersController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly AdminRepository _repository;

        public AdminUsersController(AdminRepository repository)
        {
            _repository = repository;
        }

        /* public AdminUsersController(DataContext context, CosmosClient client)
        {
            _context = context;
            _client = client;
        } */


        // GET: api/AdminUsers
        [RequiredScope("Access pos-app-asjal")]
        [HttpGet]
        public async Task<IEnumerable<AdminDTO>> GetAdminUsers()
        {

            return await _repository.GetAdminUsersAsync();
        }

        // GET: api/AdminUsers/GetById
        [RequiredScope("Access pos-app-asjal")]
        [HttpGet("GetById")]
        public async Task<ActionResult<AdminDTO>> GetAdminUser(string Id)
        {
            AdminDTO val = await _repository.GetAdminUsersAsyncByID(Id);

            if (val == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(val);
            }
        }

        // POST: api/AdminUsers/Login
        [RequiredScope("Access pos-app-asjal")]
        [HttpPost("Login")]
        public async Task<ActionResult<AdminDTO>> LoginAdminUser(string email, string password)
        {
            AdminDTO admin = await _repository.GetAdminUsersAsyncByEmail(email);

            if (admin == null)
            {
                return Conflict(new { message = "Login failed!" });
            }
            else if (admin.password == password)
            {
                return Ok(new { message = "Login successful!" });
            }
            else if (admin.password != password)
            {
                return Conflict(new { message = "Incorrect password!" });
            }
            else
            {
                return Conflict(new { message = "Login failed!" });
            }
        }

        // POST: api/AdminItems/SignUp
        [RequiredScope("Access pos-app-asjal")]
        [HttpPost("Signup")]
        public async Task<ActionResult<AdminDTO>> SignupAdminUser(string id, string email, string password, string name)
        {
            var response = await _repository.CreateAdminUsersAsync(new AdminDTO { id = id, email = email, password = password, name = name });

            if (response != null)
            {
                return Ok("Admin user signed up!");
            }
            else
            {
                return Conflict(new { message = "Id already exists" });
            }
        }
    }
}
