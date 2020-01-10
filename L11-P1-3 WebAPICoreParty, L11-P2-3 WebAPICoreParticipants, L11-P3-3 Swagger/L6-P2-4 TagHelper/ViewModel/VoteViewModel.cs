using System.ComponentModel.DataAnnotations;
using FluentValidation;
using L6_P2_4_TagHelper.Validators;
using L6_P2_4_TagHelper.ViewModel;

namespace L6_P2_4_TagHelper.ViewModel
{
    public class VoteViewModel
    {
        public int PartyId { get; set; }

        //[Required(ErrorMessage = "Please, give us your name!")]
        //[StringLength(20, MinimumLength = 3, ErrorMessage = "Your name is too long!")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Please, give us your email!")]
        //[EmailValidator(ErrorMessage = "Wrong email")]
        public string Email { get; set; }

        //[BooleanValidator(ErrorMessage = "Must be only 'yes' or 'no'!")]
        public bool IsAttend { get; set; }

    }
}

public class VoteViewValidator : AbstractValidator<VoteViewModel>
{
    public VoteViewValidator()
    {
        RuleFor(x => x.PartyId)
            .NotNull();
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Please, give us your name!")
            .Length(3, 20).WithMessage("Your name is too short!");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Please, give us your email!")
            .EmailAddress().WithMessage("Your email in not real one!");
        RuleFor(x => x.IsAttend)
            .NotNull().WithMessage("Must be only 'yes' or 'no'!");
    }
}




