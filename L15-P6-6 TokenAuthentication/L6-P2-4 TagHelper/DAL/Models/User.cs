using Microsoft.AspNetCore.Identity;

namespace L6_P2_4_TagHelper.DAL.Models
{
    public enum Sex
    {
        Male,
        Female,
        Unknown
    }
    public class User : IdentityUser
    {

    }
}
