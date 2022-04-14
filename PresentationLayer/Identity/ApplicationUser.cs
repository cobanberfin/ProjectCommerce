using Microsoft.AspNetCore.Identity;

namespace PresentationLayer.Identity
{
    public class ApplicationUser:IdentityUser
    {

        public string FullName { get; set; }
    }
}
