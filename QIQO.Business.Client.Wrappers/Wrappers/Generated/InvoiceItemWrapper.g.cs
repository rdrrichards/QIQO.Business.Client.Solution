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
	public partial class InvoiceItemWrapper : ModelWrapperBase<InvoiceItem>
	{
		public InvoiceItemWrapper(InvoiceItem model) : base(model)
		{
		}

	public System.Int32 InvoiceItemKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 InvoiceItemKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(InvoiceItemKey));

	public bool InvoiceItemKeyIsChanged => GetIsChanged(nameof(InvoiceItemKey));

	public System.Int32 FromEntityKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 FromEntityKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(FromEntityKey));

	public bool FromEntityKeyIsChanged => GetIsChanged(nameof(FromEntityKey));

	public System.Int32 InvoiceKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 InvoiceKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(InvoiceKey));

	public bool InvoiceKeyIsChanged => GetIsChanged(nameof(InvoiceKey));

	public System.Int32 InvoiceItemSeq
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 InvoiceItemSeqOriginalValue => GetOriginalValue<System.Int32>(nameof(InvoiceItemSeq));

	public bool InvoiceItemSeqIsChanged => GetIsChanged(nameof(InvoiceItemSeq));

	public System.Int32 ProductKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 ProductKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(ProductKey));

	public bool ProductKeyIsChanged => GetIsChanged(nameof(ProductKey));

	public System.String ProductName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String ProductNameOriginalValue => GetOriginalValue<System.String>(nameof(ProductName));

	public bool ProductNameIsChanged => GetIsChanged(nameof(ProductName));

	public System.String ProductDesc
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String ProductDescOriginalValue => GetOriginalValue<System.String>(nameof(ProductDesc));

	public bool ProductDescIsChanged => GetIsChanged(nameof(ProductDesc));

	public System.Int32 InvoiceItemQuantity
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 InvoiceItemQuantityOriginalValue => GetOriginalValue<System.Int32>(nameof(InvoiceItemQuantity));

	public bool InvoiceItemQuantityIsChanged => GetIsChanged(nameof(InvoiceItemQuantity));

	public System.Nullable<System.DateTime> OrderItemShipDate
	{
		get { return GetValue<System.Nullable<System.DateTime>>(); }
		set { SetValue(value); }
	}

	public System.Nullable<System.DateTime> OrderItemShipDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(OrderItemShipDate));

	public bool OrderItemShipDateIsChanged => GetIsChanged(nameof(OrderItemShipDate));

	public System.Nullable<System.DateTime> InvoiceItemCompleteDate
	{
		get { return GetValue<System.Nullable<System.DateTime>>(); }
		set { SetValue(value); }
	}

	public System.Nullable<System.DateTime> InvoiceItemCompleteDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(InvoiceItemCompleteDate));

	public bool InvoiceItemCompleteDateIsChanged => GetIsChanged(nameof(InvoiceItemCompleteDate));

	public System.Decimal ItemPricePer
	{
		get { return GetValue<System.Decimal>(); }
		set { SetValue(value); }
	}

	public System.Decimal ItemPricePerOriginalValue => GetOriginalValue<System.Decimal>(nameof(ItemPricePer));

	public bool ItemPricePerIsChanged => GetIsChanged(nameof(ItemPricePer));

	public System.Decimal InvoiceItemLineSum
	{
		get { return GetValue<System.Decimal>(); }
		set { SetValue(value); }
	}

	public System.Decimal InvoiceItemLineSumOriginalValue => GetOriginalValue<System.Decimal>(nameof(InvoiceItemLineSum));

	public bool InvoiceItemLineSumIsChanged => GetIsChanged(nameof(InvoiceItemLineSum));

	public QIQO.Business.Client.Entities.QIQOInvoiceItemStatus InvoiceItemStatus
	{
		get { return GetValue<QIQO.Business.Client.Entities.QIQOInvoiceItemStatus>(); }
		set { SetValue(value); }
	}

	public QIQO.Business.Client.Entities.QIQOInvoiceItemStatus InvoiceItemStatusOriginalValue => GetOriginalValue<QIQO.Business.Client.Entities.QIQOInvoiceItemStatus>(nameof(InvoiceItemStatus));

	public bool InvoiceItemStatusIsChanged => GetIsChanged(nameof(InvoiceItemStatus));

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
 
	public AddressWrapper OrderItemShipToAddress { get; private set; } 
 
	public AddressWrapper OrderItemBillToAddress { get; private set; } 
 
	public RepresentativeWrapper SalesRep { get; private set; } 
 
	public RepresentativeWrapper AccountRep { get; private set; } 
 
	public InvoiceItemStatusWrapper InvoiceItemStatusData { get; private set; } 
 
	public ProductWrapper InvoiceItemProduct { get; private set; } 
	
	protected override void InitializeComplexProperties(InvoiceItem model)
	{
	  if (model.OrderItemShipToAddress == null)
	  {
		throw new ArgumentException("OrderItemShipToAddress cannot be null");
	  }

	  OrderItemShipToAddress = new AddressWrapper(model.OrderItemShipToAddress);
	  RegisterComplex(OrderItemShipToAddress);

	  if (model.OrderItemBillToAddress == null)
	  {
		throw new ArgumentException("OrderItemBillToAddress cannot be null");
	  }

	  OrderItemBillToAddress = new AddressWrapper(model.OrderItemBillToAddress);
	  RegisterComplex(OrderItemBillToAddress);

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

	  if (model.InvoiceItemStatusData == null)
	  {
		throw new ArgumentException("InvoiceItemStatusData cannot be null");
	  }

	  InvoiceItemStatusData = new InvoiceItemStatusWrapper(model.InvoiceItemStatusData);
	  RegisterComplex(InvoiceItemStatusData);

	  if (model.InvoiceItemProduct == null)
	  {
		throw new ArgumentException("InvoiceItemProduct cannot be null");
	  }

	  InvoiceItemProduct = new ProductWrapper(model.InvoiceItemProduct);
	  RegisterComplex(InvoiceItemProduct);

	}
	}
}
