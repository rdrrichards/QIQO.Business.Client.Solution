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
	public partial class OrderItemWrapper : ModelWrapperBase<OrderItem>
	{
		public OrderItemWrapper(OrderItem model) : base(model)
		{
		}

	public System.Int32 OrderItemKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 OrderItemKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(OrderItemKey));

	public bool OrderItemKeyIsChanged => GetIsChanged(nameof(OrderItemKey));

	public System.Int32 OrderKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 OrderKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(OrderKey));

	public bool OrderKeyIsChanged => GetIsChanged(nameof(OrderKey));

	public System.Int32 OrderItemSeq
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 OrderItemSeqOriginalValue => GetOriginalValue<System.Int32>(nameof(OrderItemSeq));

	public bool OrderItemSeqIsChanged => GetIsChanged(nameof(OrderItemSeq));

	public System.Int32 ProductKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 ProductKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(ProductKey));

	public bool ProductKeyIsChanged => GetIsChanged(nameof(ProductKey));

	public System.String ProductCode
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String ProductCodeOriginalValue => GetOriginalValue<System.String>(nameof(ProductCode));

	public bool ProductCodeIsChanged => GetIsChanged(nameof(ProductCode));

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

	public System.Int32 OrderItemQuantity
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 OrderItemQuantityOriginalValue => GetOriginalValue<System.Int32>(nameof(OrderItemQuantity));

	public bool OrderItemQuantityIsChanged => GetIsChanged(nameof(OrderItemQuantity));

	public System.Nullable<System.DateTime> OrderItemShipDate
	{
		get { return GetValue<System.Nullable<System.DateTime>>(); }
		set { SetValue(value); }
	}

	public System.Nullable<System.DateTime> OrderItemShipDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(OrderItemShipDate));

	public bool OrderItemShipDateIsChanged => GetIsChanged(nameof(OrderItemShipDate));

	public System.Nullable<System.DateTime> OrderItemCompleteDate
	{
		get { return GetValue<System.Nullable<System.DateTime>>(); }
		set { SetValue(value); }
	}

	public System.Nullable<System.DateTime> OrderItemCompleteDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(OrderItemCompleteDate));

	public bool OrderItemCompleteDateIsChanged => GetIsChanged(nameof(OrderItemCompleteDate));

	public System.Decimal ItemPricePer
	{
		get { return GetValue<System.Decimal>(); }
		set { SetValue(value); }
	}

	public System.Decimal ItemPricePerOriginalValue => GetOriginalValue<System.Decimal>(nameof(ItemPricePer));

	public bool ItemPricePerIsChanged => GetIsChanged(nameof(ItemPricePer));

	public System.Decimal OrderItemLineSum
	{
		get { return GetValue<System.Decimal>(); }
		set { SetValue(value); }
	}

	public System.Decimal OrderItemLineSumOriginalValue => GetOriginalValue<System.Decimal>(nameof(OrderItemLineSum));

	public bool OrderItemLineSumIsChanged => GetIsChanged(nameof(OrderItemLineSum));

	public QIQO.Business.Client.Entities.QIQOOrderItemStatus OrderItemStatus
	{
		get { return GetValue<QIQO.Business.Client.Entities.QIQOOrderItemStatus>(); }
		set { SetValue(value); }
	}

	public QIQO.Business.Client.Entities.QIQOOrderItemStatus OrderItemStatusOriginalValue => GetOriginalValue<QIQO.Business.Client.Entities.QIQOOrderItemStatus>(nameof(OrderItemStatus));

	public bool OrderItemStatusIsChanged => GetIsChanged(nameof(OrderItemStatus));

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
 
	public OrderItemStatusWrapper OrderItemStatusData { get; private set; } 
 
	public ProductWrapper OrderItemProduct { get; private set; } 
 
	public ChangeTrackingCollection<CommentWrapper> Comments { get; private set; }
	
	protected override void InitializeComplexProperties(OrderItem model)
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

	  if (model.OrderItemStatusData == null)
	  {
		throw new ArgumentException("OrderItemStatusData cannot be null");
	  }

	  OrderItemStatusData = new OrderItemStatusWrapper(model.OrderItemStatusData);
	  RegisterComplex(OrderItemStatusData);

	  if (model.OrderItemProduct == null)
	  {
		throw new ArgumentException("OrderItemProduct cannot be null");
	  }

	  OrderItemProduct = new ProductWrapper(model.OrderItemProduct);
	  RegisterComplex(OrderItemProduct);

	}

	protected override void InitializeCollectionProperties(OrderItem model)
	{
		if (model.Comments == null)
		{
			throw new ArgumentException("Comments cannot be null");
		}
 
		Comments = new ChangeTrackingCollection<CommentWrapper>(model.Comments.Select(e => new CommentWrapper(e)));
		RegisterCollection(Comments, model.Comments);

	}
	}
}
