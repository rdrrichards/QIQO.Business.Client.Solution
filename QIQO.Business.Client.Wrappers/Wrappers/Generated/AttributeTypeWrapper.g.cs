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
	public partial class AttributeTypeWrapper : ModelWrapperBase<AttributeType>
	{
		public AttributeTypeWrapper(AttributeType model) : base(model)
		{
		}

	public System.Int32 AttributeTypeKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 AttributeTypeKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(AttributeTypeKey));

	public bool AttributeTypeKeyIsChanged => GetIsChanged(nameof(AttributeTypeKey));

	public System.String AttributeTypeCategory
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AttributeTypeCategoryOriginalValue => GetOriginalValue<System.String>(nameof(AttributeTypeCategory));

	public bool AttributeTypeCategoryIsChanged => GetIsChanged(nameof(AttributeTypeCategory));

	public System.String AttributeTypeCode
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AttributeTypeCodeOriginalValue => GetOriginalValue<System.String>(nameof(AttributeTypeCode));

	public bool AttributeTypeCodeIsChanged => GetIsChanged(nameof(AttributeTypeCode));

	public System.String AttributeTypeName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AttributeTypeNameOriginalValue => GetOriginalValue<System.String>(nameof(AttributeTypeName));

	public bool AttributeTypeNameIsChanged => GetIsChanged(nameof(AttributeTypeName));

	public System.String AttributeTypeDesc
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AttributeTypeDescOriginalValue => GetOriginalValue<System.String>(nameof(AttributeTypeDesc));

	public bool AttributeTypeDescIsChanged => GetIsChanged(nameof(AttributeTypeDesc));

	public QIQO.Business.Client.Entities.QIQOAttributeDataType AttributeDataTypeKey
	{
		get { return GetValue<QIQO.Business.Client.Entities.QIQOAttributeDataType>(); }
		set { SetValue(value); }
	}

	public QIQO.Business.Client.Entities.QIQOAttributeDataType AttributeDataTypeKeyOriginalValue => GetOriginalValue<QIQO.Business.Client.Entities.QIQOAttributeDataType>(nameof(AttributeDataTypeKey));

	public bool AttributeDataTypeKeyIsChanged => GetIsChanged(nameof(AttributeDataTypeKey));

	public System.String AttributeDefaultFormat
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AttributeDefaultFormatOriginalValue => GetOriginalValue<System.String>(nameof(AttributeDefaultFormat));

	public bool AttributeDefaultFormatIsChanged => GetIsChanged(nameof(AttributeDefaultFormat));

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
