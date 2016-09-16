//////////////////////////////////////////////////////////////////////////////////////////////////
// This is generated code! Do not alter this code because changes you make will be over written
// the next time the code is generated. If you need to change this code, do it via the T4 template
//////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Linq;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Entities;

namespace QIQO.Business.Client.Wrappers
{
	public partial class FeeScheduleWrapper : ModelWrapperBase<FeeSchedule>
	{
		public FeeScheduleWrapper(FeeSchedule model) : base(model)
		{
		}

	public System.Int32 FeeScheduleKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 FeeScheduleKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(FeeScheduleKey));

	public bool FeeScheduleKeyIsChanged => GetIsChanged(nameof(FeeScheduleKey));

	public System.Int32 CompanyKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 CompanyKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(CompanyKey));

	public bool CompanyKeyIsChanged => GetIsChanged(nameof(CompanyKey));

	public System.Int32 AccountKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 AccountKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(AccountKey));

	public bool AccountKeyIsChanged => GetIsChanged(nameof(AccountKey));

	public System.Int32 ProductKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 ProductKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(ProductKey));

	public bool ProductKeyIsChanged => GetIsChanged(nameof(ProductKey));

	public System.DateTime FeeScheduleStartDate
	{
		get { return GetValue<System.DateTime>(); }
		set { SetValue(value); }
	}

	public System.DateTime FeeScheduleStartDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(FeeScheduleStartDate));

	public bool FeeScheduleStartDateIsChanged => GetIsChanged(nameof(FeeScheduleStartDate));

	public System.DateTime FeeScheduleEndDate
	{
		get { return GetValue<System.DateTime>(); }
		set { SetValue(value); }
	}

	public System.DateTime FeeScheduleEndDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(FeeScheduleEndDate));

	public bool FeeScheduleEndDateIsChanged => GetIsChanged(nameof(FeeScheduleEndDate));

	public System.String FeeScheduleTypeCode
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String FeeScheduleTypeCodeOriginalValue => GetOriginalValue<System.String>(nameof(FeeScheduleTypeCode));

	public bool FeeScheduleTypeCodeIsChanged => GetIsChanged(nameof(FeeScheduleTypeCode));

	public System.Decimal FeeScheduleValue
	{
		get { return GetValue<System.Decimal>(); }
		set { SetValue(value); }
	}

	public System.Decimal FeeScheduleValueOriginalValue => GetOriginalValue<System.Decimal>(nameof(FeeScheduleValue));

	public bool FeeScheduleValueIsChanged => GetIsChanged(nameof(FeeScheduleValue));

	public System.String ProductDesc
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String ProductDescOriginalValue => GetOriginalValue<System.String>(nameof(ProductDesc));

	public bool ProductDescIsChanged => GetIsChanged(nameof(ProductDesc));

	public System.String ProductCode
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String ProductCodeOriginalValue => GetOriginalValue<System.String>(nameof(ProductCode));

	public bool ProductCodeIsChanged => GetIsChanged(nameof(ProductCode));

	public System.String AddedUserID
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AddedUserIDOriginalValue => GetOriginalValue<System.String>(nameof(AddedUserID));

	public bool AddedUserIDIsChanged => GetIsChanged(nameof(AddedUserID));

	public System.DateTime AddedDateTime
	{
		get { return GetValue<System.DateTime>(); }
		set { SetValue(value); }
	}

	public System.DateTime AddedDateTimeOriginalValue => GetOriginalValue<System.DateTime>(nameof(AddedDateTime));

	public bool AddedDateTimeIsChanged => GetIsChanged(nameof(AddedDateTime));

	public System.String UpdateUserID
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String UpdateUserIDOriginalValue => GetOriginalValue<System.String>(nameof(UpdateUserID));

	public bool UpdateUserIDIsChanged => GetIsChanged(nameof(UpdateUserID));

	public System.DateTime UpdateDateTime
	{
		get { return GetValue<System.DateTime>(); }
		set { SetValue(value); }
	}

	public System.DateTime UpdateDateTimeOriginalValue => GetOriginalValue<System.DateTime>(nameof(UpdateDateTime));

	public bool UpdateDateTimeIsChanged => GetIsChanged(nameof(UpdateDateTime));

	public System.String AccountName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AccountNameOriginalValue => GetOriginalValue<System.String>(nameof(AccountName));

	public bool AccountNameIsChanged => GetIsChanged(nameof(AccountName));

	public System.String AccountCode
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AccountCodeOriginalValue => GetOriginalValue<System.String>(nameof(AccountCode));

	public bool AccountCodeIsChanged => GetIsChanged(nameof(AccountCode));
	}
}
