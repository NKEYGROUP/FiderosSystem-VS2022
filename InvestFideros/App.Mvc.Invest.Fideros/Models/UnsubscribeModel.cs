using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using App.Mvc.Invest.Fideros.Resources;

namespace App.Mvc.Invest.Fideros.Models
{
    public class UnsubscribeModel
    {
        [Required(ErrorMessageResourceName = "email_address_error_message", ErrorMessageResourceType = typeof(ResourceSubscriptionForm))]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessageResourceName = "email_address_format_error_message", ErrorMessageResourceType = typeof(ResourceSubscriptionForm))]
        [DataType(DataType.EmailAddress)]
        [Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "email_address_label")]
        public string EmailAddress { set; get; }
    }
}