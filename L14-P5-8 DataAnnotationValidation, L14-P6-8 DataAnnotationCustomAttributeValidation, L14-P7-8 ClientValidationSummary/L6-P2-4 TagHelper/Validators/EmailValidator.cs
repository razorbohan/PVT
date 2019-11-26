using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace L6_P2_4_TagHelper.Validators
{
    public class EmailValidatorAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;

            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var match = regex.Match(value?.ToString());

            return match.Success;
        }
    }
}
