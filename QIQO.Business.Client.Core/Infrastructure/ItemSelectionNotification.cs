using Prism.Interactivity.InteractionRequest;
using Prism.Services.Dialogs;

namespace QIQO.Business.Client.Core
{
    //public class ItemSelectionNotification : IDialogResult
    //{
    //    public ItemSelectionNotification() { }
    //    public ItemSelectionNotification(object payload)
    //    {
    //        Payload = payload;
    //    }
    //    public object SelectedItem { get; set; }
    //    public object Payload { get; } = null;

    //    public IDialogParameters Parameters => throw new System.NotImplementedException();

    //    public ButtonResult Result => throw new System.NotImplementedException(); 
    //}
    public class ItemSelectionNotification : Confirmation
    {
        public ItemSelectionNotification() { }
        public ItemSelectionNotification(object payload)
        {
            Payload = payload;
        }
        public object SelectedItem { get; set; }
        public object Payload { get; } = null;
    }

}
