using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey) //jwt, webapinin kullanabileceği jwt'ların oluşturulabilmesi için credential (kullanıcı adı ve paralo; sisteme girmek için elinizde olanlar.)dır. burada ise bir anahtar var.
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature); 
        }
    }
}
