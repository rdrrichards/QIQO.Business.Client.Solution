using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Entities;
using QIQO.Business.Client.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Transactions;
using QIQO.Business.Client.Core.Infrastructure;

namespace QIQO.Business.Module.Product.ViewModels
{
    public class ProductViewModel : ViewModelBase, IRegionMemberLifetime
    {
        private readonly IEventAggregator event_aggregator;
        private readonly IServiceFactory service_factory;
        private readonly IProductListService product_service;
        private readonly IFeeScheduleEntityService fee_schedule_service;
        private BindingGroup _updateBindingGroup;
        private string _viewTitle = "Products";
        private string _loadingLabel = "";
        private string _header_msg = "Products";
        private bool _is_loading;
        private string _filterText = "";

        public ProductViewModel(IEventAggregator event_aggtr, IServiceFactory service_fctry,
            IProductListService prod_service, IFeeScheduleEntityService fee_sched_serv)
        {
            event_aggregator = event_aggtr;
            service_factory = service_fctry;
            product_service = prod_service;
            fee_schedule_service = fee_sched_serv;
            BindCommands();
            GetProducts();

            UpdateBindingGroup = new BindingGroup { Name = "Group1" };
            RegisterApplicationCommands();
            event_aggregator.GetEvent<ProductLoadedEvent>().Publish(string.Empty);
        }

        public bool KeepAlive { get; } = true;


        public override string ViewTitle { get { return _viewTitle; } }

        public string LoadingLabel
        {
            get { return _loadingLabel; }
            private set { SetProperty(ref _loadingLabel, value); }
        }

        public string FilterText
        {
            get { return _filterText; }
            set { SetProperty(ref _filterText, value); InvalidateCommands(); }
        }

        //IsLoading
        public bool IsLoading
        {
            get { return _is_loading; }
            set { SetProperty(ref _is_loading, value); }
        }

        public string HeaderMessage
        {
            get { return _header_msg; }
            private set { SetProperty(ref _header_msg, value); }
        }

        private ObservableCollection<ProductWrapper> _products;
        private List<AttributeType> _prod_att_list;

        public ObservableCollection<ProductWrapper> Products
        {
            get { return _products; }
            private set { SetProperty(ref _products, value); }
        }
        private ObservableCollection<FeeSchedule> _selectedProductFeeSchedules;
        public ObservableCollection<FeeSchedule> FeeSchedules
        {
            get { return _selectedProductFeeSchedules; }
            set { SetProperty(ref _selectedProductFeeSchedules, value); }
        }
        private object _selectedProduct;
        public object SelectedProduct
        {
            get { return _selectedProduct; }
            set {
                SetProperty(ref _selectedProduct, value);
                // fetch fee schedule for this product
                LoadFeeSchedules();
            }
        }
        public int SelectedProductIndex { get; set; }

        private object _selectedFeeSchedule;
        public object SelectedFeeSchedule
        {
            get { return _selectedFeeSchedule; }
            set { SetProperty(ref _selectedFeeSchedule, value); }
        }
        public int SelectedFeeScheduleIndex { get; set; }

        private object _selectedAttribute;
        public object SelectedAttribute
        {
            get { return _selectedAttribute; }
            set { SetProperty(ref _selectedAttribute, value); }
        }
        public int SelectedAttributeIndex { get; set; }

        public BindingGroup UpdateBindingGroup
        {
            get { return _updateBindingGroup; }
            private set { SetProperty(ref _updateBindingGroup, value); }
        }

        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand AddProductCommand { get; set; }
        public DelegateCommand DeleteProductCommand { get; set; }
        public DelegateCommand FilterCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }

        protected override void DisplayErrorMessage(Exception ex, [CallerMemberName] string methodName = "")
        {
            event_aggregator.GetEvent<GeneralErrorEvent>().Publish(methodName + " - " + ex.Message);
        }
        protected void DisplayErrorMessage(string msg)
        {
            event_aggregator.GetEvent<GeneralErrorEvent>().Publish(msg);
        }

        private void BindCommands()
        {
            CancelCommand = new DelegateCommand(DoCancel, CanCancel);
            SaveCommand = new DelegateCommand(DoSave, CanSave);
            AddProductCommand = new DelegateCommand(NewProduct);
            DeleteProductCommand = new DelegateCommand(DeleteProduct, CanDelete);
            FilterCommand = new DelegateCommand(DoFilter, CanFilter);
            RefreshCommand = new DelegateCommand(GetProducts);
        }

        private bool CanFilter()
        {
            return (FilterText.Length > 0);
        }

        private void DoFilter()
        {
            try
            {
                HeaderMessage = "Products (Filtering...)";
                IsLoading = true;

                string filter = FilterText.ToLower();

                var products = product_service.ProductList.Where(p => p.ProductDesc.ToLower().Contains(filter)
                    || p.ProductName.ToLower().Contains(filter)
                    || p.ProductNameLong.ToLower().Contains(filter)
                    || p.ProductNameShort.ToLower().Contains(filter)
                    || p.ProductCode.ToLower().Contains(filter)).ToList();

                ObservableCollection<ProductWrapper> prods = new ObservableCollection<ProductWrapper>();

                foreach (Client.Entities.Product product in products)
                {
                    ProductWrapper prod_wrapper = new ProductWrapper(product);
                    prod_wrapper.PropertyChanged += Context_PropertyChanged;
                    prods.Add(prod_wrapper);
                }

                Products = prods; // new ObservableCollection<ProductWrapper>(prods.OrderBy(p => p.ProductType).ThenBy(p => p.ProductName).ToList());
                HeaderMessage = "Products (" + Products.Count.ToString() + ")";
                IsLoading = false;
                LoadingLabel = "";
            }
            catch
            {
                //DisplayErrorMessage(ex.Message);
                HeaderMessage = "Products (0)";
                IsLoading = false;
                LoadingLabel = "";
                return;
            }
        }

        private void RegisterApplicationCommands()
        {
            ApplicationCommands.SaveProductCommand.RegisterCommand(SaveCommand);
            ApplicationCommands.DeleteProductCommand.RegisterCommand(DeleteProductCommand);
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            ApplicationCommands.SaveProductCommand.UnregisterCommand(SaveCommand);
            ApplicationCommands.DeleteProductCommand.UnregisterCommand(DeleteProductCommand);
        }

        private void Context_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            InvalidateCommands();
        }

        private void InvalidateCommands()
        {
            SaveCommand.RaiseCanExecuteChanged();
            AddProductCommand.RaiseCanExecuteChanged();
            CancelCommand.RaiseCanExecuteChanged();
            DeleteProductCommand.RaiseCanExecuteChanged();
            FilterCommand.RaiseCanExecuteChanged();
        }

        private void GetProducts()
        {
            LoadingLabel = "Loading products from service...";
            HeaderMessage = "Products (Loading...)";
            IsLoading = true;

            //ObservableCollection<ProductWrapper> prods = new ObservableCollection<ProductWrapper>();

            try
            {
                List<Client.Entities.Product> products = product_service.ProductList;
                ObservableCollection<ProductWrapper> prods = new ObservableCollection<ProductWrapper>();

                foreach (Client.Entities.Product product in products)
                {
                    ProductWrapper prod_wrapper = new ProductWrapper(product);
                    prod_wrapper.PropertyChanged += Context_PropertyChanged;
                    prods.Add(prod_wrapper);
                }

                Products = prods; // new ObservableCollection<ProductWrapper>(prods.OrderBy(p => p.ProductType).ThenBy(p => p.ProductName).ToList());
                HeaderMessage = "Products (" + Products.Count.ToString() + ")";
                DisplayErrorMessage("Product list populated successfully");
            }
            catch (Exception ex)
            {
                DisplayErrorMessage(ex.Message);
                HeaderMessage = "Products (0)";
                return;
            }

            //IProductService product_service = service_factory.CreateClient<IProductService>();
            //using (product_service)
            //{
            //    try
            //    {
            //        Task<List<Client.Entities.Product>> products = product_service.GetProductsAsync((Company)CurrentCompany);
            //        await products;
            //        ObservableCollection<ProductWrapper> prods = new ObservableCollection<ProductWrapper>(product_service.ProductList);

            //        foreach (Client.Entities.Product product in products.Result)
            //        {
            //            ProductWrapper prod_wrapper = new ProductWrapper(product);
            //            prod_wrapper.PropertyChanged += Context_PropertyChanged;
            //            prods.Add(prod_wrapper);
            //        }

            //        Products = new ObservableCollection<ProductWrapper>(prods.OrderBy(p => p.ProductType).ThenBy(p => p.ProductName).ToList());
            //        DisplayErrorMessage("Product list populated successfully");
            //    }
            //    catch (Exception ex)
            //    {
            //        DisplayErrorMessage(ex.Message);
            //        return;
            //    }
            //}
            IsLoading = false;
            LoadingLabel = "";
        }

        private void DoCancel()
        {
            if (SelectedProductIndex == -1)
                SelectedProduct = null;

            event_aggregator.GetEvent<ProductNewProductCancelEvent>().Publish(ViewNames.ProductHomeView);
        }

        private void DoSave()
        {
            ExecuteFaultHandledOperation(() =>
            {
                var product = SelectedProduct as ProductWrapper;
                IProductService product_service = service_factory.CreateClient<IProductService>();
                IEntityProductService entity_product_service = service_factory.CreateClient<IEntityProductService>();
                using (TransactionScope scope = new TransactionScope()) // TransactionScopeAsyncFlowOption.Enabled
                {
                    using (product_service)
                    {
                        //*** Make sure the attributes are set before the save happens!!
                        int ret_val = product_service.CreateProduct(product.Model);
                        // We need to create a relationship in the entity product table too, or adding new products is a waste of time.
                        if (product.ProductKey == 0)
                        {
                            EntityProduct ent_prod = new EntityProduct()
                            {
                                EntityProductEntityKey = CurrentCompanyKey,
                                EntityProductEntityTypeKey = QIQOEntityType.Company,
                                ProductKey = ret_val,
                                EntityProductSeq = 1,
                                ProductType = product.ProductType,
                                Comment = string.Format($"New product {product.ProductName} (Key: {ret_val.ToString()}) entry for company {CurrentCompanyName}")
                            };

                            using (entity_product_service)
                            {
                                int ep_ret = entity_product_service.CreateEntityProduct(ent_prod);
                            }
                            product.ProductKey = ret_val;
                        }

                        product.AcceptChanges();
                        //Products.Add(product);
                        AddNewProductToCache(product);
                        SelectedProduct = null;
                        RaisePropertyChanged("Products");
                    }
                    scope.Complete();
                }
                event_aggregator.GetEvent<ProductNewProductCompleteEvent>().Publish(ViewNames.ProductHomeView);
            });
        }

        private void NewProduct()
        {
            SelectedProduct = null;
            GetProductAttributes();
            Client.Entities.Product product = new Client.Entities.Product();

            var price_att_type = _prod_att_list.Where(item => item.AttributeTypeCode == "PRODBASE").FirstOrDefault();
            var cost_att_type = _prod_att_list.Where(item => item.AttributeTypeCode == "PRODCOST").FirstOrDefault();
            var qty_att_type = _prod_att_list.Where(item => item.AttributeTypeCode == "PRODDFQTY").FirstOrDefault();
            var inc_att_type = _prod_att_list.Where(item => item.AttributeTypeCode == "INCACCT").FirstOrDefault();
            var exp_att_type = _prod_att_list.Where(item => item.AttributeTypeCode == "EXPACCT").FirstOrDefault();

            EntityAttribute price = new EntityAttribute()
            {
                AttributeDataType = QIQOAttributeDataType.Money,
                AttributeDataTypeKey = (int)QIQOAttributeDataType.Money,
                AttributeDisplayFormat = price_att_type.AttributeDefaultFormat,
                AttributeType = QIQOAttributeType.Product_PRODBASE,
                AttributeValue = "30.00",
                EntityKey = 0,
                EntityType = QIQOEntityType.Product,
                AttributeKey = 0,
                AttributeTypeData = price_att_type
            };

            product.ProductAttributes.Add(price);

            EntityAttribute cost = new EntityAttribute()
            {
                AttributeDataType = QIQOAttributeDataType.Money,
                AttributeDataTypeKey = (int)QIQOAttributeDataType.Money,
                AttributeDisplayFormat = cost_att_type.AttributeDefaultFormat,
                AttributeType = QIQOAttributeType.Product_PRODCOST,
                AttributeValue = "10.00",
                EntityKey = 0,
                EntityType = QIQOEntityType.Product,
                AttributeKey = 0,
                AttributeTypeData = cost_att_type
            };

            product.ProductAttributes.Add(cost);

            EntityAttribute qty = new EntityAttribute()
            {
                AttributeDataType = QIQOAttributeDataType.Number,
                AttributeDataTypeKey = (int)QIQOAttributeDataType.Number,
                AttributeDisplayFormat = qty_att_type.AttributeDefaultFormat,
                AttributeType = QIQOAttributeType.Product_PRODDFQTY,
                AttributeValue = "1",
                EntityKey = 0,
                EntityType = QIQOEntityType.Product,
                AttributeKey = 0,
                AttributeTypeData = qty_att_type
            };

            product.ProductAttributes.Add(qty);

            EntityAttribute inc_acct = new EntityAttribute()
            {
                AttributeDataType = QIQOAttributeDataType.String,
                AttributeDataTypeKey = (int)QIQOAttributeDataType.String,
                AttributeDisplayFormat = inc_att_type.AttributeDefaultFormat,
                AttributeType = QIQOAttributeType.Product_INCACCT,
                AttributeValue = "4000",
                EntityKey = 0,
                EntityType = QIQOEntityType.Product,
                AttributeKey = 0,
                AttributeTypeData = inc_att_type
            };

            product.ProductAttributes.Add(inc_acct);

            EntityAttribute exp_acct = new EntityAttribute()
            {
                AttributeDataType = QIQOAttributeDataType.String,
                AttributeDataTypeKey = (int)QIQOAttributeDataType.String,
                AttributeDisplayFormat = exp_att_type.AttributeDefaultFormat,
                AttributeType = QIQOAttributeType.Product_EXPACCT,
                AttributeValue = "4300",
                EntityKey = 0,
                EntityType = QIQOEntityType.Product,
                AttributeKey = 0,
                AttributeTypeData = exp_att_type
            };

            product.ProductAttributes.Add(exp_acct);

            ProductWrapper new_product = new ProductWrapper(product);
            new_product.PropertyChanged += Context_PropertyChanged;
            SelectedProduct = new_product;
            event_aggregator.GetEvent<ProductNewProductAddEvent>().Publish(ViewNames.ProductHomeView);
        }

        private void DeleteProduct()
        {
            if (SelectedProductIndex != -1)
            {
                ExecuteFaultHandledOperation(() =>
                {
                    var product = SelectedProduct as ProductWrapper;
                    IProductService product_service = service_factory.CreateClient<IProductService>();
                    using (TransactionScope scope = new TransactionScope()) // TransactionScopeAsyncFlowOption.Enabled
                    {
                        using (product_service)
                        {
                            bool ret_val = product_service.DeleteProduct(product.Model);
                            SelectedProduct = null;
                        }
                        Products.Remove(product);
                        scope.Complete();
                    }
                });
            }
            else
                SelectedProduct = null;
        }

        private bool CanDelete()
        {
            ProductWrapper product = _selectedProduct as ProductWrapper;
            if (product != null)
                return true;
            else
                return false;
        }

        private bool CanSave()
        {
            ProductWrapper curr_prod = SelectedProduct as ProductWrapper;
            if (curr_prod != null)
            {
                if (curr_prod.IsValid && curr_prod.IsChanged)
                    return true;
                else
                    return false;
            }
            return false;
        }

        private bool CanCancel()
        {
            return true;
        }

        private void GetProductAttributes()
        {
            ExecuteFaultHandledOperation(() =>
            {
                ITypeService type_service = service_factory.CreateClient<ITypeService>();
                using (type_service)
                {
                    _prod_att_list = type_service.GetAttributeTypeListByCategory("Product");
                }
            });
        }

        private void AddNewProductToCache(ProductWrapper new_prod)
        {
            var prod = product_service.ProductList.Where(p => p.ProductKey == new_prod.ProductKey).FirstOrDefault();
            if (prod == null)
            {
                product_service.ProductList.Add(new_prod.Model);
                GetProducts();
            }
        }

        private void LoadFeeSchedules()
        {
            var curr_prod = SelectedProduct as ProductWrapper;
            if (curr_prod != null)
            {
                var prod_fees = fee_schedule_service.GetFeeSchedules(curr_prod.Model);
                FeeSchedules = new ObservableCollection<FeeSchedule>(prod_fees);
            }
        }
    }
}
