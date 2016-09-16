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
	public partial class AccountWrapper : ModelWrapperBase<Account>
	{
		public AccountWrapper(Account model) : base(model)
		{
		}

	public System.Int32 AccountKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 AccountKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(AccountKey));

	public bool AccountKeyIsChanged => GetIsChanged(nameof(AccountKey));

	public System.Int32 CompanyKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 CompanyKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(CompanyKey));

	public bool CompanyKeyIsChanged => GetIsChanged(nameof(CompanyKey));

	public QIQO.Business.Client.Entities.QIQOAccountType AccountType
	{
		get { return GetValue<QIQO.Business.Client.Entities.QIQOAccountType>(); }
		set { SetValue(value); }
	}

	public QIQO.Business.Client.Entities.QIQOAccountType AccountTypeOriginalValue => GetOriginalValue<QIQO.Business.Client.Entities.QIQOAccountType>(nameof(AccountType));

	public bool AccountTypeIsChanged => GetIsChanged(nameof(AccountType));

	public System.String AccountCode
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AccountCodeOriginalValue => GetOriginalValue<System.String>(nameof(AccountCode));

	public bool AccountCodeIsChanged => GetIsChanged(nameof(AccountCode));

	public System.String AccountName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AccountNameOriginalValue => GetOriginalValue<System.String>(nameof(AccountName));

	public bool AccountNameIsChanged => GetIsChanged(nameof(AccountName));

	public System.String AccountDesc
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AccountDescOriginalValue => GetOriginalValue<System.String>(nameof(AccountDesc));

	public bool AccountDescIsChanged => GetIsChanged(nameof(AccountDesc));

	public System.String AccountDBA
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String AccountDBAOriginalValue => GetOriginalValue<System.String>(nameof(AccountDBA));

	public bool AccountDBAIsChanged => GetIsChanged(nameof(AccountDBA));

	public System.DateTime AccountStartDate
	{
		get { return GetValue<System.DateTime>(); }
		set { SetValue(value); }
	}

	public System.DateTime AccountStartDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(AccountStartDate));

	public bool AccountStartDateIsChanged => GetIsChanged(nameof(AccountStartDate));

	public System.DateTime AccountEndDate
	{
		get { return GetValue<System.DateTime>(); }
		set { SetValue(value); }
	}

	public System.DateTime AccountEndDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(AccountEndDate));

	public bool AccountEndDateIsChanged => GetIsChanged(nameof(AccountEndDate));

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

	public System.Int32 OwnerAccountKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 OwnerAccountKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(OwnerAccountKey));

	public bool OwnerAccountKeyIsChanged => GetIsChanged(nameof(OwnerAccountKey));
 
	public ChangeTrackingCollection<AddressWrapper> Addresses { get; private set; }
 
	public ChangeTrackingCollection<EntityAttributeWrapper> AccountAttributes { get; private set; }
 
	public ChangeTrackingCollection<FeeScheduleWrapper> FeeSchedules { get; private set; }
 
	public ChangeTrackingCollection<AccountPersonWrapper> Employees { get; private set; }
 
	public ChangeTrackingCollection<ContactWrapper> Contacts { get; private set; }
 
	public ChangeTrackingCollection<CommentWrapper> Comments { get; private set; }

	protected override void InitializeCollectionProperties(Account model)
	{
		if (model.Addresses == null)
		{
			throw new ArgumentException("Addresses cannot be null");
		}
 
		Addresses = new ChangeTrackingCollection<AddressWrapper>(model.Addresses.Select(e => new AddressWrapper(e)));
		RegisterCollection(Addresses, model.Addresses);

		if (model.AccountAttributes == null)
		{
			throw new ArgumentException("AccountAttributes cannot be null");
		}
 
		AccountAttributes = new ChangeTrackingCollection<EntityAttributeWrapper>(model.AccountAttributes.Select(e => new EntityAttributeWrapper(e)));
		RegisterCollection(AccountAttributes, model.AccountAttributes);

		if (model.FeeSchedules == null)
		{
			throw new ArgumentException("FeeSchedules cannot be null");
		}
 
		FeeSchedules = new ChangeTrackingCollection<FeeScheduleWrapper>(model.FeeSchedules.Select(e => new FeeScheduleWrapper(e)));
		RegisterCollection(FeeSchedules, model.FeeSchedules);

		if (model.Employees == null)
		{
			throw new ArgumentException("Employees cannot be null");
		}
 
		Employees = new ChangeTrackingCollection<AccountPersonWrapper>(model.Employees.Select(e => new AccountPersonWrapper(e)));
		RegisterCollection(Employees, model.Employees);

		if (model.Contacts == null)
		{
			throw new ArgumentException("Contacts cannot be null");
		}
 
		Contacts = new ChangeTrackingCollection<ContactWrapper>(model.Contacts.Select(e => new ContactWrapper(e)));
		RegisterCollection(Contacts, model.Contacts);

		if (model.Comments == null)
		{
			throw new ArgumentException("Comments cannot be null");
		}
 
		Comments = new ChangeTrackingCollection<CommentWrapper>(model.Comments.Select(e => new CommentWrapper(e)));
		RegisterCollection(Comments, model.Comments);

	}
	}
}
