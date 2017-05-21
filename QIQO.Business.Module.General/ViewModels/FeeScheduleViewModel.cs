using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Entities;
using QIQO.Business.Client.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace QIQO.Business.Module.General.ViewModels
{
    public class FeeScheduleViewModel : ViewModelBase, IInteractionRequestAware
    {
        IEventAggregator event_aggregator;
        IServiceFactory service_factory;
        IProductListService product_service;
        FeeScheduleWrapper _fee_schedule;

        private ObservableCollection<Product> _productlist;
        private string _viewTitle = "Fee Schedule Add/Edit";
        private ItemEditNotification notification;
        private object _currentSelectedProduct;

        public FeeScheduleViewModel()
        {
            event_aggregator = Unity.Container.Resolve<IEventAggregator>();
            service_factory = Unity.Container.Resolve<IServiceFactory>();
            product_service = Unity.Container.Resolve<IProductListService>();

            GetProductList();
            FeeSchedule = new FeeScheduleWrapper(new FeeSchedule());
            BindCommands();
        }

        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand ProductChangedCommand { get; set; }
        public DelegateCommand FeeTypeChangedCommand { get; set; }

        public override string ViewTitle { get { return _viewTitle; } }

        public ObservableCollection<Product> ProductList
        {
            get { return _productlist; }
            private set { SetProperty(ref _productlist, value); }
        }

        public FeeScheduleWrapper FeeSchedule
        {
            get { return _fee_schedule; }
            private set { SetProperty(ref _fee_schedule, value); }
        }
        public object SelectedProduct
        {
            get { return _currentSelectedProduct; }
            set { SetProperty(ref _currentSelectedProduct, value); InvalidateCommands(); }
        }

        public List<string> FeeScheduleTypes // Get this centralized
        {
            get
            {
                return new List<string>(new string[] { "F", "P"});
            }
        }

        public INotification Notification
        {
            get
            {
                return notification;
            }
            set
            {
                if (value is ItemEditNotification)
                {
                    notification = value as ItemEditNotification;
                    var fee_schedule = notification.EditibleObject as FeeSchedule;
                    if (fee_schedule != null)
                    {
                        FeeSchedule = new FeeScheduleWrapper(fee_schedule); // need to confirm this is enough to isolate the passed in object 
                        FeeSchedule.PropertyChanged += Context_PropertyChanged;
                    }
                    RaisePropertyChanged(nameof(Notification));
                }
            }
        }

        public Action FinishInteraction { get; set; }

        private void BindCommands()
        {
            CancelCommand = new DelegateCommand(DoCancel);
            SaveCommand = new DelegateCommand(DoSave, CanDoSave);
            ProductChangedCommand = new DelegateCommand(UpdateFeeScheduleValue);
            FeeTypeChangedCommand = new DelegateCommand(ValidateFeeScheduleValue);
        }

        private void ValidateFeeScheduleValue()
        {
            if (FeeSchedule.FeeScheduleTypeCode == "F" && FeeSchedule.FeeScheduleValue <= 1M)
            {
                var curr_product = SelectedProduct as Product;
                UpdateFeeScheduleValueFromProductBase(curr_product);
            }

            if (FeeSchedule.FeeScheduleTypeCode == "P" && FeeSchedule.FeeScheduleValue == 0M)
                FeeSchedule.FeeScheduleValue = .99M;

            if (FeeSchedule.FeeScheduleTypeCode == "P" && FeeSchedule.FeeScheduleValue > 1M)
                FeeSchedule.FeeScheduleValue = .99M;
        }

        private void UpdateFeeScheduleValue()
        {
            var curr_product = SelectedProduct as Product;
            if (curr_product != null && FeeSchedule.FeeScheduleValue == 0)
            {
                FeeSchedule.ProductKey = curr_product.ProductKey;
                FeeSchedule.ProductCode = curr_product.ProductCode;
                FeeSchedule.ProductDesc = curr_product.ProductDesc;
                UpdateFeeScheduleValueFromProductBase(curr_product);
            }
        }

        private void UpdateFeeScheduleValueFromProductBase(Product curr_product)
        {
            var dp = curr_product.ProductAttributes.Where(item => item.AttributeType == QIQOAttributeType.Product_PRODBASE).FirstOrDefault();
            FeeSchedule.FeeScheduleValue = decimal.Round(decimal.Parse(dp.AttributeValue), 2);
        }

        private void Context_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            InvalidateCommands();
        }

        private void InvalidateCommands()
        {
            SaveCommand.RaiseCanExecuteChanged();
            CancelCommand.RaiseCanExecuteChanged();
        }

        private void DoCancel()
        {
            FeeSchedule = null;
            // Close window
        }

        private bool CanDoSave()
        {
            if (FeeSchedule.FeeScheduleKey == 0)
                return !HasErrors;
            else
                return FeeSchedule.IsChanged && FeeSchedule.IsValid;
        }

        private void DoSave()
        {
            FeeSchedule.AcceptChanges();
            notification.EditibleObject = FeeSchedule.Model;
            notification.Confirmed = true;
            FinishInteraction();
        }

        private void GetProductList() //Task<ObservableCollection<Product>>
        {
            ProductList = new ObservableCollection<Product>(product_service.ProductList);
        }
    }
}
