using ECOM.API.Models;
using ECOM.API.Repositories;
using Enities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECOM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class _ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        //private readonly MainDBContext _applicationDbContext;
        public _ProductController( MainDBContext applicationDbContext,IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<ActionResult> GetAllProducts()
        {
            try
            {
                return Ok(await _productRepository.GetProducts());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        [Route("GetProductById")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            try
            {
                var result = await _productRepository.GetProduct(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpPost]
        [Route("CreateProduct")]
        public async Task<ActionResult<Product>> CreateProduct(Product Product)
        {
            try
            {
                if (Product == null)
                    return BadRequest();

                var createdProduct = await _productRepository.AddProduct(Product);

                return CreatedAtAction(nameof(GetProductById),
                    new { id = createdProduct.ProductId }, createdProduct);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Product record");
            }
        }
    }
}
