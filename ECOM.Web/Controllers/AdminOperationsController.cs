using Enities.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ECOM.Web.Controllers
{
    public class AdminOperationsController : Controller
    {
        string _Uri = "https://localhost:44355/";
        
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            List<Product> products = new List<Product>();
            HttpResponseMessage res = await client.GetAsync(_Uri + "api/_Product/GetAllProducts");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                products = JsonConvert.DeserializeObject<List<Product>>(result);
            }
            return View("~/Views/AdminOperations/Products.cshtml", products);
        }
        public async Task<IActionResult> AddOrEdit(int? productId)
        {
            ViewBag.PageName = productId == null ? "Create product" : "Edit product";
            ViewBag.IsEdit = productId == null ? false : true;
            if (productId == null)
            {
                return View();
            }
            else
            {
                HttpClient client = new HttpClient();
                Product Product = new Product();
                HttpResponseMessage res = await client.GetAsync(_Uri + "api/_Product/GetProductById?"+ productId);
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    Product = JsonConvert.DeserializeObject<Product>(result);
                }


                if (Product == null)
                {
                    return NotFound();
                }
                return View(Product);
            }
        }
        //public async Task<IActionResult> AddProduct(Product product)
        //{
        //    HttpClient client = new HttpClient();
        //    Product Product = new Product();
        //    HttpResponseMessage res = await client.GetAsync(_Uri + "api/_Product/CreateProduct");
        //    if (res.IsSuccessStatusCode)
        //    {
        //        var result = res.Content.ReadAsStringAsync().Result;
        //        Product = JsonConvert.DeserializeObject<Product>(result);
        //    }
        //    return View("~/Views/AdminOperations/Products.cshtml", Product);
        //}
        //[HttpPost]
        //public async Task<IActionResult> AddProduct(Product product)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        // Create the multipart form data content
        //        MultipartFormDataContent formData = new MultipartFormDataContent();

        //        // Add the product properties as string content
        //        formData.Add(new StringContent(product.ProductCode), "ProductCode");
        //        formData.Add(new StringContent(product.ProductName), "ProductName");
        //        formData.Add(new StringContent(product.ProductType), "ProductType");
        //        formData.Add(new StringContent(product.ProductDescription), "ProductDescription");
        //        formData.Add(new StringContent(product.Price.ToString()), "Price");
        //        formData.Add(new StringContent(product.Quantity.ToString()), "Quantity");
        //        formData.Add(new StringContent(product.CategoryId.ToString()), "CategoryId");

        //        // Add each image as a byte array content
        //        foreach (var imagePath in product.ImagePaths)
        //        {
        //            byte[] imageData = await System.IO.File.ReadAllBytesAsync(imagePath);
        //            ByteArrayContent imageContent = new ByteArrayContent(imageData);
        //            formData.Add(imageContent, "Images", Path.GetFileName(imagePath));
        //        }

        //        // Send the POST request to the API endpoint
        //        HttpResponseMessage response = await client.PostAsync($"{_Uri}/api/_Product/CreateProduct", formData);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("Index"); // Redirect to the desired action after successful creation
        //        }
        //        else
        //        {
        //            // Handle the failure case, e.g., display an error message
        //            ModelState.AddModelError("", "Failed to create the product.");
        //            return View("~/Views/AdminOperations/Products.cshtml", product);
        //        }
        //    }
        //}
    }
}
