using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ITNews.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Photo { get; set; }
        List<Comment> Comments { get; set; }
    }
}
