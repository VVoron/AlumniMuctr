using System.ComponentModel.DataAnnotations;

namespace AlumniMuctr.CustomValidation
{
    public class FullNameAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (((string)value).Split(' ').Length < 2) 
                return false;

            return true;
        }
    }
}
