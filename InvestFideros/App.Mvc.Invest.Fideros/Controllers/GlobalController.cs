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
    public class GlobalController : Controller
    {
        private readonly ILogger<GlobalController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IWebInfoApiClient _webInfoApiClient;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;
        public GlobalController(IWebInfoApiClient webInfoApiClient, ILogger<GlobalController> logger, IConfiguration configuration, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor, ITempDataDictionaryFactory tempDataDictionaryFactory)
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
        public IActionResult Index(string viewname)
        {
            return View(viewname);
        }

        [HttpPost]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AboutFideros()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult BusinessDomains()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AuditRGPD()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Company()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Consulting()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CVSoliyouTiamiou()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Offers()
        {
            return View();
        }

        [HttpGet]
        public IActionResult References()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Reporting()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SoftwareDevelopment()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Technologies()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EMerchant()
        {
            return View();
        }

        [ActionName("k-mes")]
        [HttpGet]
        public IActionResult KMes()
        {
            return View();
        }        

        [HttpGet]
        public IActionResult Careers()
        {
            ViewBag.Message = "";
            ViewBag.FormAction = "Careers";
            return View();
        }


        // GET: /AcceptConsent
        [HttpGet]
        public ActionResult Newsletter(string ci)
        {
            return null;
        }

        [HttpPost]
        public JsonResult RegisterNewsletter(RegisterNewsletterModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.EmailAddress) || !Regex.IsMatch(model.EmailAddress, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", RegexOptions.IgnoreCase))
                {
                    return this.Json(new { Success = false });
                }
                string recipentName = "";
                string tagName = "WebKinasys";
                var wsMsgRouter = new WSMsgRouter.WSMsgRouterClient();
                wsMsgRouter.SubscribeToNewsletterAsync(tagName, model.EmailAddress, recipentName);
                return Json(new { Success = true });
            }
            catch (Exception ex)
            {
                return this.Json(new { Success = false });
            }
        }

        [HttpGet]
        public ActionResult unsubscribe()
        {
            ViewBag.Message = "";
            ViewBag.FormAction = "unsubscribe";
            ViewBag.PageControle = "Form";
            ViewBag.PageTitle = "unsubscribe";
            ViewBag.Message = "";
            UnsubscribeModel Model = new UnsubscribeModel();
            return View(Model);
        }

        [HttpPost]
        public ActionResult unsubscribe(UnsubscribeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ViewBag.PageTitle = "";
            ViewBag.FormAction = "UnSubscribe";
            ViewBag.FormID = "0";
            ViewBag.PageControle = "Error";
            
            if (!ModelState.IsValid)
                return View(model);

            var tagName = "WebKinasys";
           

            try
            {
                var wsMsgRouter = new WSMsgRouter.WSMsgRouterClient();
                wsMsgRouter.UnsubscribeEmailAsync(tagName, model.EmailAddress);
                ViewBag.Error = "Error";
                ViewBag.Message = "Votre demande a bien été prise en compte. Merci !";
                ViewBag.PageControle = "Success";
            }
            catch
            {
                ViewBag.Message = "L'adresse email à désinscrire est incorrecte ou n'existe pas dans notre base de données. Veuillez recommencer.";
                ViewBag.Error = "Error";
            }
            return View(model);
        }        
       
        [HttpGet]
        public ActionResult webcontact()
        {
            ViewBag.Message = "";
            ViewBag.FormAction = "WebContact";
            WebContactObject Model = new WebContactObject();
            Model.Password = "xxxxxxxxxxx";
            Model.ConfirmPassword = "xxxxxxxxxxx";
            Model.ReceiveInfo = true;
            Model.GiveMeCall = true;
            Model.Identifier = "xxxxxxxxxxx";
            Model.CountryCode = "FR";
            Model.Captcha = CaptchaModel.GetCaptchaImgBase64();
            return View(Model);

        }
        
        [HttpPost]
        public async Task<IActionResult> webcontact(WebContactObject model, List<IFormFile>? Uploads)
        {
            ViewBag.Message = "";
            ViewBag.FormAction = "WebContact";
            ViewBag.PageControle = "";

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View(model);
            }
            // Acceptation conditions utilisation site Web
            if (!model.AcceptConditions)
            {
                ModelState.AddModelError("AcceptConditions", "Veuillez accepter les conditions d'utilisation du site.");
                return View(model);
            }
            //validate captcha 
            if (model.Captcha.Code != model.Captcha.CodeCtrl)
            {
                ModelState.AddModelError("Captcha", "Caractères ressaisis incorrects, veuillez réessayer.");
                return View(model);
            }
            model.ProcessingStatus = "REQUEST_INFO";
            var webInfoContact = model.ToWebContactInfo();
            //Attached file
            string attachedFileName = "";
            bool hasAttachedFile = false;

            Byte[] bytes = null;
            BinaryData binData = null;
            if (Uploads != null)
            {
                foreach (var attachedFile in Uploads)
                {
                    if (attachedFile != null && attachedFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            attachedFile.OpenReadStream().CopyTo(memoryStream);
                            byte[] filebyteArr = memoryStream.ToArray();
                            binData = new BinaryData();
                            binData.StreamData = filebyteArr;
                            binData.ContentType = "application/octet-stream";
                            binData.Name = attachedFile.FileName;
                            var fileid = await _webInfoApiClient.UpLoadBinaryDataAsync(binData);
                            webInfoContact.AttachNames.Add(fileid);
                        }
                    }
                }
            }
            
            _webInfoApiClient.CreateWebContactAsync(webInfoContact);

            ViewBag.ContactTitle = model.Title;
            ViewBag.Firstname = model.Firstname;
            ViewBag.Lastname = model.Lastname;
            ViewBag.ApplicationName = _configuration.GetValue<string>("AppName"); ;
            ViewBag.EmailNotification = _configuration.GetValue<string>("EmailNotification");

            string OBAAppName = "";
            string OBAAppDescription = "Site Web FIderos Investissement";
            string OBAAppSecret = "";
            string OBAAppSLACode = "FIDEROS";

            string StrNotifAddress = ViewBag.EmailNotification;
            string StrComBody = "";
            string FromAddress = StrNotifAddress;
            string ReplyAddress = StrNotifAddress;
            string StrRecipientName = model.Title + " " + model.Firstname + " " + model.Lastname;
            string ToAddress = StrNotifAddress;
            var htmlFile = Path.Combine(_hostEnvironment.WebRootPath, "Content\\messaging\\WebContactNotification.htm");
            StrComBody = StaticFunctions.ReadFile(htmlFile);
            
            string subject = string.Format("Notification nouveau message sur {0}", OBAAppDescription);
            string title = "Notification nouveau message";
            Dictionary<string, string> dict = new Dictionary<string, string>() { { "$CONTENT-PAGE-TITLE$", title },
                    { "$CONTENT-TITLE$", title },
                    { "$CONTENT-DATA-USER-NAME$", StrRecipientName },
                    { "$CONTENT-DATA-APP-NAME$", OBAAppDescription },
                    { "$CONTENT-NOWDATE$", DateTime.Now.ToString("g") },
                    { "$CONTENT-DATA-USER-EMAIL$", model.EmailAddress},
                    { "$CONTENT-DATA-USER-PHONE$", model.Phone },
                    { "$CONTENT-DATA-USER-COUNTRY$", model.CountryCode},
                    { "$CONTENT-DATA-USER-MESSAGE-TYPE$", model.MessageType },
                    { "$CONTENT-DATA-USER-MESSAGE$", model.Message },
                    { "$CONTENT-COMPANY$", model.CompanyName } };
            StrComBody = StaticFunctions.ReplaceDataWithValues(StrComBody, dict);
            var wsClient = new WSMessaging.WSMessagingClient();
            if(binData != null )
            {
                attachedFileName = binData.Name;
                bytes = binData.StreamData.ToArray();   
            }           
            var strEmailingResult = await wsClient.SendExpressMessageAsync(subject, StrComBody,true, ToAddress, FromAddress, ReplyAddress, attachedFileName, bytes, OBAAppSLACode, "");
            _logger.LogInformation("Emailing Result : " + strEmailingResult);
            ViewBag.PageControle = "Success";
            return View(model);
        }

        [HttpGet]
        public ActionResult candidate()
        {
            ViewBag.Message = "";
            ViewBag.FormAction = "Candidate";
            WebContactObject Model = new WebContactObject();
            Model.Password = "xxxxxxxxxxx";
            Model.ConfirmPassword = "xxxxxxxxxxx";
            Model.ReceiveInfo = true;
            Model.GiveMeCall = true;
            Model.Identifier = "xxxxxxxxxxx";
            Model.CompanyName = "xxxxxxxxxxx";
            Model.MessageType = "INFOS_CANDIDATURE";
            Model.CountryCode = "FR";
            Model.Captcha = CaptchaModel.GetCaptchaImgBase64();
            return View(Model);
        }

        [HttpPost]
        public async Task<IActionResult> candidate(WebContactObject model, List<IFormFile>? Uploads)
        {
            ViewBag.Message = "";
            ViewBag.FormAction = "Candidate";
            ViewBag.PageControle = "";

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                //model.Captcha = null;
                return View(model);
            }
            // Acceptation conditions utilisation site Web
            if (!model.AcceptConditions)
            {
                ModelState.AddModelError("AcceptConditions", "Veuillez accepter les conditions d'utilisation du site.");
                return View(model);
            }
            //validate captcha 
            if (model.Captcha.Code != model.Captcha.CodeCtrl)
            {
                ModelState.AddModelError("Captcha", "Caractères ressaisis incorrects, veuillez réessayer.");
               return View(model);
            }
            model.ProcessingStatus = "REQUEST_CANDIDATE";
            var webInfoContact = model.ToWebContactInfo();
            //Attached file
            string attachedFileName = "";
            bool hasAttachedFile = false;

            Byte[] bytes = null;
            BinaryData binData = null;
            if (Uploads != null)
            {
                foreach (var attachedFile in Uploads)
                {
                    if (attachedFile != null && attachedFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            attachedFile.OpenReadStream().CopyTo(memoryStream);
                            byte[] filebyteArr = memoryStream.ToArray();
                            binData = new BinaryData();
                            binData.StreamData = filebyteArr;
                            binData.ContentType = "application/octet-stream";
                            binData.Name = attachedFile.FileName;
                            var fileid = await _webInfoApiClient.UpLoadBinaryDataAsync(binData);
                            webInfoContact.AttachNames.Add(fileid);
                        }
                    }
                }
            }

            _webInfoApiClient.CreateWebContactAsync(webInfoContact);

            ViewBag.ContactTitle = model.Title;
            ViewBag.Firstname = model.Firstname;
            ViewBag.Lastname = model.Lastname;
            ViewBag.ApplicationName = _configuration.GetValue<string>("AppName"); ;
            ViewBag.EmailNotification = _configuration.GetValue<string>("EmailNotification");

            string OBAAppName = "";
            string OBAAppDescription = "Site Web KINASYS";
            string OBAAppSecret = "";
            string OBAAppSLACode = "KINASYS";

            string StrNotifAddress = ViewBag.EmailNotification;
            string StrComBody = "";
            string FromAddress = StrNotifAddress;
            string ReplyAddress = StrNotifAddress;
            string StrRecipientName = model.Title + " " + model.Firstname + " " + model.Lastname;
            string ToAddress = StrNotifAddress;
            var htmlFile = Path.Combine(_hostEnvironment.WebRootPath, "Content\\messaging\\WebContactNotification.htm");
            StrComBody = StaticFunctions.ReadFile(htmlFile);

            string subject = string.Format("Notification nouveau message sur {0}", OBAAppDescription);
            string title = "Notification nouveau message";
            Dictionary<string, string> dict = new Dictionary<string, string>() { { "$CONTENT-PAGE-TITLE$", title },
                    { "$CONTENT-TITLE$", title },
                    { "$CONTENT-DATA-USER-NAME$", StrRecipientName },
                    { "$CONTENT-DATA-APP-NAME$", OBAAppDescription },
                    { "$CONTENT-NOWDATE$", DateTime.Now.ToString("g") },
                    { "$CONTENT-DATA-USER-EMAIL$", model.EmailAddress},
                    { "$CONTENT-DATA-USER-PHONE$", model.Phone },
                    { "$CONTENT-DATA-USER-COUNTRY$", model.CountryCode},
                    { "$CONTENT-DATA-USER-MESSAGE-TYPE$", model.MessageType },
                    { "$CONTENT-DATA-USER-MESSAGE$", model.Message },
                    { "$CONTENT-COMPANY$", model.CompanyName } };
            StrComBody = StaticFunctions.ReplaceDataWithValues(StrComBody, dict);
            var wsClient = new WSMessaging.WSMessagingClient();
            if (binData != null)
            {
                attachedFileName = binData.Name;
                bytes = binData.StreamData.ToArray();
            }
            
            var strEmailingResult = await wsClient.SendExpressMessageAsync(subject, StrComBody, true, ToAddress, FromAddress, ReplyAddress, attachedFileName, bytes, OBAAppSLACode, "");
            _logger.LogInformation("Emailing Result : " + strEmailingResult);
            ViewBag.PageControle = "Success";
            return View(model);
        }

        [ActionName("general-terms")]
        [HttpGet]
        public IActionResult GeneralTerms()
        {
            return View("general-terms");
        }

        [ActionName("legal-mention")]
        [HttpGet]
        public IActionResult LegalMention()
        {
            return View("legal-mention");
        }

        [ActionName("cookies-notice")]
        [HttpGet]
        public IActionResult CookiesNotice()
        {
            return View("cookies-notice");
        }

        [ActionName("privacy-notice")]
        [HttpGet]
        public IActionResult PrivacyNotice()
        {
            return View("privacy-notice");
        }


        [HttpGet]
        public JsonResult CookiesConsent()
        {
            Set("CONSENT_COOKIE", bool.TrueString, 129600);
            return Json(new { Success = true });
        }

        public EmptyResult EmailingTracker(string campaign, string emailid)
        {
            if (!string.IsNullOrEmpty(campaign) && !string.IsNullOrEmpty(emailid))
            {
                var wsClient = new WSMsgRouter.WSMsgRouterClient();
                wsClient.SetUserClickInfosAsync(campaign, emailid);
            }
            return new EmptyResult();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("Error/404")]
        public IActionResult Error404(int code)
        {
            var temp = _httpContextAccessor;
            return View("Error404");
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