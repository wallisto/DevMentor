using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Claims;

namespace DM
{
    public class PetShopClaimsAuthenticationManager : ClaimsAuthenticationManager
    {
        static Dictionary<string, int> experiencePointsMap =
                        new Dictionary<string, int>()
                        {
                          { "Alice", 50 },
                          { "Bob", 100 },
                        };

        public override IClaimsPrincipal Authenticate(string endpointUri, IClaimsPrincipal incomingPrincipal)
        {
            if (incomingPrincipal == null)
                throw new ArgumentNullException("incomingPrincipal");

            if (incomingPrincipal.Identities.Count != 1)
                throw new ArgumentException("incomingPrincipal must have exactly one identity");

            IClaimsIdentity identity = incomingPrincipal.Identities[0];
            string name = identity.Name;

            int experiencePoints = 0;
            experiencePointsMap.TryGetValue(name, out experiencePoints);

            identity.Claims.Add(new Claim(PetShopClaimTypes.ExperiencePoints, experiencePoints.ToString()));

            return incomingPrincipal;
        }
    }
}
