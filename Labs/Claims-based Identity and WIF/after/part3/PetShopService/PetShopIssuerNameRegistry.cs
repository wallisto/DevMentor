using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens;

namespace DM
{
    public class PetShopIssuerNameRegistry : IssuerNameRegistry
    {
        public override string GetIssuerName(SecurityToken securityToken)
        {
            X509SecurityToken x509Token = securityToken as X509SecurityToken;
            if (x509Token != null)
            {
                //Note: This piece of code is for illustrative purposes only. Validating certificates based on 
                //subject name is not a good practice.  This code should not be used as is in production.
                if (StringComparer.Ordinal.Equals(x509Token.Certificate.SubjectName.Name, "CN=PetShopSTS"))
                {
                    return x509Token.Certificate.SubjectName.Name;
                }
            }

            throw new SecurityTokenException("Untrusted issuer.");
        }
    }
}
