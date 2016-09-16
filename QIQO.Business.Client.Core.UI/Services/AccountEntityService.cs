using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Business.Client.Core.UI
{
    public class AccountEntityService : IAccountEntityService
    {
        private ITypeService _type_service;

        // AccountEntityService
        public AccountEntityService(ITypeService type_service)
        {
            _type_service = type_service;
        }

        public Account InitNewAccount(int company_key)
        {
            Account account = new Account();
            account.CompanyKey = company_key;


            var shipping_addr = new Address() { AddressType = QIQOAddressType.Shipping };
            var billing_addr = new Address() { AddressType = QIQOAddressType.Billing };
            var mailing_addr = new Address() { AddressType = QIQOAddressType.Mailing };

            account.Addresses.Add(shipping_addr);
            account.Addresses.Add(billing_addr);
            account.Addresses.Add(mailing_addr);
            account.AccountAttributes = GetAccountAttributes();
            account.Employees.Add(new AccountPerson()
            {
                PersonCode = "EMP1",
                PersonFirstName = "FirstName",
                PersonLastName = "LastName",
                PersonMI = "M",
                CompanyRoleType = QIQOPersonType.AccountContact,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddYears(10),
                PersonDOB = DateTime.Parse("1970-01-01")
            });
            account.Contacts.Add(new Contact()
            {
                ContactType = QIQOContactType.BusinessPhone,
                ContactValue = "555-555-1234",
                ContactActiveFlg = 1,
                ContactDefaultFlg = 1
            });

            return account;
        }
        public Account InitNewAccount(Company company)
        {
            if (company == null)
                throw new ArgumentNullException(nameof(company));

            return InitNewAccount(company.CompanyKey);
        }

        private List<EntityAttribute> GetAccountAttributes()
        {
            List<AttributeType> atttype_list = _type_service.GetAttributeTypeList();
            var acct_atts = atttype_list.Where(item => item.AttributeTypeCategory == "Account").ToList();
            var gcnt_atts = atttype_list.Where(item => item.AttributeTypeCategory == "General Contact").ToList();
            var acnt_atts = atttype_list.Where(item => item.AttributeTypeCategory == "Account Contact").ToList();

            var all_atts = acct_atts.Concat(gcnt_atts.Concat(acnt_atts));

            List<EntityAttribute> available_attributes = new List<EntityAttribute>();

            foreach (AttributeType attype in all_atts)
            {
                EntityAttribute ent_att = new EntityAttribute()
                {
                    AttributeDataTypeKey = (int)attype.AttributeDataTypeKey,
                    AttributeDisplayFormat = attype.AttributeDefaultFormat,
                    AttributeKey = 0,
                    AttributeType = (QIQOAttributeType)attype.AttributeTypeKey,
                    AttributeValue = "",
                    EntityKey = 0,
                    EntityType = QIQOEntityType.Account,
                    AttributeTypeData = attype,
                    AttributeDataType = attype.AttributeDataTypeKey,
                    EntityTypeData = new EntityType()
                };

                if (ent_att.AttributeType == QIQOAttributeType.Account_ORDNUM & ent_att.AttributeValue == "")
                    ent_att.AttributeValue = "0";
                if (ent_att.AttributeType == QIQOAttributeType.Account_ORDNUMPAT & ent_att.AttributeValue == "")
                    ent_att.AttributeValue = ent_att.AttributeDisplayFormat;

                if (ent_att.AttributeType == QIQOAttributeType.Account_INVNUM & ent_att.AttributeValue == "")
                    ent_att.AttributeValue = "0";
                if (ent_att.AttributeType == QIQOAttributeType.Account_INVNUMPAT & ent_att.AttributeValue == "")
                    ent_att.AttributeValue = ent_att.AttributeDisplayFormat;

                available_attributes.Add(ent_att);
            }
            return available_attributes;
        }
    }
}
