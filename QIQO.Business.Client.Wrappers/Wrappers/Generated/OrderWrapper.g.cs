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
	public partial class OrderWrapper : ModelWrapperBase<Order>
	{
		public OrderWrapper(Order model) : base(model)
		{
		}

	public System.Int32 OrderKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 OrderKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(OrderKey));

	public bool OrderKeyIsChanged => GetIsChanged(nameof(OrderKey));

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

	public System.String OrderNumber
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String OrderNumberOriginalValue => GetOriginalValue<System.String>(nameof(OrderNumber));

	public bool OrderNumberIsChanged => GetIsChanged(nameof(OrderNumber));

	public System.Int32 OrderItemCount
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 OrderItemCountOriginalValue => GetOriginalValue<System.Int32>(nameof(OrderItemCount));

	public bool OrderItemCountIsChanged => GetIsChanged(nameof(OrderItemCount));

	public System.Decimal OrderValueSum
	{
		get { return GetValue<System.Decimal>(); }
		set { SetValue(value); }
	}

	public System.Decimal OrderValueSumOriginalValue => GetOriginalValue<System.Decimal>(nameof(OrderValueSum));

	public bool OrderValueSumIsChanged => GetIsChanged(nameof(OrderValueSum));

	public System.DateTime OrderEntryDate
	{
		get { return GetValue<System.DateTime>(); }
		set { SetValue(value); }
	}

	public System.DateTime OrderEntryDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(OrderEntryDate));

	public bool OrderEntryDateIsChanged => GetIsChanged(nameof(OrderEntryDate));

	public System.DateTime OrderStatusDate
	{
		get { return GetValue<System.DateTime>(); }
		set { SetValue(value); }
	}

	public System.DateTime OrderStatusDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(OrderStatusDate));

	public bool OrderStatusDateIsChanged => GetIsChanged(nameof(OrderStatusDate));

	public System.Nullable<System.DateTime> OrderShipDate
	{
		get { return GetValue<System.Nullable<System.DateTime>>(); }
		set { SetValue(value); }
	}

	public System.Nullable<System.DateTime> OrderShipDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(OrderShipDate));

	public bool OrderShipDateIsChanged => GetIsChanged(nameof(OrderShipDate));

	public System.Nullable<System.DateTime> OrderCompleteDate
	{
		get { return GetValue<System.Nullable<System.DateTime>>(); }
		set { SetValue(value); }
	}

	public System.Nullable<System.DateTime> OrderCompleteDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(OrderCompleteDate));

	public bool OrderCompleteDateIsChanged => GetIsChanged(nameof(OrderCompleteDate));

	public System.Nullable<System.DateTime> DeliverByDate
	{
		get { return GetValue<System.Nullable<System.DateTime>>(); }
		set { SetValue(value); }
	}

	public System.Nullable<System.DateTime> DeliverByDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(DeliverByDate));

	public bool DeliverByDateIsChanged => GetIsChanged(nameof(DeliverByDate));

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

	public QIQO.Business.Client.Entities.QIQOOrderStatus OrderStatus
	{
		get { return GetValue<QIQO.Business.Client.Entities.QIQOOrderStatus>(); }
		set { SetValue(value); }
	}

	public QIQO.Business.Client.Entities.QIQOOrderStatus OrderStatusOriginalValue => GetOriginalValue<QIQO.Business.Client.Entities.QIQOOrderStatus>(nameof(OrderStatus));

	public bool OrderStatusIsChanged => GetIsChanged(nameof(OrderStatus));

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
 
	public PersonBaseWrapper OrderAccountContact { get; private set; } 
 
	public RepresentativeWrapper SalesRep { get; private set; } 
 
	public RepresentativeWrapper AccountRep { get; private set; } 
 
	public OrderStatusWrapper OrderStatusData { get; private set; } 
 
	public ChangeTrackingCollection<OrderItemWrapper> OrderItems { get; private set; }
 
	public ChangeTrackingCollection<CommentWrapper> Comments { get; private set; }
	
	protected override void InitializeComplexProperties(Order model)
	{
	  if (model.Account == null)
	  {
		throw new ArgumentException("Account cannot be null");
	  }

	  Account = new AccountWrapper(model.Account);
	  RegisterComplex(Account);

	  if (model.OrderAccountContact == null)
	  {
		throw new ArgumentException("OrderAccountContact cannot be null");
	  }

	  OrderAccountContact = new PersonBaseWrapper(model.OrderAccountContact);
	  RegisterComplex(OrderAccountContact);

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

	  if (model.OrderStatusData == null)
	  {
		throw new ArgumentException("OrderStatusData cannot be null");
	  }

	  OrderStatusData = new OrderStatusWrapper(model.OrderStatusData);
	  RegisterComplex(OrderStatusData);

	}

	protected override void InitializeCollectionProperties(Order model)
	{
		if (model.OrderItems == null)
		{
			throw new ArgumentException("OrderItems cannot be null");
		}
 
		OrderItems = new ChangeTrackingCollection<OrderItemWrapper>(model.OrderItems.Select(e => new OrderItemWrapper(e)));
		RegisterCollection(OrderItems, model.OrderItems);

		if (model.Comments == null)
		{
			throw new ArgumentException("Comments cannot be null");
		}
 
		Comments = new ChangeTrackingCollection<CommentWrapper>(model.Comments.Select(e => new CommentWrapper(e)));
		RegisterCollection(Comments, model.Comments);

	}
	}
}
