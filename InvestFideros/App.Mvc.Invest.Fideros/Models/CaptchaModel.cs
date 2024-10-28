using App.Mvc.Invest.Fideros.Resources;
using App.Mvc.Invest.Fideros.Utils;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Imaging;
using System.Security.AccessControl;
using System.Xml.Linq;

namespace App.Mvc.Invest.Fideros.Models
{
    public class CaptchaModel
    {
        public string? Code { set; get; }
        [Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "captcha_label")]
        [Required(ErrorMessageResourceName = "captcha_label", ErrorMessageResourceType = typeof(ResourceSubscriptionForm))]
        public string? CodeCtrl { set; get; }
        public string? ImgBase64 { get; set; }
        public static CaptchaModel GetCaptchaImgBase64()
        {
            CaptchaModel captcha = new CaptchaModel();
            captcha.Code = StaticFunctions.GenerateCaptchaCode();
            using (var mem = new MemoryStream())
            {
                var bmp2 = StaticFunctions.MakeCaptchaImge(captcha.Code, 24, 36, 130, 30);
                //render as Jpeg 
                bmp2.Save(mem, ImageFormat.Jpeg);
                byte[] byteImage = mem.ToArray();
                captcha.ImgBase64 = Convert.ToBase64String(byteImage); // Get Base64
            }
            return captcha;
        }
    }
}
