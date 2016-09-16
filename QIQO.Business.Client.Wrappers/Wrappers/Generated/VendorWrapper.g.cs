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
	public partial class VendorWrapper : ModelWrapperBase<Vendor>
	{
		public VendorWrapper(Vendor model) : base(model)
		{
		}

	public System.Int32 VendorKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 VendorKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(VendorKey));

	public bool VendorKeyIsChanged => GetIsChanged(nameof(VendorKey));

	public System.String VendorCode
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String VendorCodeOriginalValue => GetOriginalValue<System.String>(nameof(VendorCode));

	public bool VendorCodeIsChanged => GetIsChanged(nameof(VendorCode));

	public System.String VendorName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String VendorNameOriginalValue => GetOriginalValue<System.String>(nameof(VendorName));

	public bool VendorNameIsChanged => GetIsChanged(nameof(VendorName));

	public System.String VendorDesc
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String VendorDescOriginalValue => GetOriginalValue<System.String>(nameof(VendorDesc));

	public bool VendorDescIsChanged => GetIsChanged(nameof(VendorDesc));

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
 
	public ChangeTrackingCollection<VendorRepresentativeWrapper> VendorRepresentatives { get; private set; }

	protected override void InitializeCollectionProperties(Vendor model)
	{
		if (model.VendorRepresentatives == null)
		{
			throw new ArgumentException("VendorRepresentatives cannot be null");
		}
 
		VendorRepresentatives = new ChangeTrackingCollection<VendorRepresentativeWrapper>(model.VendorRepresentatives.Select(e => new VendorRepresentativeWrapper(e)));
		RegisterCollection(VendorRepresentatives, model.VendorRepresentatives);

	}
	}
}
