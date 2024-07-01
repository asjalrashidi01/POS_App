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
    public class SaleItemsController : ControllerBase
    {
        private readonly DataContext _context;

        public SaleItemsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/SaleItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDTO>>> GetSaleItems()
        {
            List<SaleEntity> sales = await _context.GetSaleList();

            List<SaleDTO> result = new List<SaleDTO>();

            foreach (var sale in sales)
            {
                result.Add(new SaleDTO { Products = sale.Products, Total = sale.Total });
            }

            return Ok(result);

        }

        // POST: api/SaleItems/AddSale
        [HttpPost]
        public async Task<ActionResult<string>> AddSaleItem(List<ProductEntity> products, float total)
        {
            var sale = new SaleEntity(products, total);
            
            _context.AddSale(sale);

            return Ok(sale);
        }

        //// GET: api/SaleItems/{id}
        //[HttpGet("{id}")]
        //public async Task<ActionResult<SaleItem>> GetSaleItem(long id)
        //{
        //    var saleItem = await _context.SaleItems.FindAsync(id);

        //    if (saleItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return saleItem;
        //}

        //// GET: api/SaleItems/Total/{id}
        //[HttpGet("Total/{id}")]
        //public async Task<ActionResult<float>> GetSaleTotal(long id)
        //{
        //    var saleItem = await _context.SaleItems.FindAsync(id);

        //    if (saleItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return saleItem.Total;
        //}
    }
}
