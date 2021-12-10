
using System.Security.Claims;

namespace CMS.Application.Extensions
{
    public static class ClaimPrincipleExtensions
    {
        //Bu extension method authentication olmuş kullanıcının username'minden Id'sini yakalayıp bize teslim edecek.
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        }

    
    }
}
