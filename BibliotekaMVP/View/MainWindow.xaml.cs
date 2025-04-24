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
            _context = new LogisticsContext("TestDataBase123456789");
            try
            {
                _context.Database.EnsureCreated();
            }
            catch
            {
                RestartApplication();
            }
            LoadOrders();
            LoadProducts();
            LoadTransports();
            LoadRoutes();
        }

        private void RestartApplication()
        {
            string exePath = Process.GetCurrentProcess().MainModule.FileName;

            Process.Start(exePath);

            Application.Current.Shutdown();
        }

        //заказы
        private async void LoadOrders(string searchTerm = null)
        {
            var orders = await _context.Orders.ToListAsync();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                orders = orders.Where(o => o.CustomerName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            OrdersDataGrid.ItemsSource = orders;
        }

        private void OrdersDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (OrdersDataGrid.SelectedItem != null)
            {
                _selectedOrder = (Order)OrdersDataGrid.SelectedItem;

                if (_selectedOrder != null)
                {
                    CustomerNameTextBox.Text = _selectedOrder.CustomerName;
                    OrderDatePicker.SelectedDate = _selectedOrder.OrderDate;
                    StatusTextBox.Text = _selectedOrder.Status;
                }
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
                    Status = StatusTextBox.Text
                };

                _context.Orders.Add(newOrder);
                await _context.SaveChangesAsync();
                LoadOrders();
                ClearFields();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private async void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedOrder != null)
            {
                try
                {
                    _selectedOrder.CustomerName = CustomerNameTextBox.Text;
                    _selectedOrder.OrderDate = OrderDatePicker.SelectedDate ?? DateTime.Now;
                    _selectedOrder.Status = StatusTextBox.Text;

                    await _context.SaveChangesAsync();
                    LoadOrders();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }

        private async void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedOrder != null)
            {
                _context.Orders.Remove(_selectedOrder);
                await _context.SaveChangesAsync();
                LoadOrders();
                ClearFields();
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            LoadOrders(SearchOrderTextBox.Text);
        }

        private void ClearFields()
        {
            CustomerNameTextBox.Clear();
            OrderDatePicker.SelectedDate = null;
            StatusTextBox.Clear();
            OrdersDataGrid.SelectedItem = null;
            _selectedOrder = null;
        }

        //продукты
        private async void LoadProducts(string searchTerm = null)
        {
            var products = await _context.Products.ToListAsync();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(p => p.ProductName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            ProductsDataGrid.ItemsSource = products;
        }

        private void ProductsDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
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
                _context.Products.Add(newProduct);
                await _context.SaveChangesAsync();
                LoadProducts();
                ClearProductsFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }

        private async void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedProduct != null)
            {
                try
                {
                    _selectedProduct.ProductName = ProductNameTextBox.Text;
                    _selectedProduct.Quantity = int.Parse(QuantityTextBox.Text);
                    _selectedProduct.Price = decimal.Parse(PriceTextBox.Text);

                    await _context.SaveChangesAsync();
                    LoadProducts();
                    ClearProductsFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private async void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedProduct != null)
            {
                _context.Products.Remove(_selectedProduct);
                await _context.SaveChangesAsync();
                LoadProducts();
                ClearProductsFields();
            }
        }

        private void SearchProductButton_Click(object sender, RoutedEventArgs e)
        {
            LoadProducts(SearchProductTextBox.Text);
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
            var transports = await _context.Transports.ToListAsync();

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

                _context.Transports.Add(newTransport);
                await _context.SaveChangesAsync();
                LoadTransports();
                ClearTransportFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private async void EditTransport_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedTransport != null)
            {
                try
                {
                    _selectedTransport.Brand = BrandTextBox.Text;
                    _selectedTransport.Model = ModelTextBox.Text;
                    _selectedTransport.Year = int.Parse(YearTextBox.Text);
                    _selectedTransport.LicensePlate = LicensePlateTextBox.Text;

                    await _context.SaveChangesAsync();
                    LoadTransports();
                    ClearTransportFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private async void DeleteTransport_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedTransport != null)
            {
                _context.Transports.Remove(_selectedTransport);
                await _context.SaveChangesAsync();
                LoadTransports();
                ClearTransportFields();
            }
        }

        private void SearchTransportButton_Click(object sender, RoutedEventArgs e)
        {
            LoadTransports(SearchTransportTextBox.Text);
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
            var routes = await _context.Routes.ToListAsync();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                routes = routes.Where(r => r.StartPoint.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                            r.EndPoint.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            RouteDataGrid.ItemsSource = routes;
        }

        private void RouteDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (RouteDataGrid.SelectedItem!=null)
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

        private async void AddRoute_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newRoute = new Route
                {
                    StartPoint = StartPointTextBox.Text,
                    EndPoint = EndPointTextBox.Text,
                    Distance = double.Parse(DistanceTextBox.Text),
                    TravelTime = double.Parse(TravelTimeTextBox.Text)
                };

                _context.Routes.Add(newRoute);
                await _context.SaveChangesAsync();
                LoadRoutes();
                ClearRouteFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private async void EditRoute_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedRoute != null)
            {
                try
                {
                    _selectedRoute.StartPoint = StartPointTextBox.Text;
                    _selectedRoute.EndPoint = EndPointTextBox.Text;
                    _selectedRoute.Distance = double.Parse(DistanceTextBox.Text);
                    _selectedRoute.TravelTime = double.Parse(TravelTimeTextBox.Text);

                    await _context.SaveChangesAsync();
                    LoadRoutes();
                    ClearRouteFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }

        private async void DeleteRoute_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedRoute != null)
            {
                _context.Routes.Remove(_selectedRoute);
                await _context.SaveChangesAsync();
                LoadRoutes();
                ClearRouteFields();
            }
        }

        private void SearchRouteButton_Click(object sender, RoutedEventArgs e)
        {
            LoadRoutes(SearchRouteTextBox.Text);
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

