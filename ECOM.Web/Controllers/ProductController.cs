using AutoMapper;
using ECOM.Web.ViewModels;
using Enities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ECOM.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;
        public ProductController(ILogger<ProductController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }
        string _Uri = "https://localhost:44355/";

        public async Task<IActionResult> AllProducts()
        {
            HttpClient client = new HttpClient();
            List<Product> lstProduct = new List<Product>();
            HttpResponseMessage res = await client.GetAsync(_Uri + "api/_Product/GetAllProducts");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                lstProduct = JsonConvert.DeserializeObject<List<Product>>(result);
            }
            //ProductViewModel productViewModel = _mapper.Map<ProductViewModel>(lstProduct);
            return  View("~/Views/Product/Products.cshtml", lstProduct);
        }
        
    }
}
