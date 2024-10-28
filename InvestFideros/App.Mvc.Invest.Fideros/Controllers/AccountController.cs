using App.Mvc.Invest.Fideros.HttpAggregator;
using App.Mvc.Invest.Fideros.Models;
using App.Mvc.Invest.Fideros.Utils;
using dotless.Core.Parser.Tree;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace App.Mvc.Invest.Fideros.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<GlobalController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IWebInfoApiClient _webInfoApiClient;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;
        public AccountController(IWebInfoApiClient webInfoApiClient, ILogger<GlobalController> logger, IConfiguration configuration, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor, ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _webInfoApiClient = webInfoApiClient;
            _hostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            var httpContext = _httpContextAccessor.HttpContext;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
            //var route = httpContext.Request.RouteValues;
            var tempData = _tempDataDictionaryFactory.GetTempData(httpContext);
            tempData["CONSENT_COOKIE"] = IsUserHasConsented();
            tempData["ACTION_NAME"] = httpContext.Request.RouteValues["action"].ToString();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
                ViewBag.ReturnUrl = "";
            else
                ViewBag.ReturnUrl = returnUrl;
            ViewBag.Message = "";
            ViewBag.FormAction = "Login";
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            string userInfo = "";
            try
            {
                ViewBag.Message = "";
                ViewBag.FormAction = "Login";
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "");
                    return View(model);
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            ViewBag.Message = "";
            ViewBag.FormAction = "ForgotPassword";
            ForgotPasswordModel Model = new ForgotPasswordModel();
            return View(Model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            ViewBag.Message = "";
            ViewBag.FormAction = "ForgotPassword";
            if (!ModelState.IsValid)
            {
                return View(model);
            }            
            return View(model);
        }


        private string IsUserHasConsented()
        {
            string result = bool.FalseString;
            var cookie = _httpContextAccessor.HttpContext.Request.Cookies["CONSENT_COOKIE"];
            if (cookie != null && cookie == bool.TrueString)
            { 
                result = bool.TrueString; 
            }
            return result;
        }

        /// <summary>  
        /// set the cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>  
        /// <param name="expireTime">expiration time</param>  
        private void Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);

            Response.Cookies.Append(key, value, option);
        }


    }
}