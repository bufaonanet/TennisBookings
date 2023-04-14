using System.Security.Claims;

namespace TennisBookings.Web.Core
{
    public static class ClaimsPrincipleExtensions
    {
        public static int GetMemberId(this ClaimsPrincipal principle)
        {
            var claim = principle.FindFirst("MemberId")?.Value;

            if (!string.IsNullOrEmpty(claim) && int.TryParse(claim, out var id))
            {
                return id;
            }

            return -1;
        }

        public static string GetMemberForename(this ClaimsPrincipal principle)
        {
            var claim = principle.FindFirst("MemberForename")?.Value;

            return !string.IsNullOrEmpty(claim) ? claim : null;
        }

        public static string GetMemberSurname(this ClaimsPrincipal principle)
        {
            var claim = principle.FindFirst("MemberSurname")?.Value;

            return !string.IsNullOrEmpty(claim) ? claim : null;
        }
    }
}
