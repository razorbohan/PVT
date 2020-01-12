using System.ComponentModel.DataAnnotations;
using FluentValidation;
using L6_P2_4_TagHelper.Validators;
using L6_P2_4_TagHelper.ViewModel;
using Microsoft.AspNetCore.Http;

namespace L6_P2_4_TagHelper.ViewModel
{
    public class VoteViewModel
    {
        public int PartyId { get; set; }

        [Required(ErrorMessage = "NameRequired")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "NameLength")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "EmailRequired")]
        [EmailValidator(ErrorMessage = "EmailError")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [BooleanValidator(ErrorMessage = "IsAttendError")]
        [Display(Name = "IsAttend")]
        public bool IsAttend { get; set; }

        [Display(Name = "Photo")]
        public IFormFile Photo { get; set; }
    }
}

//public class VoteViewValidator : AbstractValidator<VoteViewModel>
//{
//    public VoteViewValidator()
//    {
//        RuleFor(x => x.PartyId)
//            .NotNull();
//        RuleFor(x => x.Name)
//            .NotEmpty().WithMessage("Please, give us your name!")
//            .Length(3, 20).WithMessage("Your name is too short!");
//        RuleFor(x => x.Email)
//            .NotEmpty().WithMessage("Please, give us your email!")
//            .EmailAddress().WithMessage("Your email in not real one!");
//        RuleFor(x => x.IsAttend)
//            .NotNull().WithMessage("Must be only 'yes' or 'no'!");
//    }
//}
