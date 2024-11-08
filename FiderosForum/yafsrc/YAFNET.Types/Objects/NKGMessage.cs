using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAF.Types.Objects
{
    public class NKGMessage
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
        public string Occupation { get; set; }
        public string Interests { get; set; }

    }
}
