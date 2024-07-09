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
using WebAPI.Models;
using WebAPI.Models.Users;
using WebAPi.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashierUsersController : ControllerBase
    {

        private readonly CashierRepository _repository;

        public CashierUsersController(CashierRepository repository)
        {
            _repository = repository;
        }


        // GET: api/CashierUsers
        [RequiredScope("Access pos-app-asjal")]
        [HttpGet]
        public async Task<IEnumerable<CashierDTO>> GetCashierUsers()
        {

            return await _repository.GetCashierUsersAsync();
        }

        // GET: api/CashierUsers/GetById
        [RequiredScope("Access pos-app-asjal")]
        [HttpGet("GetById")]
        public async Task<ActionResult<CashierDTO>> GetCashierUser(string Id)
        {
            CashierDTO val = await _repository.GetCashierUsersAsyncByID(Id);

            if (val == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(val);
            }
        }

        // POST: api/CashierUsers/Login
        [RequiredScope("Access pos-app-asjal")]
        [HttpPost("Login")]
        public async Task<ActionResult<CashierDTO>> LoginCashierUser(string email, string password)
        {
            CashierDTO cashier = await _repository.GetCashierUsersAsyncByEmail(email);

            if (cashier == null)
            {
                return Conflict(new { message = "Login failed!" });
            }
            else if (cashier.password == password)
            {
                return Ok(new { message = "Login successful!" });
            }
            else if (cashier.password != password)
            {
                return Conflict(new { message = "Incorrect password!" });
            }
            else
            {
                return Conflict(new { message = "Login failed!" });
            }
        }

        // POST: api/CashierItems/SignUp
        [RequiredScope("Access pos-app-asjal")]
        [HttpPost("Signup")]
        public async Task<ActionResult<CashierDTO>> SignupCashierUser(string id, string email, string password, string name)
        {
            var response = await _repository.CreateCashierUsersAsync(new CashierDTO { id = id, email = email, password = password, name = name });

            if (response != null)
            {
                return Ok("Cashier user signed up!");
            }
            else
            {
                return Conflict(new { message = "Id already exists" });
            }
        }
    }
}
