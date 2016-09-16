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
	public partial class LedgerWrapper : ModelWrapperBase<Ledger>
	{
		public LedgerWrapper(Ledger model) : base(model)
		{
		}

	public System.Int32 LedgerKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 LedgerKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(LedgerKey));

	public bool LedgerKeyIsChanged => GetIsChanged(nameof(LedgerKey));

	public System.Int32 CompanyKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 CompanyKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(CompanyKey));

	public bool CompanyKeyIsChanged => GetIsChanged(nameof(CompanyKey));

	public System.String LedgeCode
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String LedgeCodeOriginalValue => GetOriginalValue<System.String>(nameof(LedgeCode));

	public bool LedgeCodeIsChanged => GetIsChanged(nameof(LedgeCode));

	public System.String LedgeName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String LedgeNameOriginalValue => GetOriginalValue<System.String>(nameof(LedgeName));

	public bool LedgeNameIsChanged => GetIsChanged(nameof(LedgeName));

	public System.String LedgeDesc
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String LedgeDescOriginalValue => GetOriginalValue<System.String>(nameof(LedgeDesc));

	public bool LedgeDescIsChanged => GetIsChanged(nameof(LedgeDesc));

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
 
	public ChangeTrackingCollection<LedgerTxnWrapper> LedgerTxns { get; private set; }

	protected override void InitializeCollectionProperties(Ledger model)
	{
		if (model.LedgerTxns == null)
		{
			throw new ArgumentException("LedgerTxns cannot be null");
		}
 
		LedgerTxns = new ChangeTrackingCollection<LedgerTxnWrapper>(model.LedgerTxns.Select(e => new LedgerTxnWrapper(e)));
		RegisterCollection(LedgerTxns, model.LedgerTxns);

	}
	}
}
