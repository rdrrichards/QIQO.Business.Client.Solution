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
	public partial class CommentTypeWrapper : ModelWrapperBase<CommentType>
	{
		public CommentTypeWrapper(CommentType model) : base(model)
		{
		}

	public System.Int32 CommentTypeKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 CommentTypeKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(CommentTypeKey));

	public bool CommentTypeKeyIsChanged => GetIsChanged(nameof(CommentTypeKey));

	public System.String CommentTypeCategory
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String CommentTypeCategoryOriginalValue => GetOriginalValue<System.String>(nameof(CommentTypeCategory));

	public bool CommentTypeCategoryIsChanged => GetIsChanged(nameof(CommentTypeCategory));

	public System.String CommentTypeCode
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String CommentTypeCodeOriginalValue => GetOriginalValue<System.String>(nameof(CommentTypeCode));

	public bool CommentTypeCodeIsChanged => GetIsChanged(nameof(CommentTypeCode));

	public System.String CommentTypeName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String CommentTypeNameOriginalValue => GetOriginalValue<System.String>(nameof(CommentTypeName));

	public bool CommentTypeNameIsChanged => GetIsChanged(nameof(CommentTypeName));

	public System.String CommentTypeDesc
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String CommentTypeDescOriginalValue => GetOriginalValue<System.String>(nameof(CommentTypeDesc));

	public bool CommentTypeDescIsChanged => GetIsChanged(nameof(CommentTypeDesc));

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
