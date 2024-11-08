/* FIDEROS
* Copyright (C) 2024-2025 Soliyou TIAMIOU
 * https://www.yetanotherforum.net/ 
 *  
 */

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using YAF.Types.Attributes;
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
    public async Task<IActionResult> PostFidUser([FromBody]NKGMessage nkgMsg)
    {
        ReturnMessage errMsg = ReturnMessage.CreateReturnMsg("SUCCESS", "");
        if (nkgMsg == null || string.IsNullOrEmpty(nkgMsg.Command))
        {
            errMsg = ReturnMessage.CreateReturnMsg("ERROR", "NKGMessage is missing or Command is missing.");
            return this.Ok(errMsg);            
        }
        //var user = await this.Get<IAspNetUsersHelper>().ValidateUserAsync(EmailAddress);
        //if (user == null)
        //{
        //    var guidApp = this.Get<BoardSettings>().ApplicationId;
        //    var newuser = new AspNetUsers
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        ApplicationId = guidApp,
        //        UserName = UserName,
        //        LoweredUserName = UserName.ToLower(),
        //        Email = EmailAddress,
        //        LoweredEmail = EmailAddress.ToLower(),
        //        IsApproved = true,
        //        EmailConfirmed = true,
        //        Profile_Country = CountryName,
        //        Profile_RealName = RealName,
        //        Profile_Occupation = Occupation,
        //        Profile_Birthday = null,
        //    };
        //    string password = Password;
        //    password ??= "forumFideros@NKG2024";
        //    var ident = await this.Get<IAspNetUsersHelper>().CreateUserAsync(newuser, password);
        //    if (!ident.Succeeded)
        //        errMsg = ReturnMessage.CreateReturnMsg("ERROR", string.Join(";", ident.Errors.Select(t => t.Code + "|" + t.Description)));

        //    // setup initial roles (if any) for this user
        //    await this.Get<IAspNetRolesHelper>().SetupUserRolesAsync(1, user);

        //    var displayName = RealName;

        //    // create the user in the YAF DB as well as sync roles...
        //    var userId = await this.Get<IAspNetRolesHelper>().CreateForumUserAsync(user, displayName, 1);
        //}
        //else
        //{
        //    user.Profile_Country = CountryName;
        //    user.Profile_RealName = RealName;
        //    user.Profile_Occupation = Occupation;
        //    var ident = await this.Get<IAspNetUsersHelper>().UpdateUserAsync(user);
        //    if (!ident.Succeeded)
        //        errMsg = ReturnMessage.CreateReturnMsg("ERROR", string.Join(";", ident.Errors.Select(t => t.Code + "|" + t.Description)));
        //}
        return this.Ok(errMsg);
    }

    [HttpGet("GetFidUser")]
    //[OutputCache]
    [Consumes("application/json")]
    public async Task<ActionResult<NKGMessage>> GetFidUser(string EmailAddress)
    {
        ReturnMessage errMsg = ReturnMessage.CreateReturnMsg("SUCCESS", "");
        if (string.IsNullOrEmpty(EmailAddress)) {
            errMsg = ReturnMessage.CreateReturnMsg("ERROR", "Email address is empty.");
            return this.Ok(errMsg);
        }
        NKGMessage nkgMsg = null;
        var user = await this.Get<IAspNetUsersHelper>().ValidateUserAsync(EmailAddress);
        if (user != null) {
           
            nkgMsg = new NKGMessage()
            {
                Command = "UPDATE",
                UserName = user.UserName,
                EmailAddress = EmailAddress,
                RealName = user.Profile_RealName,
                Avatar = "",
                CompanyName = user.Profile_Homepage,
                Occupation = user.Profile_Occupation,
                CountryCode = user.Profile_Country,
                City = user.Profile_City,
                Interests = user.Profile_Interests,
                Password = ""
            };
        }
        else {
           errMsg = ReturnMessage.CreateReturnMsg("ERROR", "User does not exists.");
        }
        return this.Ok(nkgMsg);
    }
}

public class ReturnMessage
{
    public string Code { get; set; }
    public string Message { get; set; }

    public static ReturnMessage CreateReturnMsg(string code,  string message) =>
        new ReturnMessage() { Code = code, Message = message };
}
