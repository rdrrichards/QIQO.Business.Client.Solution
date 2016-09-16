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
	public partial class CompanyWrapper : ModelWrapperBase<Company>
	{
		public CompanyWrapper(Company model) : base(model)
		{
		}

	public System.Int32 CompanyKey
	{
		get { return GetValue<System.Int32>(); }
		set { SetValue(value); }
	}

	public System.Int32 CompanyKeyOriginalValue => GetOriginalValue<System.Int32>(nameof(CompanyKey));

	public bool CompanyKeyIsChanged => GetIsChanged(nameof(CompanyKey));

	public System.String CompanyCode
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String CompanyCodeOriginalValue => GetOriginalValue<System.String>(nameof(CompanyCode));

	public bool CompanyCodeIsChanged => GetIsChanged(nameof(CompanyCode));

	public System.String CompanyName
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String CompanyNameOriginalValue => GetOriginalValue<System.String>(nameof(CompanyName));

	public bool CompanyNameIsChanged => GetIsChanged(nameof(CompanyName));

	public System.String CompanyDesc
	{
		get { return GetValue<System.String>(); }
		set { SetValue(value); }
	}

	public System.String CompanyDescOriginalValue => GetOriginalValue<System.String>(nameof(CompanyDesc));

	public bool CompanyDescIsChanged => GetIsChanged(nameof(CompanyDesc));

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
 
	public ChangeTrackingCollection<EmployeeWrapper> Employees { get; private set; }
 
	public ChangeTrackingCollection<ChartOfAccountWrapper> GLAccounts { get; private set; }
 
	public ChangeTrackingCollection<LedgerWrapper> Ledgers { get; private set; }
 
	public ChangeTrackingCollection<EntityAttributeWrapper> CompanyAttributes { get; private set; }
 
	public ChangeTrackingCollection<AddressWrapper> CompanyAddresses { get; private set; }

	protected override void InitializeCollectionProperties(Company model)
	{
		if (model.Employees == null)
		{
			throw new ArgumentException("Employees cannot be null");
		}
 
		Employees = new ChangeTrackingCollection<EmployeeWrapper>(model.Employees.Select(e => new EmployeeWrapper(e)));
		RegisterCollection(Employees, model.Employees);

		if (model.GLAccounts == null)
		{
			throw new ArgumentException("GLAccounts cannot be null");
		}
 
		GLAccounts = new ChangeTrackingCollection<ChartOfAccountWrapper>(model.GLAccounts.Select(e => new ChartOfAccountWrapper(e)));
		RegisterCollection(GLAccounts, model.GLAccounts);

		if (model.Ledgers == null)
		{
			throw new ArgumentException("Ledgers cannot be null");
		}
 
		Ledgers = new ChangeTrackingCollection<LedgerWrapper>(model.Ledgers.Select(e => new LedgerWrapper(e)));
		RegisterCollection(Ledgers, model.Ledgers);

		if (model.CompanyAttributes == null)
		{
			throw new ArgumentException("CompanyAttributes cannot be null");
		}
 
		CompanyAttributes = new ChangeTrackingCollection<EntityAttributeWrapper>(model.CompanyAttributes.Select(e => new EntityAttributeWrapper(e)));
		RegisterCollection(CompanyAttributes, model.CompanyAttributes);

		if (model.CompanyAddresses == null)
		{
			throw new ArgumentException("CompanyAddresses cannot be null");
		}
 
		CompanyAddresses = new ChangeTrackingCollection<AddressWrapper>(model.CompanyAddresses.Select(e => new AddressWrapper(e)));
		RegisterCollection(CompanyAddresses, model.CompanyAddresses);

	}
	}
}
