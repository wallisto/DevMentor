using System;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Claims;
using Microsoft.IdentityModel.Protocols.WSIdentity;

namespace DM
{
    public class PetShopUserNameSecurityTokenHandler : UserNameSecurityTokenHandler
    {
        public override bool CanValidateToken
        {
            get
            {
                return true;
            }
        }

        public override ClaimsIdentityCollection ValidateToken(SecurityToken token)
        {
            if (token == null)
                throw new ArgumentNullException("token");

            UserNameSecurityToken userNameToken = token as UserNameSecurityToken;
            if (userNameToken == null)
                throw new ArgumentException("The security token is not a valid username security token.", "token");

            string username = userNameToken.UserName;
            string password = userNameToken.Password;

            if (StringComparer.Ordinal.Equals(username, password))
            {
                return new ClaimsIdentityCollection(new ClaimsIdentity(new Claim(WSIdentityConstants.ClaimTypes.Name, username)));
            }

            throw new InvalidOperationException("The username/password is incorrect");
        }
    }
}
