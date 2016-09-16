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
	public partial class ProductTypeWrapper : ModelWrapperBase<ProductType>
	{
		public ProductTypeWrapper(ProductType model) : base(model)
		{
		}

	public System.Int32 ProductTypeKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 ProductTypeKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(ProductTypeKey));

	public bool ProductTypeKeyIsChanged => GetIsChanged(nameof(ProductTypeKey));

	public System.String ProductTypeCategory
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String ProductTypeCategoryOriginalValue => GetOriginalValue<System.String>(nameof(ProductTypeCategory));

	public bool ProductTypeCategoryIsChanged => GetIsChanged(nameof(ProductTypeCategory));

	public System.String ProductTypeCode
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String ProductTypeCodeOriginalValue => GetOriginalValue<System.String>(nameof(ProductTypeCode));

	public bool ProductTypeCodeIsChanged => GetIsChanged(nameof(ProductTypeCode));

	public System.String ProductTypeName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String ProductTypeNameOriginalValue => GetOriginalValue<System.String>(nameof(ProductTypeName));

	public bool ProductTypeNameIsChanged => GetIsChanged(nameof(ProductTypeName));

	public System.String ProductTypeDesc
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String ProductTypeDescOriginalValue => GetOriginalValue<System.String>(nameof(ProductTypeDesc));

	public bool ProductTypeDescIsChanged => GetIsChanged(nameof(ProductTypeDesc));

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
