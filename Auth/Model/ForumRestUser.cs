using Microsoft.AspNetCore.Identity;

namespace ForumAPI.Auth.Model
{
    public class ForumRestUser : IdentityUser
    {
        [PersonalData]
        public string? AdditionalInfo { get; set; }
    }
}
