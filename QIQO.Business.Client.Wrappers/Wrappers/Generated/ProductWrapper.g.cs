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
	public partial class ProductWrapper : ModelWrapperBase<Product>
	{
		public ProductWrapper(Product model) : base(model)
		{
		}

	public System.Int32 ProductKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 ProductKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(ProductKey));

	public bool ProductKeyIsChanged => GetIsChanged(nameof(ProductKey));

	public QIQO.Business.Client.Entities.QIQOProductType ProductType
	{
		get { return GetValue<QIQO.Business.Client.Entities.QIQOProductType>(); }
		set { SetValue(value); }
	}

	public QIQO.Business.Client.Entities.QIQOProductType ProductTypeOriginalValue => GetOriginalValue<QIQO.Business.Client.Entities.QIQOProductType>(nameof(ProductType));

	public bool ProductTypeIsChanged => GetIsChanged(nameof(ProductType));

	public System.String ProductCode
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String ProductCodeOriginalValue => GetOriginalValue<System.String>(nameof(ProductCode));

	public bool ProductCodeIsChanged => GetIsChanged(nameof(ProductCode));

	public System.String ProductName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String ProductNameOriginalValue => GetOriginalValue<System.String>(nameof(ProductName));

	public bool ProductNameIsChanged => GetIsChanged(nameof(ProductName));

	public System.String ProductDesc
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String ProductDescOriginalValue => GetOriginalValue<System.String>(nameof(ProductDesc));

	public bool ProductDescIsChanged => GetIsChanged(nameof(ProductDesc));

	public System.String ProductNameShort
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String ProductNameShortOriginalValue => GetOriginalValue<System.String>(nameof(ProductNameShort));

	public bool ProductNameShortIsChanged => GetIsChanged(nameof(ProductNameShort));

	public System.String ProductNameLong
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String ProductNameLongOriginalValue => GetOriginalValue<System.String>(nameof(ProductNameLong));

	public bool ProductNameLongIsChanged => GetIsChanged(nameof(ProductNameLong));

	public System.String ProductImagePath
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String ProductImagePathOriginalValue => GetOriginalValue<System.String>(nameof(ProductImagePath));

	public bool ProductImagePathIsChanged => GetIsChanged(nameof(ProductImagePath));

	public System.String ProductDescCombo
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String ProductDescComboOriginalValue => GetOriginalValue<System.String>(nameof(ProductDescCombo));

	public bool ProductDescComboIsChanged => GetIsChanged(nameof(ProductDescCombo));

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
 
	public ProductTypeWrapper ProductTypeData { get; private set; } 
 
	public ChangeTrackingCollection<EntityAttributeWrapper> ProductAttributes { get; private set; }
	
	protected override void InitializeComplexProperties(Product model)
	{
	  if (model.ProductTypeData == null)
	  {
		throw new ArgumentException("ProductTypeData cannot be null");
	  }

	  ProductTypeData = new ProductTypeWrapper(model.ProductTypeData);
	  RegisterComplex(ProductTypeData);

	}

	protected override void InitializeCollectionProperties(Product model)
	{
		if (model.ProductAttributes == null)
		{
			throw new ArgumentException("ProductAttributes cannot be null");
		}
 
		ProductAttributes = new ChangeTrackingCollection<EntityAttributeWrapper>(model.ProductAttributes.Select(e => new EntityAttributeWrapper(e)));
		RegisterCollection(ProductAttributes, model.ProductAttributes);

	}
	}
}
