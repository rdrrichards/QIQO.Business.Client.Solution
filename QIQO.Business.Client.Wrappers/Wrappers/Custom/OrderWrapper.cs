using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QIQO.Business.Client.Wrappers
{
    public partial class OrderWrapper
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (OrderItemCount == 0)
            {
                yield return new ValidationResult("The order must have at least one item in it",
                  new[] { nameof(OrderItemCount) });
            }
            //if (string.IsNullOrWhiteSpace(Account.AccountCode))
            //{
            //    yield return new ValidationResult("The order must have an account assocauted with it",
            //      new[] { nameof(Account.AccountCode) });
            //}

            //if (OrderKey == 0 && DeliverByDate < DateTime.Today)
            //{
            //    yield return new ValidationResult("The order entry date cannot be prior to the current date for a new order",
            //      new[] { nameof(DeliverByDate) });
            //}
            //if (OrderEntryDate > DeliverByDate)
            //{
            //    yield return new ValidationResult("The order entry date cannot be prior to the current date for a new order",
            //      new[] { nameof(DeliverByDate) });
            //}
            //if (OrderKey == 0 && DeliverByDate < DateTime.Today)
            //{
            //    yield return new ValidationResult("The order entry date cannot be prior to the current date for a new order",
            //      new[] { nameof(DeliverByDate) });
            //}

            //yield return new ValidationResult("");
        }
    }
}
