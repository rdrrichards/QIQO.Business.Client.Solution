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
	public partial class ContactTypeWrapper : ModelWrapperBase<ContactType>
	{
		public ContactTypeWrapper(ContactType model) : base(model)
		{
		}

	public System.Int32 ContactTypeKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 ContactTypeKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(ContactTypeKey));

	public bool ContactTypeKeyIsChanged => GetIsChanged(nameof(ContactTypeKey));

	public System.String ContactTypeCategory
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String ContactTypeCategoryOriginalValue => GetOriginalValue<System.String>(nameof(ContactTypeCategory));

	public bool ContactTypeCategoryIsChanged => GetIsChanged(nameof(ContactTypeCategory));

	public System.String ContactTypeCode
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String ContactTypeCodeOriginalValue => GetOriginalValue<System.String>(nameof(ContactTypeCode));

	public bool ContactTypeCodeIsChanged => GetIsChanged(nameof(ContactTypeCode));

	public System.String ContactTypeName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String ContactTypeNameOriginalValue => GetOriginalValue<System.String>(nameof(ContactTypeName));

	public bool ContactTypeNameIsChanged => GetIsChanged(nameof(ContactTypeName));

	public System.String ContactTypeDesc
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String ContactTypeDescOriginalValue => GetOriginalValue<System.String>(nameof(ContactTypeDesc));

	public bool ContactTypeDescIsChanged => GetIsChanged(nameof(ContactTypeDesc));

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

	public System.Int32 TypeRowKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 TypeRowKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(TypeRowKey));

	public bool TypeRowKeyIsChanged => GetIsChanged(nameof(TypeRowKey));
	}
}
