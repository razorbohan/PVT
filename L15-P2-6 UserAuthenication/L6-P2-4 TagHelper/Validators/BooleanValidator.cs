using System.ComponentModel.DataAnnotations;

namespace L6_P2_4_TagHelper.Validators
{
    public class BooleanValidatorAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            return value is bool;
        }
    }
}
