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
	public partial class AddressPostalWrapper : ModelWrapperBase<AddressPostal>
	{
		public AddressPostalWrapper(AddressPostal model) : base(model)
		{
		}

	public System.String CountryName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String CountryNameOriginalValue => GetOriginalValue<System.String>(nameof(CountryName));

	public bool CountryNameIsChanged => GetIsChanged(nameof(CountryName));

	public System.String PostalCode
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String PostalCodeOriginalValue => GetOriginalValue<System.String>(nameof(PostalCode));

	public bool PostalCodeIsChanged => GetIsChanged(nameof(PostalCode));

	public System.String StateCode
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String StateCodeOriginalValue => GetOriginalValue<System.String>(nameof(StateCode));

	public bool StateCodeIsChanged => GetIsChanged(nameof(StateCode));

	public System.String StateFullName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String StateFullNameOriginalValue => GetOriginalValue<System.String>(nameof(StateFullName));

	public bool StateFullNameIsChanged => GetIsChanged(nameof(StateFullName));

	public System.String CityName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String CityNameOriginalValue => GetOriginalValue<System.String>(nameof(CityName));

	public bool CityNameIsChanged => GetIsChanged(nameof(CityName));

	public System.String CountyName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String CountyNameOriginalValue => GetOriginalValue<System.String>(nameof(CountyName));

	public bool CountyNameIsChanged => GetIsChanged(nameof(CountyName));

	public System.Int32 TimeZone
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 TimeZoneOriginalValue => GetOriginalValue<System.Int32>(nameof(TimeZone));

	public bool TimeZoneIsChanged => GetIsChanged(nameof(TimeZone));
	}
}
