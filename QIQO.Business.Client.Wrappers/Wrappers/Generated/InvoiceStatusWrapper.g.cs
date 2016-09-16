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
	public partial class InvoiceStatusWrapper : ModelWrapperBase<InvoiceStatus>
	{
		public InvoiceStatusWrapper(InvoiceStatus model) : base(model)
		{
		}

	public System.Int32 InvoiceStatusKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 InvoiceStatusKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(InvoiceStatusKey));

	public bool InvoiceStatusKeyIsChanged => GetIsChanged(nameof(InvoiceStatusKey));

	public System.String InvoiceStatusCode
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String InvoiceStatusCodeOriginalValue => GetOriginalValue<System.String>(nameof(InvoiceStatusCode));

	public bool InvoiceStatusCodeIsChanged => GetIsChanged(nameof(InvoiceStatusCode));

	public System.String InvoiceStatusName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String InvoiceStatusNameOriginalValue => GetOriginalValue<System.String>(nameof(InvoiceStatusName));

	public bool InvoiceStatusNameIsChanged => GetIsChanged(nameof(InvoiceStatusName));

	public System.String InvoiceStatusDesc
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String InvoiceStatusDescOriginalValue => GetOriginalValue<System.String>(nameof(InvoiceStatusDesc));

	public bool InvoiceStatusDescIsChanged => GetIsChanged(nameof(InvoiceStatusDesc));

	public System.String InvoiceStatusType
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String InvoiceStatusTypeOriginalValue => GetOriginalValue<System.String>(nameof(InvoiceStatusType));

	public bool InvoiceStatusTypeIsChanged => GetIsChanged(nameof(InvoiceStatusType));

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
