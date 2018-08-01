using JournalResearcher.DataAccess.Data;
using JournalResearcher.DataAccess.Data.Models;
using JournalResearcher.Provider;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace JournalResearcher.Resources
{
    public class LoginTokenIssuer
    {
        public static TokenProviderOptions GetOptions()
        {

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.Config.GetSection("TokenAuthentication:SecretKey").Value));

            return new TokenProviderOptions
            {
                Path = Configuration.Config.GetSection("TokenAuthentication:TokenPath").Value,
                Audience = Configuration.Config.GetSection("TokenAuthentication:Audience").Value,
                Issuer = Configuration.Config.GetSection("TokenAuthentication:Issuer").Value,
                Expiration = TimeSpan.FromMinutes(Convert.ToInt32(Configuration.Config.GetSection("TokenAuthentication:ExpirationMinutes").Value)),
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            };
        }

        public static TokenResponseData Execute(ApplicationUser user, ApplicationDbContext db, RefreshToken refreshToken = null)
        {
            var options = GetOptions();
            var now = DateTime.UtcNow;

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUniversalTime().ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            };

            var userClaims = db.UserClaims.Where(i => i.UserId == user.Id);
            foreach (var userClaim in userClaims)
            {
                claims.Add(new Claim(userClaim.ClaimType, userClaim.ClaimValue));
            }
            var userRoles = db.UserRoles.Where(i => i.UserId == user.Id);
            foreach (var userRole in userRoles)
            {
                var role = db.Roles.Single(i => i.Id == userRole.RoleId);
                claims.Add(new Claim(TokenExtension.RoleClaimType, role.Name));
            }

            if (refreshToken == null)
            {
                refreshToken = new RefreshToken()
                {
                    UserId = user.Id,
                    Token = Guid.NewGuid().ToString("N"),
                };
                db.InsertNew(refreshToken);
            }

            refreshToken.IssuedUtc = now;
            refreshToken.ExpiresUtc = now.Add(options.Expiration);
            db.SaveChanges();

            var jwt = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: claims.ToArray(),
                notBefore: now,
                expires: now.Add(options.Expiration),
                signingCredentials: options.SigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new TokenResponseData
            {
                AccessToken = encodedJwt,
                RefreshToken = refreshToken.Token,
                ExpiresIn = (int)options.Expiration.TotalSeconds,
                Key = user.Id,
                //                userName = user.UserName,
                //                email = user.Email,
                Role = user.Role,

                //                isAdmin = claims.Any(i => i.Type == Extensions.RoleClaimType && i.Value == Extensions.AdminRole)
            };
            return response;
        }
    }
}
