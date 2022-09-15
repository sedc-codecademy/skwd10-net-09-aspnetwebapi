using Notes.Application;
using System.Security.Claims;

namespace Notes.Api
{
    public class ClaimsPrinicipalWrapper // facade pattern
        : IRolePrincipal
    {
        private readonly ClaimsPrincipal claimsPrincipal;

        public ClaimsPrinicipalWrapper(ClaimsPrincipal claimsPrincipal)
        {
            this.claimsPrincipal = claimsPrincipal;
        }

        public string Name => claimsPrincipal.FindFirstValue(ClaimTypes.Name);

        public int Id => int.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier));

        public bool IsInRole(string role)
        {
            return claimsPrincipal.IsInRole(role);
        }
    }
}
