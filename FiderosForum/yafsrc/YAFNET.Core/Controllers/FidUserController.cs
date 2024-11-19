/* FIDEROS
* Copyright (C) 2024-2025 Soliyou TIAMIOU
 * https://www.yetanotherforum.net/ 
 *  
 */

using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using ServiceStack;
using YAF.Core.Extensions;
using YAF.Types.Attributes;
using YAF.Types.Models;
using YAF.Types.Objects;

namespace YAF.Core.Controllers;

/// <summary>
/// The class that all YAF forum pages are derived from.
/// </summary>
/// 
/// <summary>
/// The User controller.
/// </summary>
[CamelCaseOutput]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/[controller]")]
[ApiController]
public class FidUserController : Controller, IHaveServiceLocator, IHaveLocalization
{
    /// <summary>
    /// The Unicode Encoder
    /// </summary>
    private readonly UnicodeEncoder unicodeEncoder;

    /// <summary>
    /// Initializes a new instance of the <see cref="ForumBaseController"/> class.
    /// </summary>
    public FidUserController()
    {
        this.Get<IInjectServices>().Inject(this);
        this.unicodeEncoder = new UnicodeEncoder();
    }

    /// <summary>
    ///   Gets the Localization.
    /// </summary>
    public ILocalization Localization => this.Get<ILocalization>();

    /// <summary>
    ///   Gets ServiceLocator.
    /// </summary>
    public IServiceLocator ServiceLocator => BoardContext.Current.ServiceLocator;

    [HttpPost("PostFidUser")]
    //[OutputCache]
    [Consumes("application/json")]
    public async Task<IActionResult> PostFidUser([FromBody] NkgMessage nkgMsg)
    {
        MessageStatus errMsg = MessageStatus.CreateReturnMsg(MsgStatusValue.Ok, "");
        if (nkgMsg == null || nkgMsg.MessageStatus == null 
            || string.IsNullOrEmpty(nkgMsg.Command) || string.IsNullOrEmpty(nkgMsg.EmailAddress) || string.IsNullOrEmpty(nkgMsg.UserName))
        {
            errMsg.Status = MsgStatusValue.Error;
            errMsg.Message = "Message is not valid.";
            return this.Ok(errMsg);
        }
        bool userit = false;
        var guidApp = this.Get<BoardSettings>().ApplicationId;
         string password = "forumFideros@NKG2024";
               
        var user = await this.Get<IAspNetUsersHelper>().ValidateUserAsync(nkgMsg.EmailAddress);
        if (user == null && nkgMsg.Command == "CREATE")
        {
            var newuser = new AspNetUsers
            {
                Id = Guid.NewGuid().ToString(),
                ApplicationId = guidApp,
                UserName = nkgMsg.UserName,
                LoweredUserName = nkgMsg.UserName.ToLower(),
                Email = nkgMsg.EmailAddress,
                LoweredEmail = nkgMsg.EmailAddress.ToLower(),
                IsApproved = true,
                EmailConfirmed = true,
                Profile_Country = nkgMsg.CountryCode,
                Profile_City = nkgMsg.City,
                Profile_RealName = nkgMsg.RealName,
                Profile_Occupation = nkgMsg.Occupation,
                Profile_Interests = nkgMsg.Interests,
                Profile_Birthday = nkgMsg.Birthday,
                Profile_Homepage = nkgMsg.HomePage,
                Profile_Company = nkgMsg.CompanyName
            };
            try
            {
                var ident = await this.Get<IAspNetUsersHelper>().CreateUserAsync(newuser, password);
                if (!ident.Succeeded)
                {
                    errMsg.Status = MsgStatusValue.Error;
                    errMsg.Message = string.Join(";", ident.Errors.Select(t => t.Code + "|" + t.Description));
                    return this.Ok(errMsg);
                }
            }
            catch (Exception ex)
            {
                var str = ex.Message;
            }
            // setup initial roles (if any) for this user
            await this.Get<IAspNetRolesHelper>().SetupUserRolesAsync(1, newuser);
            user = newuser;
            userit = true;

        }
        else if (user != null && nkgMsg.Command == "UPDATE")
        {
            user.ApplicationId = guidApp;
            user.Profile_Country = nkgMsg.CountryCode;
            user.IsApproved = true;
            user.EmailConfirmed = true;
            user.Profile_City = nkgMsg.City;
            user.Profile_RealName = nkgMsg.RealName;
            user.Profile_Occupation = nkgMsg.Occupation;
            user.Profile_Interests = nkgMsg.Interests;
            user.Profile_Birthday = nkgMsg.Birthday;
            user.Profile_Homepage = nkgMsg.HomePage;
            user.Profile_Company = nkgMsg.CompanyName;
            var ident = await this.Get<IAspNetUsersHelper>().UpdateUserAsync(user);
            if (!ident.Succeeded)
            {
                errMsg.Status = MsgStatusValue.Error;
                errMsg.Message = string.Join(";", ident.Errors.Select(t => t.Code + "|" + t.Description));
                return this.Ok(errMsg);
            }
            var code = HttpUtility.UrlEncode( await this.Get<IAspNetUsersHelper>().GeneratePasswordResetTokenAsync(user), Encoding.UTF8);
            var result = await this.Get<IAspNetUsersHelper>()
            .ResetPasswordAsync(user, HttpUtility.UrlDecode(code, Encoding.UTF8), password);
            userit = true;
        }
        if (userit)
        {
            var displayName = nkgMsg.RealName;

            // create the user in the YAF DB as well as sync roles...
            var userId = await this.Get<IAspNetRolesHelper>().CreateForumUserAsync(user, displayName, 1);
            var userForum = this.GetRepository<User>().GetById(userId.GetValueOrDefault());
            userForum.Avatar = nkgMsg.Avatar;
            this.GetRepository<User>().Update(userForum);
        }
        else
        {
            errMsg.Status = MsgStatusValue.Error;
            errMsg.Message = "Impossible de créer ce utilisateur.";
            return this.Ok(errMsg);
        }
        //else if (user != null && nkgMsg.Command == "DELETE")
        //{

        //}
        return this.Ok(errMsg);
    }

    [HttpGet("GetFidUser")]
    //[OutputCache]
    [Consumes("application/json")]
    public async Task<ActionResult<NkgMessage>> GetFidUser(string EmailAddress)
    {
        NkgMessage nkgMsg = new NkgMessage();
        nkgMsg.MessageStatus = MessageStatus.CreateReturnMsg(MsgStatusValue.Ok, "");
        if (string.IsNullOrEmpty(EmailAddress))
        {
            nkgMsg.MessageStatus = MessageStatus.CreateReturnMsg(MsgStatusValue.Error, "Email address is empty.");
            return this.Ok(nkgMsg);
        }
        var user = await this.Get<IAspNetUsersHelper>().ValidateUserAsync(EmailAddress);
        if (user != null)
        {
            nkgMsg.Command = "UPDATE";
            nkgMsg.UserName = user.UserName;
            nkgMsg.EmailAddress = EmailAddress;
            nkgMsg.RealName = user.Profile_RealName;
            nkgMsg.Avatar = "";
            nkgMsg.CompanyName = user.Profile_Homepage;
            nkgMsg.Occupation = user.Profile_Occupation;
            nkgMsg.CountryCode = user.Profile_Country;
            nkgMsg.City = user.Profile_City;
            nkgMsg.Interests = user.Profile_Interests;
            nkgMsg.Password = "";
            nkgMsg.Birthday = user.Profile_Birthday;
        }
        else
        {
            nkgMsg.Command = "CREATE";
            nkgMsg.MessageStatus = MessageStatus.CreateReturnMsg(MsgStatusValue.Error, "User does not exists.");
        }
        return this.Ok(nkgMsg);
    }

    public static string HashPassword(string password)
    {
        // Generate a 128-bit salt using a sequence of
        // cryptographically strong random bytes.
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
        // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
        return hashed;
    }
}


