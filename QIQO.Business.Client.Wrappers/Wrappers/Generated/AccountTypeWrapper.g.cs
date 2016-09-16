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
	public partial class AccountTypeWrapper : ModelWrapperBase<AccountType>
	{
		public AccountTypeWrapper(AccountType model) : base(model)
		{
		}

	public System.Int32 AccountTypeKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 AccountTypeKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(AccountTypeKey));

	public bool AccountTypeKeyIsChanged => GetIsChanged(nameof(AccountTypeKey));

	public System.String AccountTypeCode
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AccountTypeCodeOriginalValue => GetOriginalValue<System.String>(nameof(AccountTypeCode));

	public bool AccountTypeCodeIsChanged => GetIsChanged(nameof(AccountTypeCode));

	public System.String AccountTypeName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AccountTypeNameOriginalValue => GetOriginalValue<System.String>(nameof(AccountTypeName));

	public bool AccountTypeNameIsChanged => GetIsChanged(nameof(AccountTypeName));

	public System.String AccountTypeDesc
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AccountTypeDescOriginalValue => GetOriginalValue<System.String>(nameof(AccountTypeDesc));

	public bool AccountTypeDescIsChanged => GetIsChanged(nameof(AccountTypeDesc));

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

	public System.Int32 TypeRowKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 TypeRowKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(TypeRowKey));

	public bool TypeRowKeyIsChanged => GetIsChanged(nameof(TypeRowKey));
	}
}
