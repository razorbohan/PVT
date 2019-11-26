using System.ComponentModel.DataAnnotations;
using L6_P2_4_TagHelper.Validators;

namespace L6_P2_4_TagHelper.ViewModel
{
    public class VoteViewModel
    {
        public int PartyId { get; set; }

        [Required(ErrorMessage = "Please, give us your name!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Your name is too long!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please, give us your email!")]
        [EmailValidator(ErrorMessage = "Wrong email")]
        public string Email { get; set; }

        [BooleanValidator(ErrorMessage = "Must be only 'yes' or 'no'!")]
        public bool IsAttend { get; set; }
    }
}