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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppCarSharing
{
    /// <summary>
    /// Interaction logic for AUTH_Page.xaml
    /// </summary>
    public partial class AUTH_Page : Page
    {
        public AUTH_Page()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void GetApiButton_Click(object sender, RoutedEventArgs e)
        {
            PageNavigator.frm.Navigate(new API_Page());
        }

        private void txtUsername_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            //tb.Focus();
            if (tb.Text == "Логин")
            {
                tb.Text = "";
                tb.Foreground = Brushes.Black;
            }
        }

        private void txtUsername_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb1 = (TextBox)sender;
                if (string.IsNullOrEmpty(tb1.Text))
                {
                    tb1.Text = "Логин";
                tb1.Foreground = Brushes.DarkGray;   

                 }
        }
        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtPassword.SecurePassword.Length > 0)
            {
                lblPasswordHint.Visibility = Visibility.Collapsed;
            }
            else
            {
                lblPasswordHint.Visibility = Visibility.Visible;
            }
        }
    }
}
