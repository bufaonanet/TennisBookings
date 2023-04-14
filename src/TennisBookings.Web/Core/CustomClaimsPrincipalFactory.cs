using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TennisBookings.Web.Data;

namespace TennisBookings.Web.Core
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<TennisBookingsUser, TennisBookingsRole>
    {
        public CustomClaimsPrincipalFactory(
            UserManager<TennisBookingsUser> userManager,
            RoleManager<TennisBookingsRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(TennisBookingsUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            var fullUser = await UserManager.Users.Include("Member").SingleOrDefaultAsync(u => u.Id == user.Id);

            identity.AddClaim(new Claim("MemberId", fullUser.Member?.Id.ToString() ?? ""));
            identity.AddClaim(new Claim("MemberForename", fullUser.Member?.Forename ?? ""));
            identity.AddClaim(new Claim("MemberSurname", fullUser.Member?.Surname ?? ""));
            return identity;
        }
    }
}
