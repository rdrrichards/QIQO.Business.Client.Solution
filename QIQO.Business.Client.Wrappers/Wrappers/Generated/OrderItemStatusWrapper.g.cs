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
	public partial class OrderItemStatusWrapper : ModelWrapperBase<OrderItemStatus>
	{
		public OrderItemStatusWrapper(OrderItemStatus model) : base(model)
		{
		}

	public System.Int32 OrderItemStatusKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 OrderItemStatusKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(OrderItemStatusKey));

	public bool OrderItemStatusKeyIsChanged => GetIsChanged(nameof(OrderItemStatusKey));

	public System.String OrderItemStatusCode
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String OrderItemStatusCodeOriginalValue => GetOriginalValue<System.String>(nameof(OrderItemStatusCode));

	public bool OrderItemStatusCodeIsChanged => GetIsChanged(nameof(OrderItemStatusCode));

	public System.String OrderItemStatusName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String OrderItemStatusNameOriginalValue => GetOriginalValue<System.String>(nameof(OrderItemStatusName));

	public bool OrderItemStatusNameIsChanged => GetIsChanged(nameof(OrderItemStatusName));

	public System.String OrderItemStatusDesc
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String OrderItemStatusDescOriginalValue => GetOriginalValue<System.String>(nameof(OrderItemStatusDesc));

	public bool OrderItemStatusDescIsChanged => GetIsChanged(nameof(OrderItemStatusDesc));

	public System.String OrderItemStatusType
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String OrderItemStatusTypeOriginalValue => GetOriginalValue<System.String>(nameof(OrderItemStatusType));

	public bool OrderItemStatusTypeIsChanged => GetIsChanged(nameof(OrderItemStatusType));

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
