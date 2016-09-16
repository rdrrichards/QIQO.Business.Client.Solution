using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace QIQO.Business.Client.Core.UI
{
    public static class DataGridChangeBehavior
    {
        public static readonly DependencyProperty IsActiveProperty;

        static DataGridChangeBehavior()
        {
            IsActiveProperty = DependencyProperty.RegisterAttached("IsActive", typeof(bool), typeof(DataGridChangeBehavior), new PropertyMetadata(false, OnIsActivePropertyChanged));
        }

        public static bool GetIsActive(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsActiveProperty);
        }

        public static void SetIsActive(DependencyObject obj, bool value)
        {
            obj.SetValue(IsActiveProperty, value);
        }

        private static void OnIsActivePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = d as DataGrid;
            if (dataGrid != null)
            {
                if ((bool)e.NewValue)
                {
                    dataGrid.Loaded += DataGrid_Loaded;
                }
                else
                {
                    dataGrid.Loaded -= DataGrid_Loaded;
                }
            }
        }

        private static void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var dataGrid = (DataGrid)sender;
            foreach (var textColumn in dataGrid.Columns.OfType<DataGridTextColumn>())
            {
                var binding = textColumn.Binding as Binding;
                if (binding != null)
                {
                    textColumn.EditingElementStyle
                      = CreateEditingElementStyle(dataGrid, binding.Path.Path);

                    textColumn.ElementStyle
                      = CreateElementStyle(dataGrid, binding.Path.Path);
                }
            }
        }

        private static Style CreateEditingElementStyle(DataGrid dataGrid, string bindingPath)
        {
            var baseStyle = dataGrid.FindResource(typeof(TextBox)) as Style;
            var style = new Style(typeof(TextBox), baseStyle);
            AddSetters(style, bindingPath, dataGrid);
            return style;
        }

        private static Style CreateElementStyle(DataGrid dataGrid, string bindingPath)
        {
            var baseStyle = dataGrid.FindResource("TextBlockBaseStyle") as Style;
            var style = new Style(typeof(TextBlock), baseStyle);
            AddSetters(style, bindingPath, dataGrid);
            return style;
        }

        private static void AddSetters(Style style, string bindingPath, DataGrid dataGrid)
        {
            style.Setters.Add(new Setter(ChangeBehavior.IsActiveProperty, false));

            style.Setters.Add(new Setter(ChangeBehavior.IsChangedProperty,
                                           new Binding(bindingPath + "IsChanged")));

            style.Setters.Add(new Setter(ChangeBehavior.OriginalValueProperty,
                                           new Binding(bindingPath + "OriginalValue")));

            style.Setters.Add(new Setter(Validation.ErrorTemplateProperty,
              dataGrid.FindResource("ErrorInsideErrorTemplate")));
        }
    }
}
