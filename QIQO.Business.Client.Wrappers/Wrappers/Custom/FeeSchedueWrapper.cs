using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QIQO.Business.Client.Wrappers
{
    public partial class FeeScheduleWrapper
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FeeScheduleValue <= 0M)
            {
                yield return new ValidationResult("A fee schedule requires a value greater than 0",
                  new[] { nameof(FeeScheduleValue) });
            }

            if (FeeScheduleStartDate == null)
            {
                yield return new ValidationResult("A fee schedule requires a start date",
                  new[] { nameof(FeeScheduleStartDate) });
            }

            if (FeeScheduleEndDate == null)
            {
                yield return new ValidationResult("A fee schedule requires a end date",
                  new[] { nameof(FeeScheduleEndDate) });
            }

            if (string.IsNullOrWhiteSpace(FeeScheduleTypeCode))
            {
                yield return new ValidationResult("A fee schedule requires a type",
                  new[] { nameof(FeeScheduleTypeCode) });
            }
        }
    }
}
