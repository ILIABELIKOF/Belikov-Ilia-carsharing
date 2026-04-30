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
    /// Interaction logic for PasswordChager.xaml
    /// </summary>
    public partial class PasswordChager : Page
    {
        public PasswordChager()
        {
            InitializeComponent();
        }

        private void ConfirmNewPassword_Click(object sender, RoutedEventArgs e)
        { 
            //ЗАГЛУШКА
            PageNavigator.frm.Navigate(new AdminPage());
        }
        
        
        //РАБОТА С ВИДИМОСТЬЮ НАДПИСЕЙ
        private void OldPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (OldPassword.SecurePassword.Length > 0)
            {
                lblOldPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                lblOldPassword.Visibility = Visibility.Visible;
            }
        }

        private void NewPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (NewPassword.SecurePassword.Length > 0)
            {
                lblNewPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                lblNewPassword.Visibility = Visibility.Visible;
            }

        }
        private void RepeatNewPassword_PasswordChanged(object sender, RoutedEventArgs e) 
        { 
        
            if (RepeatNewPassword.SecurePassword.Length > 0) 
            { 
                lblRepeatNewPassword.Visibility = Visibility.Collapsed;
            }
            else 
            { 
                lblRepeatNewPassword.Visibility= Visibility.Visible;
            }
        
        }
    }
}
