using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ltl_codeplatform.Services
{
    public class JwtService
    {
        private readonly string _signinKey;
        public JwtService(string signinKey)
        {
            _signinKey = signinKey;
        }
        public JwtSecurityToken GenerateToken(string name,string issuer, DateTime experation, ICollection<Claim> additionalClaims)
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, name),
                // This guarantees the token is unique
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var claimList = new List<Claim>(claims);
            claimList.AddRange(additionalClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signinKey));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(issuer, null, claims: claimList,null,expires: experation,signingCredentials: credentials);

            return token;
        }

    }
}
