using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Claims;
using Microsoft.IdentityModel.Configuration;
using Microsoft.IdentityModel.SecurityTokenService;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Protocols.WSIdentity;
using Microsoft.IdentityModel.Protocols.WSTrust;

namespace DM
{
    public class PetShopSTS : SecurityTokenService
    {
        public PetShopSTS(SecurityTokenServiceConfiguration config)
        :   base(config)
        {}

        protected override Scope GetScope(IClaimsPrincipal principal, RequestSecurityToken request)
        {
            if (request.AppliesTo == null)
            {
                throw new InvalidRequestException("AppliedTo not specified");
            }

            if (!request.AppliesTo.Uri.Equals(new Uri("http://localhost:9000/Services")))
            {
                throw new InvalidRequestException("Target service not supported by this STS");
            }

            return new Scope(request.AppliesTo.Uri.AbsoluteUri, base.SecurityTokenServiceConfiguration.SigningCredentials)
                        {
                            EncryptingCredentials =
                                    new X509EncryptingCredentials(GetCertificate(   StoreLocation.CurrentUser,
                                                                                    StoreName.TrustedPeople,
                                                                                    X509FindType.FindBySubjectName,
                                                                                    "PetShopService"))
                        };
        }

        // to simplify the sample, we use hard coded experience points here
        static Dictionary<string, string> experienceClassMap = new Dictionary<string, string>()
        {
            { "Alice", "Intermediate" },
            { "Bob", "Expert" },
        };

        protected override IClaimsIdentity GetOutputClaimsIdentity(IClaimsPrincipal principal, RequestSecurityToken request, Scope scope)
        {
            if (principal == null)
            {
                throw new ArgumentNullException("incomingPrincipal");
            }

            // We should have only one Identity.
            if (principal.Identities.Count != 1)
            {
                throw new ArgumentException("incomingPrincipal");
            }

            IClaimsIdentity identity = principal.Identities[0];

            string experienceClass = "Beginner";
            experienceClassMap.TryGetValue(identity.Name, out experienceClass);

            return new ClaimsIdentity(
                new Claim[] 
                {
                    new Claim(WSIdentityConstants.ClaimTypes.Name, identity.Name),
                    new Claim(PetShopClaimTypes.ExperienceClass, experienceClass)
                });
        }

        private static X509Certificate2 GetCertificate(StoreLocation storeLocation, StoreName storeName,
                                        X509FindType x509FindType, string value)
        {
            var store = new X509Store(storeName, storeLocation);
            store.Open(OpenFlags.ReadOnly);
            try
            {
                var certs = store.Certificates.Find(x509FindType, value, false);
                if (certs.Count == 0)
                    throw new ApplicationException(string.Format("Certificate not found: {0}", value));
                else if (certs.Count > 1)
                    throw new ApplicationException(string.Format("More than one certificate found: {0}", value));
                return certs[0];
            }
            finally
            {
                store.Close();
            }
        }
    }
}
