using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;


namespace ECOM.Web.Helpers
{
    public class CookieServices
    {
        
        public class CachedCookie
        {
            public string Name { get; set; }

            public string Value { get; set; }

            public CookieOptions Options { get; set; }

            public bool IsDeleted { get; set; }
        }

        public interface ICookieService
        {
            void Delete(string cookieName);
            T Get<T>(string cookieName, bool isBase64 = false) where T : class;
            T GetOrSet<T>(string cookieName, Func<T> setFunc, DateTimeOffset? expiry = null, bool isBase64 = false) where T : class;
            void Set<T>(string cookieName, T data, DateTimeOffset? expiry = null, bool base64Encode = false) where T : class;
            void WriteToResponse(HttpContext context);
            IConfiguration getConfg();
        }

        public class CookieService : ICookieService
        {
            public IConfiguration _confg;
            private readonly HttpContext _httpContext;
            private Dictionary<string, CachedCookie> _pendingCookies = null;

            public CookieService(IHttpContextAccessor httpContextAccessor, IConfiguration confg)
            {
                _confg = confg;
                _httpContext = httpContextAccessor.HttpContext;
                _pendingCookies = new Dictionary<string, CachedCookie>();
            }

            protected CachedCookie Add(string cookieName)
            {
                var cookie = new CachedCookie
                {
                    Name = cookieName
                };
                _pendingCookies.Add(cookieName, cookie);

                return cookie;
            }

            void ICookieService.Delete(string cookieName)
            {
                Delete(cookieName);
            }

            protected CachedCookie Delete(string cookieName)
            {
                if (_pendingCookies.TryGetValue(cookieName, out CachedCookie cookie))
                    cookie.IsDeleted = true;
                else
                {
                    cookie = new CachedCookie
                    {
                        Name = cookieName,
                        IsDeleted = true
                    };
                    _pendingCookies.Add(cookieName, cookie);
                }

                return cookie;
            }

            //public T Get<T>(string cookieName, bool isBase64 = false)
            //    where T : class
            //{
            //    //return ExceptionHandler.SwallowOnException(() =>
            //    //{
            //    if (_pendingCookies.TryGetValue(cookieName, out CachedCookie cookie))
            //    {
            //        if (cookie.IsDeleted)
            //            return default(T);

            //        return isBase64 ? Newtonsoft.Json.JsonConvert.DeserializeObject<T>(cookie.Value.FromBase64String())
            //            : Newtonsoft.Json.JsonConvert.DeserializeObject<T>(cookie.Value);
            //    }

            //    if (_httpContext.Request.Cookies.TryGetValue(cookieName, out string cookieValue))
            //        return isBase64 ? Newtonsoft.Json.JsonConvert.DeserializeObject<T>(cookieValue.FromBase64String())
            //            : Newtonsoft.Json.JsonConvert.DeserializeObject<T>(cookieValue);
            //    return SessionHelper.GetObjectFromJson<T>(_httpContext.Session, cookieName);
            //    //return default(T);
            //    //});
            //}

            //public T GetOrSet<T>(string cookieName, Func<T> setFunc, DateTimeOffset? expiry = null, bool isBase64 = false)
            //    where T : class
            //{
            //    T cookie = Get<T>(cookieName, isBase64);

            //    if (cookie != null)
            //        return cookie;

            //    T data = setFunc();
            //    Set(cookieName, data, expiry, isBase64);

            //    return data;
            //}

            //public void Set<T>(string cookieName, T data, DateTimeOffset? expiry = null, bool base64Encode = false)
            //    where T : class
            //{
            //    CookieOptions options = new CookieOptions()
            //    {
            //        Secure = _httpContext.Request.IsHttps
            //    };
            //    if (expiry.HasValue)
            //        options.Expires = expiry.Value;

            //    //if (!_pendingCookies.TryGetValue(cookieName, out CachedCookie cookie))
            //    //	cookie = Add(cookieName);
            //    string strCookieValue = base64Encode
            //            ? Newtonsoft.Json.JsonConvert.SerializeObject(data).ToBase64String()
            //            : Newtonsoft.Json.JsonConvert.SerializeObject(data);
            //    //CachedCookie cookie;
            //    //_StoredCookies.Append(cookieName, strCookieValue);
            //    if (_httpContext.Request.Cookies.ContainsKey(cookieName))
            //        _httpContext.Response.Cookies.Delete(cookieName);
            //    _httpContext.Response.Cookies.Append(cookieName, strCookieValue);
            //    SessionHelper.SetObjectAsJson(_httpContext.Session, cookieName, data);
            //}

            public void WriteToResponse(HttpContext context)
            {
                foreach (var cookie in _pendingCookies.Values)
                {
                    if (cookie.IsDeleted)
                        context.Response.Cookies.Delete(cookie.Name);
                    else
                        context.Response.Cookies.Append(cookie.Name, cookie.Value, cookie.Options);
                }
            }

            public IConfiguration getConfg()
            {
                return _confg;
            }

            public T Get<T>(string cookieName, bool isBase64 = false) where T : class
            {
                throw new NotImplementedException();
            }

            public T GetOrSet<T>(string cookieName, Func<T> setFunc, DateTimeOffset? expiry = null, bool isBase64 = false) where T : class
            {
                throw new NotImplementedException();
            }

            public void Set<T>(string cookieName, T data, DateTimeOffset? expiry = null, bool base64Encode = false) where T : class
            {
                throw new NotImplementedException();
            }
        }
    }
}
