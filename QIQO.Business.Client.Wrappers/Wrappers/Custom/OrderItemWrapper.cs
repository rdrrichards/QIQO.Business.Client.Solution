using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QIQO.Business.Client.Wrappers
{
    public partial class OrderItemWrapper
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (OrderItemQuantity < 0)
            {
                yield return new ValidationResult("The order item quantity must greater than or equal to zero (0)",
                  new[] { nameof(OrderItemQuantity) });
            }
            if (ItemPricePer < 0)
            {
                yield return new ValidationResult("The order item quantity must greater than or equal to zero (0)",
                  new[] { nameof(ItemPricePer) });
            }
            if (OrderItemStatus == Entities.QIQOOrderItemStatus.Complete && OrderItemCompleteDate == null)
            {
                yield return new ValidationResult("A completed date must be supplied if the status of the line item is complete",
                  new[] { nameof(OrderItemCompleteDate) });
            }
            if (OrderItemStatus == Entities.QIQOOrderItemStatus.Canceled && OrderItemCompleteDate == null)
            {
                yield return new ValidationResult("A completed date must be supplied if the status of the line item is canceled",
                  new[] { nameof(OrderItemCompleteDate) });
            }
        }
    }
}
