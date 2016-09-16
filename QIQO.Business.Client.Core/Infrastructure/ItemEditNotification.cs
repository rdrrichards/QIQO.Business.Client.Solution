using Prism.Interactivity.InteractionRequest;

namespace QIQO.Business.Client.Core
{
    public class ItemEditNotification : Confirmation
    {
        public ItemEditNotification()
        {
            EditibleObject = null;
        }

        public ItemEditNotification(object object_to_edit)
        {
            EditibleObject = object_to_edit;
        }

        public object EditibleObject { get; set; }
    }
}