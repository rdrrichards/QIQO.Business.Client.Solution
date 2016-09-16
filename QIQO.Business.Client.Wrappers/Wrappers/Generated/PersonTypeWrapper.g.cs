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
	public partial class PersonTypeWrapper : ModelWrapperBase<PersonType>
	{
		public PersonTypeWrapper(PersonType model) : base(model)
		{
		}

	public System.Int32 PersonTypeKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 PersonTypeKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(PersonTypeKey));

	public bool PersonTypeKeyIsChanged => GetIsChanged(nameof(PersonTypeKey));

	public System.String PersonTypeCategory
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String PersonTypeCategoryOriginalValue => GetOriginalValue<System.String>(nameof(PersonTypeCategory));

	public bool PersonTypeCategoryIsChanged => GetIsChanged(nameof(PersonTypeCategory));

	public System.String PersonTypeCode
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String PersonTypeCodeOriginalValue => GetOriginalValue<System.String>(nameof(PersonTypeCode));

	public bool PersonTypeCodeIsChanged => GetIsChanged(nameof(PersonTypeCode));

	public System.String PersonTypeName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String PersonTypeNameOriginalValue => GetOriginalValue<System.String>(nameof(PersonTypeName));

	public bool PersonTypeNameIsChanged => GetIsChanged(nameof(PersonTypeName));

	public System.String PersonTypeDesc
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String PersonTypeDescOriginalValue => GetOriginalValue<System.String>(nameof(PersonTypeDesc));

	public bool PersonTypeDescIsChanged => GetIsChanged(nameof(PersonTypeDesc));

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
