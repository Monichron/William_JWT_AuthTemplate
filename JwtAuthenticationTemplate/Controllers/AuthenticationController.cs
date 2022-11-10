using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthenticationTemplate.Controllers
{
    
    public class User 
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
    
    public class Authentication
    {
        private JwtSecurityTokenHandler _tokenhandler;
        SqlConnectionsAndLogging _log;
        private byte[] SecretKey;
        public Authentication()
        {
            _tokenhandler = new JwtSecurityTokenHandler();
            SecretKey = Encoding.ASCII.GetBytes("Do3S_pine_4pple_ReallY_bElongOn_P1zza-Answ3r-HelloNO");
            SqlConnectionsAndLogging log = new SqlConnectionsAndLogging();
            _log = log;
        }
       
        public string GetToken(User UserInfo)
        {
            _log.logApi(DateTime.Now, "getToken", UserInfo.UserName);
            string _username = "imapirate";
            string _password = "4eva";
            if (UserInfo.UserName == _username && UserInfo.Password == _password)
            {
                string token = GenerateToken(UserInfo.UserName);
                token = token.Substring(17);
                int seperatorIndex = token.IndexOf(',');
                token = token.Substring(0, seperatorIndex);
                if (validateToken(token))
                {
                    return token;
                }
                return "failed to generate valid token";
            }
            else
            {
                return "username or password is incorrect";
            }
        }

        private dynamic GenerateToken(string username)
        {
            var Claims = new List<Claim>
         {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds().ToString())
         };
            var token = new JwtSecurityToken(
            new JwtHeader(new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Do3S_pine_4pple_ReallY_bElongOn_P1zza-Answ3r-HelloNO")), SecurityAlgorithms.HmacSha256)),
            new JwtPayload(Claims));
            var output = new { Access_Token = new JwtSecurityTokenHandler().WriteToken(token), username = username };
            return output.ToString();
        }
        public bool validateToken(string token)
        {
            if (token == "")
            {
                return false;
            }
            try
            {
                TokenValidationParameters holder = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(SecretKey),
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero

                };
                var claims = _tokenhandler.ValidateToken(token, holder,
                out SecurityToken validatedToken
               );
                return true;
            }
            catch (Exception e)
            {
                return false;

            }

        }

    }
}

