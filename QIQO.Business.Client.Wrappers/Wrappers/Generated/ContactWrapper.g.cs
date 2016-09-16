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
	public partial class ContactWrapper : ModelWrapperBase<Contact>
	{
		public ContactWrapper(Contact model) : base(model)
		{
		}

	public System.Int32 ContactKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 ContactKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(ContactKey));

	public bool ContactKeyIsChanged => GetIsChanged(nameof(ContactKey));

	public System.Int32 EntityKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 EntityKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(EntityKey));

	public bool EntityKeyIsChanged => GetIsChanged(nameof(EntityKey));

	public System.Int32 EntityTypeKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 EntityTypeKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(EntityTypeKey));

	public bool EntityTypeKeyIsChanged => GetIsChanged(nameof(EntityTypeKey));

	public System.Int32 ContactTypeKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 ContactTypeKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(ContactTypeKey));

	public bool ContactTypeKeyIsChanged => GetIsChanged(nameof(ContactTypeKey));

	public QIQO.Business.Client.Entities.QIQOContactType ContactType
	{
		get { return GetValue<QIQO.Business.Client.Entities.QIQOContactType>(); }
		set { SetValue(value); }
	}

	public QIQO.Business.Client.Entities.QIQOContactType ContactTypeOriginalValue => GetOriginalValue<QIQO.Business.Client.Entities.QIQOContactType>(nameof(ContactType));

	public bool ContactTypeIsChanged => GetIsChanged(nameof(ContactType));

	public System.String ContactValue
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String ContactValueOriginalValue => GetOriginalValue<System.String>(nameof(ContactValue));

	public bool ContactValueIsChanged => GetIsChanged(nameof(ContactValue));

	public System.Int32 ContactDefaultFlg
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 ContactDefaultFlgOriginalValue => GetOriginalValue<System.Int32>(nameof(ContactDefaultFlg));

	public bool ContactDefaultFlgIsChanged => GetIsChanged(nameof(ContactDefaultFlg));

	public System.Int32 ContactActiveFlg
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 ContactActiveFlgOriginalValue => GetOriginalValue<System.Int32>(nameof(ContactActiveFlg));

	public bool ContactActiveFlgIsChanged => GetIsChanged(nameof(ContactActiveFlg));

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
