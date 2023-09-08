using Enities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECOM.API.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(int ProductId);
        Task<Product> AddProduct(Product Product);
        Task<Product> UpdateProduct(Product Product);
        void DeleteProduct(int ProductId);
    }
}
