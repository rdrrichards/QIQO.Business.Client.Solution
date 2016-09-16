using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QIQO.Business.Client.Wrappers
{
    public partial class AddressWrapper
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(AddressLine1) && (AddressType == Entities.QIQOAddressType.Billing || AddressType == Entities.QIQOAddressType.Shipping))
            {
                yield return new ValidationResult("The address must have a street number and name",
                  new[] { nameof(AddressLine1) });
            }

            if (string.IsNullOrWhiteSpace(AddressCity) && (AddressType == Entities.QIQOAddressType.Billing || AddressType == Entities.QIQOAddressType.Shipping))
            {
                yield return new ValidationResult("The address must have a city",
                  new[] { nameof(AddressCity) });
            }

            if (string.IsNullOrWhiteSpace(AddressState) && (AddressType == Entities.QIQOAddressType.Billing || AddressType == Entities.QIQOAddressType.Shipping))
            {
                yield return new ValidationResult("The address must have a state",
                  new[] { nameof(AddressState) });
            }

            if (string.IsNullOrWhiteSpace(AddressPostalCode) && (AddressType == Entities.QIQOAddressType.Billing || AddressType == Entities.QIQOAddressType.Shipping))
            {
                yield return new ValidationResult("The address must have a postal code",
                  new[] { nameof(AddressPostalCode) });
            }
        }
    }
}
