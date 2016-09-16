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
	public partial class UserSessionWrapper : ModelWrapperBase<UserSession>
	{
		public UserSessionWrapper(UserSession model) : base(model)
		{
		}

	public System.Int32 ProcessID
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 ProcessIDOriginalValue => GetOriginalValue<System.Int32>(nameof(ProcessID));

	public bool ProcessIDIsChanged => GetIsChanged(nameof(ProcessID));

	public System.String SessionID
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String SessionIDOriginalValue => GetOriginalValue<System.String>(nameof(SessionID));

	public bool SessionIDIsChanged => GetIsChanged(nameof(SessionID));

	public System.String UserDomain
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String UserDomainOriginalValue => GetOriginalValue<System.String>(nameof(UserDomain));

	public bool UserDomainIsChanged => GetIsChanged(nameof(UserDomain));

	public System.String UserName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String UserNameOriginalValue => GetOriginalValue<System.String>(nameof(UserName));

	public bool UserNameIsChanged => GetIsChanged(nameof(UserName));

	public System.String HostName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String HostNameOriginalValue => GetOriginalValue<System.String>(nameof(HostName));

	public bool HostNameIsChanged => GetIsChanged(nameof(HostName));

	public System.DateTime StartTime
	{
		get { return GetValue<System.DateTime>(); }
		set { SetValue(value); }
	}

	public System.DateTime StartTimeOriginalValue => GetOriginalValue<System.DateTime>(nameof(StartTime));

	public bool StartTimeIsChanged => GetIsChanged(nameof(StartTime));

	public System.Nullable<System.DateTime> EndTime
	{
		get { return GetValue<System.Nullable<System.DateTime>>(); }
		set { SetValue(value); }
	}

	public System.Nullable<System.DateTime> EndTimeOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(EndTime));

	public bool EndTimeIsChanged => GetIsChanged(nameof(EndTime));

	public System.Int32 Active
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 ActiveOriginalValue => GetOriginalValue<System.Int32>(nameof(Active));

	public bool ActiveIsChanged => GetIsChanged(nameof(Active));

	public System.Int32 CompanyKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 CompanyKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(CompanyKey));

	public bool CompanyKeyIsChanged => GetIsChanged(nameof(CompanyKey));

	public System.String CompanyName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String CompanyNameOriginalValue => GetOriginalValue<System.String>(nameof(CompanyName));

	public bool CompanyNameIsChanged => GetIsChanged(nameof(CompanyName));

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
