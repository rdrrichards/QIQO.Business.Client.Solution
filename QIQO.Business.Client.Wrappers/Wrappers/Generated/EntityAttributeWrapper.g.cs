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
	public partial class EntityAttributeWrapper : ModelWrapperBase<EntityAttribute>
	{
		public EntityAttributeWrapper(EntityAttribute model) : base(model)
		{
		}

	public System.Int32 AttributeKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 AttributeKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(AttributeKey));

	public bool AttributeKeyIsChanged => GetIsChanged(nameof(AttributeKey));

	public System.Int32 EntityKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 EntityKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(EntityKey));

	public bool EntityKeyIsChanged => GetIsChanged(nameof(EntityKey));

	public QIQO.Business.Client.Entities.QIQOEntityType EntityType
	{
		get { return GetValue<QIQO.Business.Client.Entities.QIQOEntityType>(); }
		set { SetValue(value); }
	}

	public QIQO.Business.Client.Entities.QIQOEntityType EntityTypeOriginalValue => GetOriginalValue<QIQO.Business.Client.Entities.QIQOEntityType>(nameof(EntityType));

	public bool EntityTypeIsChanged => GetIsChanged(nameof(EntityType));

	public QIQO.Business.Client.Entities.QIQOAttributeType AttributeType
	{
		get { return GetValue<QIQO.Business.Client.Entities.QIQOAttributeType>(); }
		set { SetValue(value); }
	}

	public QIQO.Business.Client.Entities.QIQOAttributeType AttributeTypeOriginalValue => GetOriginalValue<QIQO.Business.Client.Entities.QIQOAttributeType>(nameof(AttributeType));

	public bool AttributeTypeIsChanged => GetIsChanged(nameof(AttributeType));

	public System.String AttributeValue
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AttributeValueOriginalValue => GetOriginalValue<System.String>(nameof(AttributeValue));

	public bool AttributeValueIsChanged => GetIsChanged(nameof(AttributeValue));

	public System.Int32 AttributeDataTypeKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 AttributeDataTypeKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(AttributeDataTypeKey));

	public bool AttributeDataTypeKeyIsChanged => GetIsChanged(nameof(AttributeDataTypeKey));

	public QIQO.Business.Client.Entities.QIQOAttributeDataType AttributeDataType
	{
		get { return GetValue<QIQO.Business.Client.Entities.QIQOAttributeDataType>(); }
		set { SetValue(value); }
	}

	public QIQO.Business.Client.Entities.QIQOAttributeDataType AttributeDataTypeOriginalValue => GetOriginalValue<QIQO.Business.Client.Entities.QIQOAttributeDataType>(nameof(AttributeDataType));

	public bool AttributeDataTypeIsChanged => GetIsChanged(nameof(AttributeDataType));

	public System.String AttributeDisplayFormat
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AttributeDisplayFormatOriginalValue => GetOriginalValue<System.String>(nameof(AttributeDisplayFormat));

	public bool AttributeDisplayFormatIsChanged => GetIsChanged(nameof(AttributeDisplayFormat));

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
 
	public EntityTypeWrapper EntityTypeData { get; private set; } 
 
	public AttributeTypeWrapper AttributeTypeData { get; private set; } 
	
	protected override void InitializeComplexProperties(EntityAttribute model)
	{
	  if (model.EntityTypeData == null)
	  {
		throw new ArgumentException("EntityTypeData cannot be null");
	  }

	  EntityTypeData = new EntityTypeWrapper(model.EntityTypeData);
	  RegisterComplex(EntityTypeData);

	  if (model.AttributeTypeData == null)
	  {
		throw new ArgumentException("AttributeTypeData cannot be null");
	  }

	  AttributeTypeData = new AttributeTypeWrapper(model.AttributeTypeData);
	  RegisterComplex(AttributeTypeData);

	}
	}
}
