using App.Mvc.Invest.Fideros.HttpAggregator;
using App.Mvc.Invest.Fideros.Resources;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace App.Mvc.Invest.Fideros.Models
{
    public class WebContactObject
    {
        public string? Id { set; get; }
        public string? Identifier { set; get; }
        public string? Reference { set; get; }


        [Required(ErrorMessageResourceName = "title_error_message", ErrorMessageResourceType = typeof(ResourceSubscriptionForm))]
        [Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "title_label")]
        public string? Title { set; get; }

        [Required(ErrorMessageResourceName = "firstname_error_message", ErrorMessageResourceType = typeof(ResourceSubscriptionForm))]
        [Display(Name = "firstname_label", ResourceType = typeof(ResourceSubscriptionForm))]
        public string? Firstname { set; get; }
        public string? Middlename { set; get; }

        [Required(ErrorMessageResourceName = "lastname_error_message", ErrorMessageResourceType = typeof(ResourceSubscriptionForm))]
        [Display(Name = "lastname_label", ResourceType = typeof(ResourceSubscriptionForm))]
        public string? Lastname { set; get; }


        [Required(ErrorMessageResourceName = "email_address_error_message", ErrorMessageResourceType = typeof(ResourceSubscriptionForm))]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessageResourceName = "email_address_format_error_message", ErrorMessageResourceType = typeof(ResourceSubscriptionForm))]
        [DataType(DataType.EmailAddress)]
        [Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "email_address_label")]
        public string? EmailAddress { set; get; }

        [Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "phone_label")]
        public string? Phone { set; get; }

        [Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "mobile_phone_label")]
        public string? MobilePhone { set; get; }

        [Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "position_label")]
        public string? Position { set; get; }
        public string? PreferredLanguage { set; get; }

        //Company Infos 
        [Required(ErrorMessageResourceName = "company_name_error_message", ErrorMessageResourceType = typeof(ResourceSubscriptionForm))]
        [Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "company_name_label")]
        public string? CompanyName { set; get; }
        [Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "company_register_number_label")]
        public string? CompanyRegisterNumber { set; get; }
        [Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "company_holding_name_label")]
        public string? CompanyHoldingName { set; get; }
        [Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "company_legal_form_label")]
        public string? CompanyLegalForm { set; get; }
        [Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "company_type_label")]
        public string? CompanyVATNumber { set; get; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceName = "password_error_message", ErrorMessageResourceType = typeof(ResourceSubscriptionForm))]
        [StringLength(12, ErrorMessageResourceName = "password_length_message", ErrorMessageResourceType = typeof(ResourceSubscriptionForm), MinimumLength = 6)]
        [Display(Name = "password_label", ResourceType = typeof(ResourceSubscriptionForm))]
        public string? Password { set; get; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceName = "confirm_password_error_message", ErrorMessageResourceType = typeof(ResourceSubscriptionForm))]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceName = "confirm_password_compare_message", ErrorMessageResourceType = typeof(ResourceSubscriptionForm))]
        [Display(Name = "confirm_password_label", ResourceType = typeof(ResourceSubscriptionForm))]
        public string? ConfirmPassword { get; set; }


        [Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "message_label")]
        [Required(ErrorMessageResourceName = "message_error_message", ErrorMessageResourceType = typeof(ResourceSubscriptionForm))]
        [AllowHtml]
        public string? Message { set; get; }

        [Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "message_type_label")]
        [Required(ErrorMessageResourceName = "message_error_message_type", ErrorMessageResourceType = typeof(ResourceSubscriptionForm))]
        [AllowHtml]
        public string? MessageType { set; get; }



        //Address Infos

        [Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "zone_geo_label")]
        public string? CountryCode { set; get; }

        public string? State { set; get; }
        [Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "town_label")]
        public string? City { set; get; }
        [Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "address_line1_label")]
        public string? AddressLine1 { set; get; }
        [Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "address_line2_label")]
        public string? AddressLine2 { set; get; }
        [Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "address_postal_code_label")]
        public string? AddressPostalCode { set; get; }
        public string? AddressPostOfficeBox { set; get; }

        //[Required(ErrorMessageResourceName = "activity_code_error_message", ErrorMessageResourceType = typeof(ResourceSubscriptionForm))]
        [Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "activity_code_label")]
        public string? ActivityCode { set; get; }
        public string? ActivityDescription { set; get; }
        public string? SectorDescription { get; set; }
        public string? PassQuestion1 { get; set; }
        public string? PassQuestion2 { get; set; }
        public string? PassAnswer1 { get; set; }
        public string? PassAnswer2 { get; set; }
        public string? Origin { get; set; }

        //[Required(ErrorMessageResourceName = "web_site_error_message", ErrorMessageResourceType = typeof(ResourceSubscriptionForm))]
        [Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "web_site_label")]
        public string? WebSite { set; get; }

        //[Required(ErrorMessageResourceName = "sector_code_error_message", ErrorMessageResourceType = typeof(ResourceSubscriptionForm))]
        [Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "sector_code_label")]
        public string? SectorCode { set; get; }
        [Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "employee_label")]
        public Int16? Employee { set; get; }
        [Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "contact_preference_label")]
        public string? ContactPreference { set; get; }
        public string? SubscriptionType1 { get; set; }
        public string? SubscriptionType2 { get; set; }
        public string? SubscriptionType3 { get; set; }
        public string? AttachedFile1 { set; get; }
        public string? AttachedFile2 { set; get; }
        //[Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "contact_receiveinfo_label")]
        public Nullable<bool> ReceiveInfo { set; get; }
        public Nullable<bool> ReceivePartnerInfo { set; get; }
        public Nullable<bool> GiveMeCall { set; get; }
        public Nullable<bool> SendCatalog { set; get; }
        public string? CallPeriod { set; get; }
        [Required]
        [Display(ResourceType = typeof(ResourceSubscriptionForm), Name = "accept_conditions_label")]
        public bool AcceptConditions { set; get; }
        public virtual DateTime? CreationDate
        {
            get;
            set;
        }

        public virtual DateTime? ModificationDate
        {
            get;
            set;
        }

        public virtual string? ProcessingStatus
        {
            get;
            set;
        }

        public CaptchaModel? Captcha { set; get; } = new();

        [BindProperty, Display(Name = "Choose or Drag Files Here")]
        public List<UploadModel> Uploads { get; set; } = new();

        public WebContactInfo ToWebContactInfo()
        {
            var webCI = new WebContactInfo()
            {
                Id = Id,
                Reference = Reference,
                Title = Title,
                Firstname = Firstname,
                Middlename = Middlename,
                Lastname = Lastname,
                EmailAddress = EmailAddress,
                Phone = Phone,
                MobilePhone = MobilePhone,
                Position = Position,
                PreferredLanguage = PreferredLanguage,
                CompanyName = CompanyName,
                CompanyLegalForm = CompanyLegalForm,
                CompanyHoldingName = CompanyHoldingName,
                CompanyRegisterNumber = CompanyRegisterNumber,
                CompanyVATNumber = CompanyVATNumber,
                ActivityDescription = ActivityDescription,
                ActivityCode = ActivityCode,
                SectorDescription = SectorDescription,
                SectorCode = SectorCode,
                WebSite = WebSite,
                AddressLine1 = AddressLine1,
                AddressLine2 = AddressLine2,
                AddressPostalCode = AddressPostalCode,
                AddressPostOfficeBox = AddressPostOfficeBox,
                City = City,
                State = State,
                CountryCode = CountryCode,
                Employee = Employee,
                ContactPreference = ContactPreference,
                SubscriptionType1 = SubscriptionType1,
                SubscriptionType2 = SubscriptionType2,
                SubscriptionType3 = SubscriptionType3,
                ReceiveInfo = ReceiveInfo,
                ReceivePartnerInfo = ReceivePartnerInfo,
                GiveMeCall = GiveMeCall,
                SendCatalog = SendCatalog,
                CallPeriod = CallPeriod,
                ProcessingStatus = ProcessingStatus,
                CreationDate = CreationDate,
                Message = Message,
                Password = Password,
                Identifier = Identifier,
                PassQuestion1 = PassQuestion1,
                PassQuestion2 = PassQuestion2,
                PassAnswer1 = PassAnswer1,
                PassAnswer2 = PassAnswer2,
                Origin = Origin,
                MessageType = MessageType
            };

            return webCI;
        }
    }

    public class UploadModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
    }
}