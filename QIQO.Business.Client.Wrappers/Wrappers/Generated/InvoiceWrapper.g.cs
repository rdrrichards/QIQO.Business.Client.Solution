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
	public partial class InvoiceWrapper : ModelWrapperBase<Invoice>
	{
		public InvoiceWrapper(Invoice model) : base(model)
		{
		}

	public System.Int32 InvoiceKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 InvoiceKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(InvoiceKey));

	public bool InvoiceKeyIsChanged => GetIsChanged(nameof(InvoiceKey));

	public System.Int32 FromEntityKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 FromEntityKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(FromEntityKey));

	public bool FromEntityKeyIsChanged => GetIsChanged(nameof(FromEntityKey));

	public System.Int32 AccountKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 AccountKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(AccountKey));

	public bool AccountKeyIsChanged => GetIsChanged(nameof(AccountKey));

	public System.Int32 AccountContactKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 AccountContactKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(AccountContactKey));

	public bool AccountContactKeyIsChanged => GetIsChanged(nameof(AccountContactKey));

	public System.String InvoiceNumber
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String InvoiceNumberOriginalValue => GetOriginalValue<System.String>(nameof(InvoiceNumber));

	public bool InvoiceNumberIsChanged => GetIsChanged(nameof(InvoiceNumber));

	public System.Int32 InvoiceItemCount
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 InvoiceItemCountOriginalValue => GetOriginalValue<System.Int32>(nameof(InvoiceItemCount));

	public bool InvoiceItemCountIsChanged => GetIsChanged(nameof(InvoiceItemCount));

	public System.Decimal InvoiceValueSum
	{
		get { return GetValue<System.Decimal>(); }
		set { SetValue(value); }
	}

	public System.Decimal InvoiceValueSumOriginalValue => GetOriginalValue<System.Decimal>(nameof(InvoiceValueSum));

	public bool InvoiceValueSumIsChanged => GetIsChanged(nameof(InvoiceValueSum));

	public System.DateTime OrderEntryDate
	{
		get { return GetValue<System.DateTime>(); }
		set { SetValue(value); }
	}

	public System.DateTime OrderEntryDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(OrderEntryDate));

	public bool OrderEntryDateIsChanged => GetIsChanged(nameof(OrderEntryDate));

	public System.DateTime InvoiceEntryDate
	{
		get { return GetValue<System.DateTime>(); }
		set { SetValue(value); }
	}

	public System.DateTime InvoiceEntryDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(InvoiceEntryDate));

	public bool InvoiceEntryDateIsChanged => GetIsChanged(nameof(InvoiceEntryDate));

	public System.DateTime InvoiceStatusDate
	{
		get { return GetValue<System.DateTime>(); }
		set { SetValue(value); }
	}

	public System.DateTime InvoiceStatusDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(InvoiceStatusDate));

	public bool InvoiceStatusDateIsChanged => GetIsChanged(nameof(InvoiceStatusDate));

	public System.Nullable<System.DateTime> OrderShipDate
	{
		get { return GetValue<System.Nullable<System.DateTime>>(); }
		set { SetValue(value); }
	}

	public System.Nullable<System.DateTime> OrderShipDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(OrderShipDate));

	public bool OrderShipDateIsChanged => GetIsChanged(nameof(OrderShipDate));

	public System.Nullable<System.DateTime> InvoiceCompleteDate
	{
		get { return GetValue<System.Nullable<System.DateTime>>(); }
		set { SetValue(value); }
	}

	public System.Nullable<System.DateTime> InvoiceCompleteDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(InvoiceCompleteDate));

	public bool InvoiceCompleteDateIsChanged => GetIsChanged(nameof(InvoiceCompleteDate));

	public System.Int32 SalesRepKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 SalesRepKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(SalesRepKey));

	public bool SalesRepKeyIsChanged => GetIsChanged(nameof(SalesRepKey));

	public System.Int32 AccountRepKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 AccountRepKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(AccountRepKey));

	public bool AccountRepKeyIsChanged => GetIsChanged(nameof(AccountRepKey));

	public QIQO.Business.Client.Entities.QIQOInvoiceStatus InvoiceStatus
	{
		get { return GetValue<QIQO.Business.Client.Entities.QIQOInvoiceStatus>(); }
		set { SetValue(value); }
	}

	public QIQO.Business.Client.Entities.QIQOInvoiceStatus InvoiceStatusOriginalValue => GetOriginalValue<QIQO.Business.Client.Entities.QIQOInvoiceStatus>(nameof(InvoiceStatus));

	public bool InvoiceStatusIsChanged => GetIsChanged(nameof(InvoiceStatus));

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
 
	public AccountWrapper Account { get; private set; } 
 
	public PersonBaseWrapper InvoiceAccountContact { get; private set; } 
 
	public RepresentativeWrapper SalesRep { get; private set; } 
 
	public RepresentativeWrapper AccountRep { get; private set; } 
 
	public InvoiceStatusWrapper InvoiceStatusData { get; private set; } 
 
	public ChangeTrackingCollection<InvoiceItemWrapper> InvoiceItems { get; private set; }
 
	public ChangeTrackingCollection<CommentWrapper> Comments { get; private set; }
	
	protected override void InitializeComplexProperties(Invoice model)
	{
	  if (model.Account == null)
	  {
		throw new ArgumentException("Account cannot be null");
	  }

	  Account = new AccountWrapper(model.Account);
	  RegisterComplex(Account);

	  if (model.InvoiceAccountContact == null)
	  {
		throw new ArgumentException("InvoiceAccountContact cannot be null");
	  }

	  InvoiceAccountContact = new PersonBaseWrapper(model.InvoiceAccountContact);
	  RegisterComplex(InvoiceAccountContact);

	  if (model.SalesRep == null)
	  {
		throw new ArgumentException("SalesRep cannot be null");
	  }

	  SalesRep = new RepresentativeWrapper(model.SalesRep);
	  RegisterComplex(SalesRep);

	  if (model.AccountRep == null)
	  {
		throw new ArgumentException("AccountRep cannot be null");
	  }

	  AccountRep = new RepresentativeWrapper(model.AccountRep);
	  RegisterComplex(AccountRep);

	  if (model.InvoiceStatusData == null)
	  {
		throw new ArgumentException("InvoiceStatusData cannot be null");
	  }

	  InvoiceStatusData = new InvoiceStatusWrapper(model.InvoiceStatusData);
	  RegisterComplex(InvoiceStatusData);

	}

	protected override void InitializeCollectionProperties(Invoice model)
	{
		if (model.InvoiceItems == null)
		{
			throw new ArgumentException("InvoiceItems cannot be null");
		}
 
		InvoiceItems = new ChangeTrackingCollection<InvoiceItemWrapper>(model.InvoiceItems.Select(e => new InvoiceItemWrapper(e)));
		RegisterCollection(InvoiceItems, model.InvoiceItems);

		if (model.Comments == null)
		{
			throw new ArgumentException("Comments cannot be null");
		}
 
		Comments = new ChangeTrackingCollection<CommentWrapper>(model.Comments.Select(e => new CommentWrapper(e)));
		RegisterCollection(Comments, model.Comments);

	}
	}
}
