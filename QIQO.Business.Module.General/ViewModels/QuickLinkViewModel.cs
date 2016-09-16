using QIQO.Business.Client.Core.UI;
using System.Collections.ObjectModel;
using QIQO.Business.Client.Core.Infrastructure;
using Prism.Commands;
using System;
using Prism.Regions;

namespace QIQO.Business.Module.General.ViewModels
{
    public class QuickLinkViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        private string _header_msg = "Quick Links";
        private ObservableCollection<ActionItem> _actions;
        private object _selected_action;

        public QuickLinkViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            InitializeActions();
            DoActionCommand = new DelegateCommand(DoAction);
        }

        private void DoAction()
        {
            var action = SelectedAction as ActionItem;
            if (action != null)
                _regionManager.RequestNavigate(RegionNames.ContentRegion, action.ActionUri);
        }

        private void InitializeActions()
        {
            var actions = new ObservableCollection<ActionItem>();
            actions.Add(new ActionItem() { ActionName = "New Order", ActionUri = ViewNames.OrderViewX });
            actions.Add(new ActionItem() { ActionName = "New Invoice", ActionUri = ViewNames.InvoiceViewX });
            actions.Add(new ActionItem() { ActionName = "Find Order", ActionUri = ViewNames.OrderHomeView });
            actions.Add(new ActionItem() { ActionName = "Find Invoice", ActionUri = ViewNames.InvoiceHomeView });
            QuickListItems = actions;
        }

        public DelegateCommand DoActionCommand { get; set; }
        public int SelectedActionIndex { get; set; }
        public object SelectedAction
        {
            get { return _selected_action; }
            set { SetProperty(ref _selected_action, value); }
        }

        public ObservableCollection<ActionItem> QuickListItems
        {
            get { return _actions; }
            private set { SetProperty(ref _actions, value); }
        }

        public string HeaderMessage
        {
            get { return _header_msg; }
            private set { SetProperty(ref _header_msg, value); }
        }
    }

    public class ActionItem
    {
        public string ActionName { get; set; }
        public string ActionUri { get; set; }
    }
}
