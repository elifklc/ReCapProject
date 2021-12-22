using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; } //IConfiguration apimizdeki appsettings.js'i okumaya yarıyor.{ get; } read-only demek. microsoft.extension yükledik.
        private TokenOptions _tokenOptions; //TokenOptions appsettings'te okunan değerleri TokenOptions nesnesine atacağım. tokenın değerleri anlamına geliyor.
        private DateTime _accessTokenExpiration;  //token options ne zaman değersizleşecek.
        public JwtHelper(IConfiguration configuration) 
        { //config dosyasından tokenı okuyacağız.
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>(); //Configuration bunu gördüğün an appsettings.js'teki TokenOptions section'ını al ve get ile TokenOptions'ta maple.

        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration); //token ne zaman bitecek.
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey); //token optionsdaki securityKeyi okuyor. nugetten microsoft.extensions.configuration'ı indirebilirsin.
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey); //hangi anahtar(security key) ve algoyu kullanacak.
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims); //jwt üretimi çıkacak. 4 adet ihtiyaç duyduğu parametreeler var.
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler(); //bu nesneyi kullanarak elimdeki token bilgisini yazdıracağım.
            var token = jwtSecurityTokenHandler.WriteToken(jwt); //bakınız yazdırdım. stringe çevirdik writetoken ile.

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, 
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now, //expiration bilgisi şimdiden önceyse geçerli değil.
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        //claimler(roller) bizim için önemli.
        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims) //kullanıcının claimlerini oluştururken yine yardımcı bir method kullanmışım. operationClaims, dbden çektiğim claimler(roller).
        {
            //.nette bir nesneye yeni metodlar ekleyebiliyoruz. buna genişletme yani extension deniyor. core'da extension klasörüne bak! extension, genişletilen nesne demektir.
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}"); //$"..." iki stringi yanyana yazdırmak için kullanılan taktik.
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

            return claims;
        }
    }
}
