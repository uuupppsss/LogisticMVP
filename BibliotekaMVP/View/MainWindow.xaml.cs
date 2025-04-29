using BibliotekaMVP.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BibliotekaMVP.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LogisticsContext _context;
        private Order _selectedOrder;
        private Product _selectedProduct;
        private Transport _selectedTransport;
        private Route _selectedRoute;
        
        public MainWindow()
        {
            InitializeComponent();
            _context = new LogisticsContext();
            //try
            //{
            //    //_context.Database.EnsureCreated();
            //}
            //catch
            //{
            //    //RestartApplication();
            //}
            LoadOrders();
            LoadProducts();
            LoadTransports();
            LoadRoutes();
        }

        //private void RestartApplication()
        //{
        //    string exePath = Process.GetCurrentProcess().MainModule.FileName;

        //    Process.Start(exePath);

        //    Application.Current.Shutdown();
        //}

        //заказы
        private async void LoadOrders(string searchTerm = null)
        {
            var orders = await _context.GetOrders();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                orders = orders.Where(o => o.CustomerName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            OrdersDataGrid.ItemsSource = orders;
            var statuses = await _context.GetOrderStatuses();
            StatusListBox.ItemsSource = statuses;
        }

        private void OrdersDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
           
            try
            {
                if (OrdersDataGrid.SelectedItem != null)
                {
                    _selectedOrder = (Order)OrdersDataGrid.SelectedItem;

                    if (_selectedOrder != null)
                    {
                        CustomerNameTextBox.Text = _selectedOrder.CustomerName;
                        OrderDatePicker.SelectedDate = _selectedOrder.OrderDate;
                        StatusListBox.SelectedItem = _selectedOrder.Status;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newOrder = new Order
                {
                    CustomerName = CustomerNameTextBox.Text,
                    OrderDate = OrderDatePicker.SelectedDate ?? DateTime.Now,
                    Status=(string)StatusListBox.SelectedItem
                };

                if(newOrder.CustomerName!=null&&newOrder.Status!=null)
                {
                    await _context.AddOrder(newOrder);
                    LoadOrders();
                    ClearFields();
                }
                
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private async void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (_selectedOrder != null && CustomerNameTextBox.Text != null && StatusListBox.SelectedItem != null)
                {
                    _selectedOrder.CustomerName = CustomerNameTextBox.Text;
                    _selectedOrder.OrderDate = OrderDatePicker.SelectedDate ?? DateTime.Now;
                    _selectedOrder.Status = (string)StatusListBox.SelectedItem;
                    await _context.UpdateOrder(_selectedOrder);
                    LoadOrders();
                    ClearFields();

                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (_selectedOrder != null)
                {
                    await _context.DeleteOrder(_selectedOrder.Id);
                    LoadOrders();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                LoadOrders(SearchOrderTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearFields()
        {
            CustomerNameTextBox.Clear();
            OrderDatePicker.SelectedDate = null;
            StatusListBox.SelectedItem = null;
            OrdersDataGrid.SelectedItem = null;
            _selectedOrder = null;
        }

        //продукты
        private async void LoadProducts(string searchTerm = null)
        {
            var products = await _context.GetProducts();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(p => p.ProductName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            ProductsDataGrid.ItemsSource = products;
        }

        private void ProductsDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                if (ProductsDataGrid.SelectedItem != null)
                {
                    _selectedProduct = (Product)ProductsDataGrid.SelectedItem;

                    if (_selectedProduct != null)
                    {
                        ProductNameTextBox.Text = _selectedProduct.ProductName;
                        QuantityTextBox.Text = _selectedProduct.Quantity.ToString();

                        PriceTextBox.Text = _selectedProduct.Price.ToString("F2") + "рублей";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newProduct = new Product
                {
                    ProductName = ProductNameTextBox.Text,
                    Quantity = int.Parse(QuantityTextBox.Text),
                    Price = decimal.Parse(PriceTextBox.Text)
                };
                if (newProduct.ProductName!=null)
                {
                    await _context.AddProduct(newProduct);
                    LoadProducts();
                    ClearProductsFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }

        private async void EditProduct_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                if (_selectedProduct != null&& ProductNameTextBox.Text!=null)
                {
                    _selectedProduct.ProductName = ProductNameTextBox.Text;
                    _selectedProduct.Quantity = int.Parse(QuantityTextBox.Text);
                    _selectedProduct.Price = decimal.Parse(PriceTextBox.Text);
                    await _context.UpdateProduct(_selectedProduct);
                    LoadProducts();
                    ClearProductsFields();
                }

                ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (_selectedProduct != null)
                {
                    await _context.DeleteProduct(_selectedProduct.Id);
                    LoadProducts();
                    ClearProductsFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchProductButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                LoadProducts(SearchProductTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearProductsFields()
        {
            ProductNameTextBox.Clear();
            QuantityTextBox.Clear();
            PriceTextBox.Clear();
            ProductsDataGrid.SelectedItem = null;
            _selectedProduct = null;
        }

        // Транспорт
        private async void LoadTransports(string searchTerm = null)
        {
            var transports = await _context.GetTransports();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                transports = transports.Where
                    (t => t.Brand.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                                    t.Model.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                                    t.LicensePlate.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            TransportDataGrid.ItemsSource = transports;
        }



        private void TransportDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
            try
            {
                if (TransportDataGrid.SelectedItem != null)
                {
                    _selectedTransport = (Transport)TransportDataGrid.SelectedItem;

                    if (_selectedTransport != null)
                    {
                        BrandTextBox.Text = _selectedTransport.Brand;
                        ModelTextBox.Text = _selectedTransport.Model;
                        YearTextBox.Text = _selectedTransport.Year.ToString();
                        LicensePlateTextBox.Text = _selectedTransport.LicensePlate;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void AddTransport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newTransport = new Transport
                {
                    Brand = BrandTextBox.Text,
                    Model = ModelTextBox.Text,
                    Year = int.Parse(YearTextBox.Text),
                    LicensePlate = LicensePlateTextBox.Text
                };

                if(newTransport.Model!=null&&newTransport.Brand!=null&&newTransport.LicensePlate!=null)
                {
                    await _context.AddTransport(newTransport);
                    LoadTransports();
                    ClearTransportFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private async void EditTransport_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                if (_selectedTransport != null&& BrandTextBox.Text != null && ModelTextBox.Text != null && LicensePlateTextBox.Text != null)
                {
                    _selectedTransport.Brand = BrandTextBox.Text;
                    _selectedTransport.Model = ModelTextBox.Text;
                    _selectedTransport.Year = int.Parse(YearTextBox.Text);
                    _selectedTransport.LicensePlate = LicensePlateTextBox.Text;
                    await _context.UpdateTRansport(_selectedTransport);
                    LoadTransports();
                    ClearTransportFields();
                }

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void DeleteTransport_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedTransport != null)
            {
                await _context.DeleteTransport(_selectedTransport.Id);
                LoadTransports();
                ClearTransportFields();
            }
        }

        private void SearchTransportButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                LoadTransports(SearchTransportTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearTransportFields()
        {
            BrandTextBox.Clear();
            ModelTextBox.Clear();
            YearTextBox.Clear();
            LicensePlateTextBox.Clear();
            TransportDataGrid.SelectedItem = null;
            _selectedTransport = null;
        }

        //маршруты
        private async void LoadRoutes(string searchTerm = null)
        {
            var routes = await _context.GetRoutes();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                routes = routes.Where(r => r.StartPoint.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                            r.EndPoint.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            RouteDataGrid.ItemsSource = routes;
            var orders=await _context.GetOrders();
            List<int> ordersIds=orders.Select(r => r.Id).ToList();
            OrderNumbers.ItemsSource= ordersIds;

            var transports=await _context.GetTransports();
            List<int> transportIds=transports.Select(r => r.Id).ToList();
            TransportNumbers.ItemsSource= transportIds;
        }

        private void RouteDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
            try
            {
                if (RouteDataGrid.SelectedItem != null)
                {
                    _selectedRoute = (Route)RouteDataGrid.SelectedItem;

                    if (_selectedRoute != null)
                    {
                        StartPointTextBox.Text = _selectedRoute.StartPoint;
                        EndPointTextBox.Text = _selectedRoute.EndPoint;
                        DistanceTextBox.Text = _selectedRoute.Distance.ToString();
                        TravelTimeTextBox.Text = _selectedRoute.TravelTime.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void AddRoute_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newRoute = new Route
                {
                    StartPoint = StartPointTextBox.Text,
                    EndPoint = EndPointTextBox.Text,
                    Distance = double.Parse(DistanceTextBox.Text),
                    TravelTime = double.Parse(TravelTimeTextBox.Text),
                    IdTransport = (int)TransportNumbers.SelectedItem,
                    IdOrder=(int)OrderNumbers.SelectedItem,
                };

                if (newRoute.StartPoint!=null&&newRoute.EndPoint!=null&&newRoute.IdTransport!=0&&newRoute.IdOrder!=0)
                {
                    await _context.AddRoutes(newRoute);
                    LoadRoutes();
                    ClearRouteFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private async void EditRoute_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                if (_selectedRoute != null&& StartPointTextBox.Text!=null&& EndPointTextBox.Text!=null&&TransportNumbers.SelectedItem!=null&&OrderNumbers.SelectedItem!=null)
                {
                    _selectedRoute.StartPoint = StartPointTextBox.Text;
                    _selectedRoute.EndPoint = EndPointTextBox.Text;
                    _selectedRoute.Distance = double.Parse(DistanceTextBox.Text);
                    _selectedRoute.TravelTime = double.Parse(TravelTimeTextBox.Text);
                    _selectedRoute.IdTransport = (int)TransportNumbers.SelectedItem;
                    _selectedRoute.IdOrder = (int)OrderNumbers.SelectedItem;

                    await _context.UpdateRoute(_selectedRoute);
                    LoadRoutes();
                    ClearRouteFields();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void DeleteRoute_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                if (_selectedRoute != null)
                {
                    await _context.DeleteRoute(_selectedRoute.Id);
                    LoadRoutes();
                    ClearRouteFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchRouteButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                LoadRoutes(SearchRouteTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearRouteFields()
        {
            StartPointTextBox.Clear();
            EndPointTextBox.Clear();
            DistanceTextBox.Clear();
            TravelTimeTextBox.Clear();
            RouteDataGrid.SelectedItem = null;
            _selectedRoute = null;
        }

    }
}

