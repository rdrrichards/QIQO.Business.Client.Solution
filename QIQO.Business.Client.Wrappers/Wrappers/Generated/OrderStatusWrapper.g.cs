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
	public partial class OrderStatusWrapper : ModelWrapperBase<OrderStatus>
	{
		public OrderStatusWrapper(OrderStatus model) : base(model)
		{
		}

	public System.Int32 OrderStatusKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 OrderStatusKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(OrderStatusKey));

	public bool OrderStatusKeyIsChanged => GetIsChanged(nameof(OrderStatusKey));

	public System.String OrderStatusCode
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String OrderStatusCodeOriginalValue => GetOriginalValue<System.String>(nameof(OrderStatusCode));

	public bool OrderStatusCodeIsChanged => GetIsChanged(nameof(OrderStatusCode));

	public System.String OrderStatusName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String OrderStatusNameOriginalValue => GetOriginalValue<System.String>(nameof(OrderStatusName));

	public bool OrderStatusNameIsChanged => GetIsChanged(nameof(OrderStatusName));

	public System.String OrderStatusDesc
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String OrderStatusDescOriginalValue => GetOriginalValue<System.String>(nameof(OrderStatusDesc));

	public bool OrderStatusDescIsChanged => GetIsChanged(nameof(OrderStatusDesc));

	public System.String OrderStatusType
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String OrderStatusTypeOriginalValue => GetOriginalValue<System.String>(nameof(OrderStatusType));

	public bool OrderStatusTypeIsChanged => GetIsChanged(nameof(OrderStatusType));

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
