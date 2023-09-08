using ECOM.API.Models;
using Enities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECOM.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        #region Member
        private readonly MainDBContext _DbContext;
        #endregion

        #region Constructor
        public ProductRepository(MainDBContext DbContext)
        {
            _DbContext = DbContext;
        }
        #endregion
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _DbContext.Products.ToListAsync();
        }

        public async Task<Product> GetProduct(int ProductId)
        {
            return await _DbContext.Products
                .FirstOrDefaultAsync(e => e.ProductId == ProductId);
        }

        public async Task<Product> AddProduct(Product Product)
        {
            var result = await _DbContext.Products.AddAsync(Product);
            await _DbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Product> UpdateProduct(Product Product)
        {
            var result = await _DbContext.Products
                .FirstOrDefaultAsync(e => e.ProductId == Product.ProductId);

            if (result != null)
            {
                //result.ProductId = Product.ProductId
                result.ProductCode = Product.ProductCode;
                result.ProductName = Product.ProductName;
                result.ProductType = Product.ProductType;
                result.ProductDescription = Product.ProductDescription;
                result.Price = Product.Price;
                result.Quantity = Product.Quantity;
                //result.ImagePath = Product.ImagePath;
                result.CategoryId = Product.CategoryId;

                await _DbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async void DeleteProduct(int ProductId)
        {
            var result = await _DbContext.Products
                .FirstOrDefaultAsync(e => e.ProductId == ProductId);
            if (result != null)
            {
                _DbContext.Products.Remove(result);
                await _DbContext.SaveChangesAsync();
            }
        }
    }
}

