using BibliotekaMVP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BibliotekaMVP.View
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        private AuthManager auth;
        public AuthWindow()
        {
            InitializeComponent();
            auth=new AuthManager();
        }

        private void SignInClick(object sender, RoutedEventArgs e)
        {
            auth.SignIn(UsernameTextBox.Text, PasswordBox.Password);
            if(AuthManager.CurrentUser!=null)
            {
                MainWindow window = new MainWindow();
                window.Show();
                this.Close();
            }
        }

        //private void SignUpClick(object sender, RoutedEventArgs e)
        //{
        //    auth.SignUp(UsernameTextBox.Text, PasswordBox.Password);
        //}
    }
}
