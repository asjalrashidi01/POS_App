using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS_App;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductItemsController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductItemsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ProductItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryDTO>>> GetProductItems()
        {
            List<ProductEntity> products = await _context.GetProductList();

            return Ok(products);
        }

        //// GET: api/ProductItems/{name}
        //[HttpGet("{name}")]
        //public async Task<ActionResult<ProductItem>> GetProductItem(string name)
        //{
        //    var productItem = await _context.ProductItems.FindAsync(name);

        //    if (productItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return productItem;
        //}

        // POST: api/ProductItems/AddProduct
        [HttpPost("AddProduct")]
        public async Task<ActionResult<string>> AddProductItem(string name, float price, float quantity, int type, int category)
        {
            _context.AddInventoryProduct(new ProductEntity(name, price, quantity, type, category));

            return Ok(new { message = name + " has been added to the inventory." });
            
        }

        // PATCH: api/ProductItems/UpdateProduct
        [HttpPatch("UpdateProduct")]
        public async Task<ActionResult<string>> UpdateProductItem(string name, string new_name, float price, float quantity, int type, int category)
        {
            var product = await _context.UpdateInventoryProduct(name, new_name, price, quantity, type, category);

            if (product == "\n\nProduct not found. Please try again.")
            {
                return NotFound(new {message = "Product not found. Please try again."});
            }
            else if (product == "Success!")
            {
                return Ok(new { message = name + " has been updated." });
            }
            else
            {
                return Conflict(new { message = "An error occurred. Please try again." });
            }
        }

        // DELETE: api/ProductItems/DeleteProduct
        [HttpDelete("DeleteProduct")]
        public async Task<ActionResult<string>> DeleteProductItem(string name)
        {
            var product = await _context.RemoveInventoryProduct(name);
            
            if (product == "\n\nProduct not found. Please try again.")
            {
                return Conflict(new { message = "Product not found. Please try again." }); ;
            }
            else if (product == "Success!")
            {
                return Ok(new { message = name + " has been removed from the inventory." });
            }
            else
            {
                return Conflict(new { message = "An error occurred. Please try again." });
            }
        }
    }
}

