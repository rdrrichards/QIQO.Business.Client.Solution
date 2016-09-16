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
	public partial class AccountPersonWrapper : ModelWrapperBase<AccountPerson>
	{
		public AccountPersonWrapper(AccountPerson model) : base(model)
		{
		}

	public System.String RoleInCompany
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String RoleInCompanyOriginalValue => GetOriginalValue<System.String>(nameof(RoleInCompany));

	public bool RoleInCompanyIsChanged => GetIsChanged(nameof(RoleInCompany));

	public System.Int32 EntityPersonKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 EntityPersonKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(EntityPersonKey));

	public bool EntityPersonKeyIsChanged => GetIsChanged(nameof(EntityPersonKey));

	public System.DateTime StartDate
	{
		get { return GetValue<System.DateTime>(); }
		set { SetValue(value); }
	}

	public System.DateTime StartDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(StartDate));

	public bool StartDateIsChanged => GetIsChanged(nameof(StartDate));

	public System.DateTime EndDate
	{
		get { return GetValue<System.DateTime>(); }
		set { SetValue(value); }
	}

	public System.DateTime EndDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(EndDate));

	public bool EndDateIsChanged => GetIsChanged(nameof(EndDate));

	public System.String Comment
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));

	public bool CommentIsChanged => GetIsChanged(nameof(Comment));

	public QIQO.Business.Client.Entities.QIQOPersonType CompanyRoleType
	{
		get { return GetValue<QIQO.Business.Client.Entities.QIQOPersonType>(); }
		set { SetValue(value); }
	}

	public QIQO.Business.Client.Entities.QIQOPersonType CompanyRoleTypeOriginalValue => GetOriginalValue<QIQO.Business.Client.Entities.QIQOPersonType>(nameof(CompanyRoleType));

	public bool CompanyRoleTypeIsChanged => GetIsChanged(nameof(CompanyRoleType));

	public System.Int32 PersonKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 PersonKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(PersonKey));

	public bool PersonKeyIsChanged => GetIsChanged(nameof(PersonKey));

	public System.String PersonCode
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String PersonCodeOriginalValue => GetOriginalValue<System.String>(nameof(PersonCode));

	public bool PersonCodeIsChanged => GetIsChanged(nameof(PersonCode));

	public System.String PersonFirstName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String PersonFirstNameOriginalValue => GetOriginalValue<System.String>(nameof(PersonFirstName));

	public bool PersonFirstNameIsChanged => GetIsChanged(nameof(PersonFirstName));

	public System.String PersonMI
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String PersonMIOriginalValue => GetOriginalValue<System.String>(nameof(PersonMI));

	public bool PersonMIIsChanged => GetIsChanged(nameof(PersonMI));

	public System.String PersonLastName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String PersonLastNameOriginalValue => GetOriginalValue<System.String>(nameof(PersonLastName));

	public bool PersonLastNameIsChanged => GetIsChanged(nameof(PersonLastName));

	public System.String PersonFullNameFL
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String PersonFullNameFLOriginalValue => GetOriginalValue<System.String>(nameof(PersonFullNameFL));

	public bool PersonFullNameFLIsChanged => GetIsChanged(nameof(PersonFullNameFL));

	public System.String PersonFullNameFML
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String PersonFullNameFMLOriginalValue => GetOriginalValue<System.String>(nameof(PersonFullNameFML));

	public bool PersonFullNameFMLIsChanged => GetIsChanged(nameof(PersonFullNameFML));

	public System.String PersonFullNameLF
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String PersonFullNameLFOriginalValue => GetOriginalValue<System.String>(nameof(PersonFullNameLF));

	public bool PersonFullNameLFIsChanged => GetIsChanged(nameof(PersonFullNameLF));

	public System.String PersonFullNameLFM
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String PersonFullNameLFMOriginalValue => GetOriginalValue<System.String>(nameof(PersonFullNameLFM));

	public bool PersonFullNameLFMIsChanged => GetIsChanged(nameof(PersonFullNameLFM));

	public System.Nullable<System.DateTime> PersonDOB
	{
		get { return GetValue<System.Nullable<System.DateTime>>(); }
		set { SetValue(value); }
	}

	public System.Nullable<System.DateTime> PersonDOBOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(PersonDOB));

	public bool PersonDOBIsChanged => GetIsChanged(nameof(PersonDOB));

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
 
	public PersonTypeWrapper PersonTypeData { get; private set; } 
 
	public ChangeTrackingCollection<AddressWrapper> Addresses { get; private set; }
 
	public ChangeTrackingCollection<EntityAttributeWrapper> PersonAttributes { get; private set; }
	
	protected override void InitializeComplexProperties(AccountPerson model)
	{
	  if (model.PersonTypeData == null)
	  {
		throw new ArgumentException("PersonTypeData cannot be null");
	  }

	  PersonTypeData = new PersonTypeWrapper(model.PersonTypeData);
	  RegisterComplex(PersonTypeData);

	}

	protected override void InitializeCollectionProperties(AccountPerson model)
	{
		if (model.Addresses == null)
		{
			throw new ArgumentException("Addresses cannot be null");
		}
 
		Addresses = new ChangeTrackingCollection<AddressWrapper>(model.Addresses.Select(e => new AddressWrapper(e)));
		RegisterCollection(Addresses, model.Addresses);

		if (model.PersonAttributes == null)
		{
			throw new ArgumentException("PersonAttributes cannot be null");
		}
 
		PersonAttributes = new ChangeTrackingCollection<EntityAttributeWrapper>(model.PersonAttributes.Select(e => new EntityAttributeWrapper(e)));
		RegisterCollection(PersonAttributes, model.PersonAttributes);

	}
	}
}
