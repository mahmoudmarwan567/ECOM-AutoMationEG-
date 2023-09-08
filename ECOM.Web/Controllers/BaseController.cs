using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static ECOM.Web.Helpers.CookieServices;

namespace ECOM.Web.Controllers
{
    public class BaseController : Controller
    {
        protected IConfiguration _configuration;
        protected ICookieService _cookieService;
        public static double httpClientTimeOutMinutes = 10;
        public string _Uri = "https://localhost:44355/";
        public BaseController(IConfiguration configuration, ICookieService cookieService)
        {
            _configuration = configuration;
            _cookieService = cookieService;
        }
        //public HttpClient InitialWithJWT(string ConnectionName = "MainAPI")
        //{
        //    HttpClient Client = new HttpClient();
        //    Client.Timeout = TimeSpan.FromMinutes(httpClientTimeOutMinutes);
        //    string _Uri = _configuration.GetSection("ConnectionAPIs").GetSection(ConnectionName).Value;
        //    Client.BaseAddress = new Uri(_Uri);

        //    if (_cookieService != null)
        //    {
        //        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _cookieService.Get<TokenJWTModel>(PublicVariables.c_LOGGEDTOKEN).Token);
        //        Client.DefaultRequestHeaders.Add("sec_key", Utilities.Helpers.Constants.sec_key);
        //    }

        //    return Client;
        //}
        //public void SetClientForJWT(HttpClient client)
        //{
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _cookieService.Get<TokenJWTModel>(PublicVariables.c_LOGGEDTOKEN).Token);
        //    client.DefaultRequestHeaders.Add("sec_key", Utilities.Helpers.Constants.sec_key);
        //}
    }
}
