using Ardalis.Specification;
using MovieApp.Infrastructure.Identity;

namespace MovieApp.Infrastructure.Specifications
{
    public class UserDetailsSpecification : Specification<ApplicationUser>
    {
        public UserDetailsSpecification(string? userName, string? Password)
        {
            Query.Where(user => user.UserName == userName && user.Id == Password );
        }
    }
}
