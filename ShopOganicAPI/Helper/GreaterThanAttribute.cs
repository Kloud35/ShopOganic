using System;
using System.ComponentModel.DataAnnotations;
namespace ShopOganic.Helper
{
    public class GreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public GreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
            {
                return new ValidationResult($"Property {_comparisonProperty} not found.");
            }

            var comparisonValue = property.GetValue(validationContext.ObjectInstance);

            if (value is DateTime && comparisonValue is DateTime)
            {
                var startDateTime = (DateTime)value;
                var endDateTime = (DateTime)comparisonValue;

                if (startDateTime <= endDateTime)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }

}
