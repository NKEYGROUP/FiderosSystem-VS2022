using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAF.Types.Objects
{
    public class NkgMessage
    {
        public string Command { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string RealName { get; set; } //Prénom Nom
        public string CountryCode { get; set; }
        public string City { get; set; }
        public string CompanyName { get; set; }
        public string HomePage { get; set; }
        public string Occupation { get; set; }
        public string Interests { get; set; }
        public DateTime? Birthday { get; set; }
        public MessageStatus MessageStatus { get; set; }
    }
    public class MessageStatus
    {
        public string Status { get; set; }
        public string Message { get; set; }

        public static MessageStatus CreateReturnMsg(string status, string message) =>
            new MessageStatus() { Status = status, Message = message };
    }

    public static class MsgStatusValue
    {
        public static readonly string Ok = "OK";
        public static readonly string Error = "ERROR";
        public static readonly string Unknown = "UNKNOWN";
    }
}
