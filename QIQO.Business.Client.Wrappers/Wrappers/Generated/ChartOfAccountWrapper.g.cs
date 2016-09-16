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
	public partial class ChartOfAccountWrapper : ModelWrapperBase<ChartOfAccount>
	{
		public ChartOfAccountWrapper(ChartOfAccount model) : base(model)
		{
		}

	public System.Int32 ChartOfAccountKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 ChartOfAccountKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(ChartOfAccountKey));

	public bool ChartOfAccountKeyIsChanged => GetIsChanged(nameof(ChartOfAccountKey));

	public System.String AccountNo
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AccountNoOriginalValue => GetOriginalValue<System.String>(nameof(AccountNo));

	public bool AccountNoIsChanged => GetIsChanged(nameof(AccountNo));

	public System.String AccountType
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AccountTypeOriginalValue => GetOriginalValue<System.String>(nameof(AccountType));

	public bool AccountTypeIsChanged => GetIsChanged(nameof(AccountType));

	public System.String AccountName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AccountNameOriginalValue => GetOriginalValue<System.String>(nameof(AccountName));

	public bool AccountNameIsChanged => GetIsChanged(nameof(AccountName));

	public System.String BalanceType
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String BalanceTypeOriginalValue => GetOriginalValue<System.String>(nameof(BalanceType));

	public bool BalanceTypeIsChanged => GetIsChanged(nameof(BalanceType));

	public System.String BankAccountFlag
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String BankAccountFlagOriginalValue => GetOriginalValue<System.String>(nameof(BankAccountFlag));

	public bool BankAccountFlagIsChanged => GetIsChanged(nameof(BankAccountFlag));

	public System.Int32 CompanyKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 CompanyKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(CompanyKey));

	public bool CompanyKeyIsChanged => GetIsChanged(nameof(CompanyKey));

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
	}
}
