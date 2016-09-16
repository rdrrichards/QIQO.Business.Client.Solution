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
	public partial class InvoiceItemStatusWrapper : ModelWrapperBase<InvoiceItemStatus>
	{
		public InvoiceItemStatusWrapper(InvoiceItemStatus model) : base(model)
		{
		}

	public System.Int32 InvoiceItemStatusKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 InvoiceItemStatusKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(InvoiceItemStatusKey));

	public bool InvoiceItemStatusKeyIsChanged => GetIsChanged(nameof(InvoiceItemStatusKey));

	public System.String InvoiceItemStatusType
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String InvoiceItemStatusTypeOriginalValue => GetOriginalValue<System.String>(nameof(InvoiceItemStatusType));

	public bool InvoiceItemStatusTypeIsChanged => GetIsChanged(nameof(InvoiceItemStatusType));

	public System.String InvoiceItemStatusCode
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String InvoiceItemStatusCodeOriginalValue => GetOriginalValue<System.String>(nameof(InvoiceItemStatusCode));

	public bool InvoiceItemStatusCodeIsChanged => GetIsChanged(nameof(InvoiceItemStatusCode));

	public System.String InvoiceItemStatusName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String InvoiceItemStatusNameOriginalValue => GetOriginalValue<System.String>(nameof(InvoiceItemStatusName));

	public bool InvoiceItemStatusNameIsChanged => GetIsChanged(nameof(InvoiceItemStatusName));

	public System.String InvoiceItemStatusDesc
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String InvoiceItemStatusDescOriginalValue => GetOriginalValue<System.String>(nameof(InvoiceItemStatusDesc));

	public bool InvoiceItemStatusDescIsChanged => GetIsChanged(nameof(InvoiceItemStatusDesc));

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
