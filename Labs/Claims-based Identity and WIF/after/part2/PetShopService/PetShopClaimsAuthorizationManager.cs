using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Claims;

namespace DM
{
    public class PetShopClaimsAuthorizationManager : ClaimsAuthorizationManager
    {
        public override bool CheckAccess(AuthorizationContext context)
        {
            var action = context.Action.First();
            var resource = context.Resource.First();
            var identity = context.Principal.Identities[0];

            if (action.Value.Equals("BuyAnimal", StringComparison.OrdinalIgnoreCase))
            {
                var xp = (from claim in identity.Claims
                          where claim.ClaimType == PetShopClaimTypes.ExperiencePoints
                          select claim)
                         .First();

                if (int.Parse(resource.Value) > int.Parse(xp.Value))
                {
                    return false;
                }
            }

            return base.CheckAccess(context);
        }
    }
}
