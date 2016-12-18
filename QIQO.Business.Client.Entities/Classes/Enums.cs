namespace QIQO.Business.Client.Entities
{
    public enum QIQOEntityType
    {
        //entity_type_key	entity_type_code	entity_type_name
        //1	            COMP	            Company
        //2	            PERS	            Person
        //3	            ACCT	            Account
        //4	            PRD	                Product
        //5	            ORD	                Order
        //6	            ORD_ITEM	        Order Item
        //7	            INV	                Invoice
        //8	            INV_ITEM	        Invoice Item
        //9	            GL	                General Ledger
        //10	            GL_ITEM	            General Ledger Txn
        //11	            VEND	            Vendor

        //Unknown = 0,
        Company = 1,
        Person = 2,
        Account = 3,
        Product = 4,
        Order = 5,
        OrderItem = 6,
        Invoice = 7,
        InvoiceItem = 8,
        GeneralLedger = 9,
        GeneralLedgerTxn = 10,
        Vendor = 11,
        Manager = 12,
        CoWorker = 13
    }

    public enum QIQOEntityNumberType
    {
        InvoiceNumber = 1,
        OrderNumber = 2,
        EmployeeNumber = 3,
        AccountNumber = 4,
        VendorNumber = 5,
        ContactNumber = 6
    }

    public enum QIQOAccountType
    {
        //Unknown = 0,
        TestAccount = 1,
        Business = 2,
        Individual = 3
    }

    public enum QIQOAddressType
    {
        //Unknown = 0,
        Mailing = 1,
        Shipping = 2,
        Billing = 3
    }

    public enum QIQOAttributeType
    {
        //Unknown = 0,

        Account_INVNUM = 1,
        Account_INVNUMPAT = 2,
        Account_ORDNUM = 3,
        Account_ORDNUMPAT = 4,
        Account_URLCOM = 19,
        Account_URLINFO = 18,

        AccountEmployee_EMAILCO = 21,
        AccountEmployee_EMAILPER = 22,
        AccountEmployee_LANG = 23,

        AccountRepresentative_REGION1 = 24,
        AccountRepresentative_REGION2 = 25,
        AccountRepresentative_REGION3 = 26,

        Company_ACCTNUM = 8,
        Company_ACCTNUMPAT = 9,
        Company_EMPNUM = 10,
        Company_EMPNUMPAT = 11,
        Company_URLCOM = 17,
        Company_URLINFO = 16,
        Company_VENDORNUM = 12,
        Company_VENDORNUMP = 13,

        Employee_DEFAULTCO = 7,
        Employee_LANG = 20,
        Employee_LOGIN = 5,
        Employee_SSN = 6,

        Product_PRODBASE = 30,
        Product_PRODCOST = 31,
        Product_PRODDFQTY = 32,
        Product_INCACCT = 53,
        Product_EXPACCT = 54,

        SalesRepresentative_REGION1 = 27,
        SalesRepresentative_REGION2 = 28,
        SalesRepresentative_REGION3 = 29,

        GeneralContact_PHN_HOME = 33,
        GeneralContact_PHN_CELL = 34,
        GeneralContact_PHN_WORK = 35,
        GeneralContact_PHN_PAGR = 36,
        GeneralContact_PHN_FAX = 37,
        GeneralContact_PHN_OTH = 38,
        GeneralContact_EMAIL_PERS = 39,
        GeneralContact_EMAIL_WRKP = 40,
        GeneralContact_EMAIL_WRKS = 41,
        GeneralContact_SKYPE = 42,
        GeneralContact_FACEB_URL = 43,
        GeneralContact_EM_OPTOUT = 44,

        CompanyContact_CNCT_MAIN = 45,
        CompanyContact_CNCT_SEC = 46,
        CompanyContact_CNCT_OTH = 47,

        AccountContact_CNCT_MAIN = 48,
        AccountContact_CNCT_SEC = 49,
        AccountContact_CNCT_OTH = 50,
    }

    public enum QIQOAttributeDataType
    {
        Number = 1,
        String = 2,
        Money = 3
    }

    public enum QIQOInvoiceStatus
    {
        //Unknown = 0,
        New = 1,
        InProcess = 2,
        PendingPayment = 3,
        Complete = 4,
        Canceled = 5
    }

    public enum QIQOInvoiceItemStatus
    {
        //Unknown = 0,
        New = 6,
        InProcess = 7,
        PendingPayment = 8,
        Complete = 9,
        Canceled = 10
    }

    public enum QIQOOrderStatus
    {
        //Unknown = 0,
        Scheduled = 1,
        Open = 2,
        InProcess = 3,
        Fulfilled = 4,
        PendingPayment = 5,
        Complete = 6,
        Canceled = 13
    }

    public enum QIQOOrderItemStatus
    {
        //Unknown = 0,
        Scheduled = 7,
        Open = 8,
        InProcess = 9,
        Fulfilled = 10,
        PendingPayment = 11,
        Complete = 12,
        Canceled = 14
    }

    public enum QIQOPersonType
    {
        //Unknown = 0,
        EmployeeHourly = 1,
        EmployeeSalary = 2,
        SalesRepresentative = 3,
        AccountRepresentative = 4,
        AccountOwner = 5,
        AccountEmployee = 6,
        AccountContact = 7,
        VendorRepresentative = 8
    }

    public enum QIQOProductType
    {
        //Unknown = 0,
        Sweet9 = 1,
        Sweet10 = 2,
        Sweet6 = 3,
        Sweet8 = 4,
        Savory4 = 5,
        Savory6 = 6,
        Savory8 = 7,
        SweetMini = 8
    }

    public enum QIQOContactType
    {
        AccountContact = 1, //ACNT
        CellPhone = 2, //PHNCELL
        HomePhone = 3, //PHNHOME
        BusinessPhone = 4, //PHNBUS
        WorkPhone = 5, //PHNWORK
        OtherPhone = 6, //PHNOTH1
        WorkFax = 7, //FAXWORK
        PersonalFax = 8, //FAXPERS
        WorkEmail = 9, //EMAILWRK
        PersonalEmail = 10, //EMAILPERS
        OtherEmail = 11, //EMAILOTH1
        InstanceMessenger = 12 // IM1
    }

    public enum QIQOCommentType
    {
        Public = 1,
        Private = 2
    }
}
