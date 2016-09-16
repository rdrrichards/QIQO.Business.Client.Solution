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
	public partial class LedgerTxnWrapper : ModelWrapperBase<LedgerTxn>
	{
		public LedgerTxnWrapper(LedgerTxn model) : base(model)
		{
		}

	public System.Int32 LedgerTxnKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 LedgerTxnKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(LedgerTxnKey));

	public bool LedgerTxnKeyIsChanged => GetIsChanged(nameof(LedgerTxnKey));

	public System.Int32 LedgerKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 LedgerKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(LedgerKey));

	public bool LedgerKeyIsChanged => GetIsChanged(nameof(LedgerKey));

	public System.String Comment
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));

	public bool CommentIsChanged => GetIsChanged(nameof(Comment));

	public System.String AccountFrom
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AccountFromOriginalValue => GetOriginalValue<System.String>(nameof(AccountFrom));

	public bool AccountFromIsChanged => GetIsChanged(nameof(AccountFrom));

	public System.String AccountTo
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AccountToOriginalValue => GetOriginalValue<System.String>(nameof(AccountTo));

	public bool AccountToIsChanged => GetIsChanged(nameof(AccountTo));

	public System.DateTime EntryDate
	{
		get { return GetValue<System.DateTime>(); }
		set { SetValue(value); }
	}

	public System.DateTime EntryDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(EntryDate));

	public bool EntryDateIsChanged => GetIsChanged(nameof(EntryDate));

	public System.DateTime PostDate
	{
		get { return GetValue<System.DateTime>(); }
		set { SetValue(value); }
	}

	public System.DateTime PostDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(PostDate));

	public bool PostDateIsChanged => GetIsChanged(nameof(PostDate));

	public System.Decimal Credit
	{
		get { return GetValue<System.Decimal>(); }
		set { SetValue(value); }
	}

	public System.Decimal CreditOriginalValue => GetOriginalValue<System.Decimal>(nameof(Credit));

	public bool CreditIsChanged => GetIsChanged(nameof(Credit));

	public System.Decimal Debit
	{
		get { return GetValue<System.Decimal>(); }
		set { SetValue(value); }
	}

	public System.Decimal DebitOriginalValue => GetOriginalValue<System.Decimal>(nameof(Debit));

	public bool DebitIsChanged => GetIsChanged(nameof(Debit));

	public System.Int32 EntityKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 EntityKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(EntityKey));

	public bool EntityKeyIsChanged => GetIsChanged(nameof(EntityKey));

	public QIQO.Business.Client.Entities.QIQOEntityType EntityType
	{
		get { return GetValue<QIQO.Business.Client.Entities.QIQOEntityType>(); }
		set { SetValue(value); }
	}

	public QIQO.Business.Client.Entities.QIQOEntityType EntityTypeOriginalValue => GetOriginalValue<QIQO.Business.Client.Entities.QIQOEntityType>(nameof(EntityType));

	public bool EntityTypeIsChanged => GetIsChanged(nameof(EntityType));

	public System.Int32 LedgerTxnNum
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 LedgerTxnNumOriginalValue => GetOriginalValue<System.Int32>(nameof(LedgerTxnNum));

	public bool LedgerTxnNumIsChanged => GetIsChanged(nameof(LedgerTxnNum));

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
