using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QIQO.Business.Client.Wrappers
{
    public partial class ContactWrapper
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(ContactValue))
            {
                yield return new ValidationResult("A contact requires a value",
                  new[] { nameof(ContactValue) });
            }
        }
    }
}
