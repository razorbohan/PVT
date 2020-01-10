using Microsoft.AspNetCore.Identity;
using System;

namespace L6_P2_4_TagHelper.Models
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
