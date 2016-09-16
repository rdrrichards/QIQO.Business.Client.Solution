using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QIQO.Business.Client.Wrappers
{
    public partial class CompanyWrapper
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(CompanyCode))
            {
                yield return new ValidationResult("The company must have a code",
                  new[] { nameof(CompanyCode) });
            }

            if (string.IsNullOrWhiteSpace(CompanyName))
            {
                yield return new ValidationResult("The company must have a name",
                  new[] { nameof(CompanyName) });
            }

            if (string.IsNullOrWhiteSpace(CompanyDesc))
            {
                yield return new ValidationResult("The company must have a description",
                  new[] { nameof(CompanyDesc) });
            }
        }
    }
}
