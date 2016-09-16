using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QIQO.Business.Client.Wrappers
{
    public partial class ChartOfAccountWrapper
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(AccountNo))
            {
                yield return new ValidationResult("An account requires an identifying number",
                  new[] { nameof(AccountNo) });
            }

            if (string.IsNullOrWhiteSpace(AccountName))
            {
                yield return new ValidationResult("An account requires a name",
                  new[] { nameof(AccountName) });
            }

            if (string.IsNullOrWhiteSpace(AccountType))
            {
                yield return new ValidationResult("An account requires a type",
                  new[] { nameof(AccountType) });
            }

            if (string.IsNullOrWhiteSpace(BalanceType))
            {
                yield return new ValidationResult("An account requires a balance type",
                  new[] { nameof(BalanceType) });
            }
        }
    }
}
