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
    public class ProductItemsController : ControllerBase
    {
        private readonly ProductContext _context;

        public ProductItemsController(ProductContext context)
        {
            _context = context;
        }

        // GET: api/ProductItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductItem>>> GetProductItems()
        {
            return await _context.ProductItems.ToListAsync();
        }

        // GET: api/ProductItems/{name}
        [HttpGet("{name}")]
        public async Task<ActionResult<ProductItem>> GetProductItem(string name)
        {
            var productItem = await _context.ProductItems.FindAsync(name);

            if (productItem == null)
            {
                return NotFound();
            }

            return productItem;
        }

        // POST: api/ProductItems/AddProduct
        [HttpPost("AddProduct")]
        public async Task<ActionResult<ProductItem>> AddProductItem(string name, float price, float quantity, int type, int category)
        {
            var product = await _context.ProductItems.FirstOrDefaultAsync(x => x.Name == name);
            if (product != null)
            {
                return Conflict(new { message = "Product already exists. Update quantity." }); ;
            }
            else
            {
                var productUser = new ProductItem
                {
                    Name = name,
                    Price = price,
                    Quantity = quantity,
                    Type = type,
                    Category = category
                };

                _context.ProductItems.Add(productUser);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetProductItem", new { name = productUser.Name }, productUser);

            }
        }

        // PATCH: api/ProductItems/UpdateProduct
        [HttpPatch("UpdateProduct")]
        public async Task<ActionResult<ProductItem>> UpdateProductItem(string name, float price, float quantity, int type, int category)
        {
            var product = await _context.ProductItems.FirstOrDefaultAsync(x => x.Name == name);
            if (product == null)
            {
                return Conflict(new { message = "Product does not exist." }); ;
            }
            else
            {
                product.Name = name;
                product.Price = price;
                product.Quantity = quantity;
                product.Type = type;
                product.Category = category;
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetProductItem", new { name = product.Name }, product);

            }
        }

        // DELETE: api/ProductItems/DeleteProduct
        [HttpDelete("DeleteProduct")]
        public async Task<ActionResult<ProductItem>> DeleteProductItem(string name)
        {
            var product = await _context.ProductItems.FirstOrDefaultAsync(x => x.Name == name);
            if (product == null)
            {
                return Conflict(new { message = "Product does not exist." }); ;
            }
            else
            {
                _context.ProductItems.Remove(product);

                await _context.SaveChangesAsync();

                return NoContent();

            }
        }
    }
}

