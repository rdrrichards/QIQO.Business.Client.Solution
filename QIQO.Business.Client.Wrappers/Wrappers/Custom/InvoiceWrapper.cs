using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QIQO.Business.Client.Wrappers
{
    public partial class InvoiceWrapper
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (InvoiceItemCount == 0)
            {
                yield return new ValidationResult("The invoice must have at least one item in it",
                  new[] { nameof(InvoiceItemCount) });
            }
        }
    }
}
