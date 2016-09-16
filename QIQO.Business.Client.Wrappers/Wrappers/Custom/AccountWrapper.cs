using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QIQO.Business.Client.Wrappers
{
    public partial class AccountWrapper //: ModelWrapperBase<Account>
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(AccountCode))
            {
                yield return new ValidationResult("Account code is required",
                  new[] { nameof(AccountCode) });
            }

            if (string.IsNullOrWhiteSpace(AccountName))
            {
                yield return new ValidationResult("Account name is required",
                  new[] { nameof(AccountName) });
            }


            if (string.IsNullOrWhiteSpace(AccountDesc))
            {
                yield return new ValidationResult("Account desciption is required",
                  new[] { nameof(AccountDesc) });
            }
            if (string.IsNullOrWhiteSpace(AccountDBA))
            {
                yield return new ValidationResult("Account knickname/doing business as (DBA) is required",
                  new[] { nameof(AccountDBA) });
            }

            //if (Addresses.Count == 0)
            //{
            //    yield return new ValidationResult("An account must have at least one address",
            //      new[] { nameof(Addresses) });
            //}
            //yield return new ValidationResult("");
        }
    }
}
