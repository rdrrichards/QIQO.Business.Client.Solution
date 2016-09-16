using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QIQO.Business.Client.Wrappers
{
    public partial class InvoiceItemWrapper
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (InvoiceItemQuantity < 0)
            {
                yield return new ValidationResult("The order item quantity must greater than or equal to zero (0)",
                  new[] { nameof(InvoiceItemQuantity) });
            }
            if (ItemPricePer < 0)
            {
                yield return new ValidationResult("The order item quantity must greater than or equal to zero (0)",
                  new[] { nameof(ItemPricePer) });
            }
            if (InvoiceItemStatus == Entities.QIQOInvoiceItemStatus.Complete && InvoiceItemCompleteDate == null)
            {
                yield return new ValidationResult("A completed date must be supplied if the status of the line item is complete",
                  new[] { nameof(InvoiceItemCompleteDate) });
            }
            if (InvoiceItemStatus == Entities.QIQOInvoiceItemStatus.Canceled && InvoiceItemCompleteDate == null)
            {
                yield return new ValidationResult("A completed date must be supplied if the status of the line item is canceled",
                  new[] { nameof(InvoiceItemCompleteDate) });
            }
        }
    }
}
