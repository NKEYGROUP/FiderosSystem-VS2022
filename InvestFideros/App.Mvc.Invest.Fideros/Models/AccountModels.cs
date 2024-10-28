using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace App.Mvc.Invest.Fideros.Models
{

    public class UserProfile
    {
        [Display(Name = "Nom d'utilisateur")]
        public string UserName { get; set; }

        [Display(Name = "Nom")]
        public string LastName { get; set; }

        [Display(Name = "Prénom")]
        public string FirstName { get; set; }

        [Display(Name = "Adresse email")]
        public string EmailAddress { get; set; }

    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "Nom d'utilisateur")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe actuel")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nouveau mot de passe")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le nouveau mot de passe")]
        [Compare("NewPassword", ErrorMessage = "Le nouveau mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "Identifiant", Description = "Identifiant obligatoire")]
        public string Identifier { get; set; }

        [Required]
        [RegularExpression(@"^\S*$", ErrorMessage = "Les espaces ne sont pas autorisés.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe", Description = "Mot de passe obligatoire")]
        public string Password { get; set; }

        [Display(Name = "Mémoriser le mot de passe ?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterNewsletterModel
    {
        [Required (ErrorMessage = "Adresse email obligatoire.")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Format de email incorrect.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }
    }

    public class ValidateModel
    {
        [Required]
        [Display(Name = "Code activation")]
        public string ValidationCode { get; set; }

        [Display(Name = "Code parrainage (optionnel)")]
        public string SponsorCode { get; set; }

        //[Required]
        [Display(Name = "Question secrète")]
        public string PasswordQuestion { get; set; }
        
        //[Required]
        [Display(Name = "Réponse question secrète")]
        public string PasswordAnswer { get; set; }

        public string Token { get; set; }
        public string RelyingParty { get; set; }

        [Required]
        [Display(Name = "Veuillez accepter les conditions d'utilisation du site")]
        public bool IsApproved { get; set; }

    }

    public class ForgotPasswordModel
    {
        [Required]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Format de email incorrect.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Adresse email")]
        public string EmailAddress { get; set; }
    }

    public class ResetPasswordModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Code validation")]
        public string ValidationCode { get; set; }
        [Required]
        [RegularExpression(@"^\S*$", ErrorMessage = "Les espaces ne sont pas autorisés.")]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nouveau mot de passe")]
        public string Password { get; set; }
        [Required]
        [RegularExpression(@"^\S*$", ErrorMessage = "Les espaces ne sont pas autorisés.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe ")]
        [Compare("Password", ErrorMessage = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
