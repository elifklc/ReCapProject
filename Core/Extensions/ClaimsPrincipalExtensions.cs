using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType) //bir kişinin claimlerini ararken.net bizi biraz uğraştırıyor.jwtden gelen tokenları okumak için claimsprincipal, claimlere ulaşmak için.nette olan classtır.hangi claimtype(rol) için bulmam gerekli . ? null olaibilir anlamına gelir.
        {
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
            return result;
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }
    }
}
