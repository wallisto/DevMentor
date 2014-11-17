using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Configuration;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.SecurityTokenService;

namespace DM
{
    public class PetShopSTSConfiguration : SecurityTokenServiceConfiguration
    {
        public PetShopSTSConfiguration()
            : base("urn:dm:petshop:sts")
        {
            this.SecurityTokenService = typeof(PetShopSTS);

            this.SigningCredentials = new X509SigningCredentials(GetCertificate(StoreLocation.CurrentUser,
                                                                                StoreName.TrustedPeople,
                                                                                X509FindType.FindBySubjectName,
                                                                                "PetShopSTS"));
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
