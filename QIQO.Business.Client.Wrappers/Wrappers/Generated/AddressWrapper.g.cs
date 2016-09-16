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
	public partial class AddressWrapper : ModelWrapperBase<Address>
	{
		public AddressWrapper(Address model) : base(model)
		{
		}

	public System.Int32 AddressKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 AddressKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(AddressKey));

	public bool AddressKeyIsChanged => GetIsChanged(nameof(AddressKey));

	public QIQO.Business.Client.Entities.QIQOAddressType AddressType
	{
		get { return GetValue<QIQO.Business.Client.Entities.QIQOAddressType>(); }
		set { SetValue(value); }
	}

	public QIQO.Business.Client.Entities.QIQOAddressType AddressTypeOriginalValue => GetOriginalValue<QIQO.Business.Client.Entities.QIQOAddressType>(nameof(AddressType));

	public bool AddressTypeIsChanged => GetIsChanged(nameof(AddressType));

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

	public System.String AddressLine1
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AddressLine1OriginalValue => GetOriginalValue<System.String>(nameof(AddressLine1));

	public bool AddressLine1IsChanged => GetIsChanged(nameof(AddressLine1));

	public System.String AddressLine2
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AddressLine2OriginalValue => GetOriginalValue<System.String>(nameof(AddressLine2));

	public bool AddressLine2IsChanged => GetIsChanged(nameof(AddressLine2));

	public System.String AddressLine3
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AddressLine3OriginalValue => GetOriginalValue<System.String>(nameof(AddressLine3));

	public bool AddressLine3IsChanged => GetIsChanged(nameof(AddressLine3));

	public System.String AddressLine4
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AddressLine4OriginalValue => GetOriginalValue<System.String>(nameof(AddressLine4));

	public bool AddressLine4IsChanged => GetIsChanged(nameof(AddressLine4));

	public System.String AddressCity
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AddressCityOriginalValue => GetOriginalValue<System.String>(nameof(AddressCity));

	public bool AddressCityIsChanged => GetIsChanged(nameof(AddressCity));

	public System.String AddressState
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AddressStateOriginalValue => GetOriginalValue<System.String>(nameof(AddressState));

	public bool AddressStateIsChanged => GetIsChanged(nameof(AddressState));

	public System.String AddressCounty
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AddressCountyOriginalValue => GetOriginalValue<System.String>(nameof(AddressCounty));

	public bool AddressCountyIsChanged => GetIsChanged(nameof(AddressCounty));

	public System.String AddressCountry
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AddressCountryOriginalValue => GetOriginalValue<System.String>(nameof(AddressCountry));

	public bool AddressCountryIsChanged => GetIsChanged(nameof(AddressCountry));

	public System.String AddressPostalCode
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AddressPostalCodeOriginalValue => GetOriginalValue<System.String>(nameof(AddressPostalCode));

	public bool AddressPostalCodeIsChanged => GetIsChanged(nameof(AddressPostalCode));

	public System.String AddressNotes
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AddressNotesOriginalValue => GetOriginalValue<System.String>(nameof(AddressNotes));

	public bool AddressNotesIsChanged => GetIsChanged(nameof(AddressNotes));

	public System.Boolean AddressDefaultFlag
	{
		get { return GetValue<System.Boolean>(); }
		set { SetValue(value); }
	}

	public System.Boolean AddressDefaultFlagOriginalValue => GetOriginalValue<System.Boolean>(nameof(AddressDefaultFlag));

	public bool AddressDefaultFlagIsChanged => GetIsChanged(nameof(AddressDefaultFlag));

	public System.Boolean AddressActiveFlag
	{
		get { return GetValue<System.Boolean>(); }
		set { SetValue(value); }
	}

	public System.Boolean AddressActiveFlagOriginalValue => GetOriginalValue<System.Boolean>(nameof(AddressActiveFlag));

	public bool AddressActiveFlagIsChanged => GetIsChanged(nameof(AddressActiveFlag));

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
 
	public AddressTypeWrapper AddressTypeData { get; private set; } 
	
	protected override void InitializeComplexProperties(Address model)
	{
	  if (model.AddressTypeData == null)
	  {
		throw new ArgumentException("AddressTypeData cannot be null");
	  }

	  AddressTypeData = new AddressTypeWrapper(model.AddressTypeData);
	  RegisterComplex(AddressTypeData);

	}
	}
}
