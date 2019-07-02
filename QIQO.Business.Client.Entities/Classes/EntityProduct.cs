namespace QIQO.Business.Client.Entities
{
    public class EntityProduct : Product
    {
        //public int EntityProductKey { get; set; }

        //These next 5 properties make a unique row 
        public int EntityProductKey { get; set; }
        public QIQOProductType EntityProductType { get; set; } = QIQOProductType.Sweet9;
        public int EntityProductSeq { get; set; }

        public int EntityProductEntityKey { get; set; }
        public QIQOEntityType EntityProductEntityTypeKey { get; set; } = QIQOEntityType.Account;
        public EntityType EntityProductEntityTypeData { get; set; } = new EntityType();

        // I expect this to be emplty most of the time
        public string Comment { get; set; }
    }
}
