using QIQO.Business.Client.Entities;
using System.Collections.Generic;

namespace QIQO.Business.Client.Core.UI
{
    public interface IFeeScheduleEntityService
    {
        List<FeeSchedule> GetFeeSchedules(Product product);
        List<FeeSchedule> GetFeeSchedules(Account account);
    }
}
