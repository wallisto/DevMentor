using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace DM
{
    class PetShopUserNamePasswordValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            // replace with real validation in your code
            if (StringComparer.Ordinal.Equals(userName, password))
            {
                // OK
                return;
            }

            throw new SecurityTokenValidationException("hmm, don't know who you are");
        }
    }
}
