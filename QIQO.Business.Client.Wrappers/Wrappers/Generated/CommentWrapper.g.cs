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
	public partial class CommentWrapper : ModelWrapperBase<Comment>
	{
		public CommentWrapper(Comment model) : base(model)
		{
		}

	public System.Int32 CommentKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 CommentKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(CommentKey));

	public bool CommentKeyIsChanged => GetIsChanged(nameof(CommentKey));

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

	public QIQO.Business.Client.Entities.QIQOCommentType CommentType
	{
		get { return GetValue<QIQO.Business.Client.Entities.QIQOCommentType>(); }
		set { SetValue(value); }
	}

	public QIQO.Business.Client.Entities.QIQOCommentType CommentTypeOriginalValue => GetOriginalValue<QIQO.Business.Client.Entities.QIQOCommentType>(nameof(CommentType));

	public bool CommentTypeIsChanged => GetIsChanged(nameof(CommentType));

	public System.String CommentValue
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String CommentValueOriginalValue => GetOriginalValue<System.String>(nameof(CommentValue));

	public bool CommentValueIsChanged => GetIsChanged(nameof(CommentValue));

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
