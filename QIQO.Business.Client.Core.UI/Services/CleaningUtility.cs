using QIQO.Business.Client.Entities;

namespace QIQO.Business.Client.Core.UI
{
    public class CleaningUtility : ICleaningUtility
    {
        public void CleanAddress(Address address)
        {
            address.AddressLine1 = (address.AddressLine1 == null) ? string.Empty : address.AddressLine1;
            address.AddressLine2 = (address.AddressLine2 == null) ? string.Empty : address.AddressLine2;
            address.AddressLine3 = (address.AddressLine3 == null) ? string.Empty : address.AddressLine3;
            address.AddressLine4 = (address.AddressLine4 == null) ? string.Empty : address.AddressLine4;
            address.AddressCity = (address.AddressCity == null) ? string.Empty : address.AddressCity;
            address.AddressState = (address.AddressState == null) ? string.Empty : address.AddressState;
            address.AddressPostalCode = (address.AddressPostalCode == null) ? string.Empty : address.AddressPostalCode;
            address.AddressCounty = (address.AddressCounty == null) ? string.Empty : address.AddressCounty;
            address.AddressCountry = (address.AddressCountry == null) ? string.Empty : address.AddressCountry;
            address.AddressNotes = (address.AddressNotes == null) ? string.Empty : address.AddressNotes;
            address.AddressActiveFlag = true;
            address.AddressDefaultFlag = true;
            address.AddressKey = address.AddressKey;
        }
    }
}
