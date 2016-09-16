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
	public partial class AuditLogWrapper : ModelWrapperBase<AuditLog>
	{
		public AuditLogWrapper(AuditLog model) : base(model)
		{
		}

	public System.Int64 AuditLogKey
	{
		get { return GetValue<System.Int64>(); }
		set { SetValue(value); }
	}

	public System.Int64 AuditLogKeyOriginalValue => GetOriginalValue<System.Int64>(nameof(AuditLogKey));

	public bool AuditLogKeyIsChanged => GetIsChanged(nameof(AuditLogKey));

	public System.String AuditAction
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AuditActionOriginalValue => GetOriginalValue<System.String>(nameof(AuditAction));

	public bool AuditActionIsChanged => GetIsChanged(nameof(AuditAction));

	public System.String AuditBusinessObject
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AuditBusinessObjectOriginalValue => GetOriginalValue<System.String>(nameof(AuditBusinessObject));

	public bool AuditBusinessObjectIsChanged => GetIsChanged(nameof(AuditBusinessObject));

	public System.DateTime AuditDateTime
	{
		get { return GetValue<System.DateTime>(); }
		set { SetValue(value); }
	}

	public System.DateTime AuditDateTimeOriginalValue => GetOriginalValue<System.DateTime>(nameof(AuditDateTime));

	public bool AuditDateTimeIsChanged => GetIsChanged(nameof(AuditDateTime));

	public System.String AuditUserID
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AuditUserIDOriginalValue => GetOriginalValue<System.String>(nameof(AuditUserID));

	public bool AuditUserIDIsChanged => GetIsChanged(nameof(AuditUserID));

	public System.String AuditApplicationName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AuditApplicationNameOriginalValue => GetOriginalValue<System.String>(nameof(AuditApplicationName));

	public bool AuditApplicationNameIsChanged => GetIsChanged(nameof(AuditApplicationName));

	public System.String AuditHostName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AuditHostNameOriginalValue => GetOriginalValue<System.String>(nameof(AuditHostName));

	public bool AuditHostNameIsChanged => GetIsChanged(nameof(AuditHostName));

	public System.String AuditComment
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AuditCommentOriginalValue => GetOriginalValue<System.String>(nameof(AuditComment));

	public bool AuditCommentIsChanged => GetIsChanged(nameof(AuditComment));

	public System.String AuditOldDataXML
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AuditOldDataXMLOriginalValue => GetOriginalValue<System.String>(nameof(AuditOldDataXML));

	public bool AuditOldDataXMLIsChanged => GetIsChanged(nameof(AuditOldDataXML));

	public System.String AuditNewDataXML
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AuditNewDataXMLOriginalValue => GetOriginalValue<System.String>(nameof(AuditNewDataXML));

	public bool AuditNewDataXMLIsChanged => GetIsChanged(nameof(AuditNewDataXML));

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
