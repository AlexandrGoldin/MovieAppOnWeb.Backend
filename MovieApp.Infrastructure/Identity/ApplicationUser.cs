using Microsoft.AspNetCore.Identity;
using MovieApp.Infrastructure.Interfaces;

namespace MovieApp.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, IAggregateRoot
    {
       public string? AvatarUri {  get; set; }
    }
}
