using System.ComponentModel.DataAnnotations;

namespace TestOrders.Attributes
{
    public class ValidateSortByAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var sortBy = (string)value;

            var validSortByValues = new[] { "volume", "amount" };

            if (!validSortByValues.Contains(sortBy.ToLower()))
            {
                return new ValidationResult("Invalid sortBy value. Acceptable values are 'volume' and 'amount'.");
            }

            return ValidationResult.Success;
        }
    }

}
