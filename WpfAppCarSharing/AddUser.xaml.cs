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
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Page
    {
        public AddUser()
        {
            InitializeComponent();
        }

        private void ConfirmAddUser_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();//заглушка
        }


















        //ПОЛЯ
        private void FirstNameAdd_GotFocus(object sender, RoutedEventArgs e)
        { 
            if (FirstNameAdd.Text == "Введите Имя")
            {
                FirstNameAdd.Clear();
            }
        }

        private void FirstNameAdd_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FirstNameAdd.Text))
            {
                FirstNameAdd.Text = "Введите Имя";
            }
        }

        private void LastNameAdd_GotFocus(object sender, RoutedEventArgs e)
        {
            if (LastNameAdd.Text == "Введите Фамилию")
            {
                LastNameAdd.Clear();
            }
        }

        private void LastNameAdd_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LastNameAdd.Text))
            {
                LastNameAdd.Text = "Введите Фамилию";
            }
        }

        private void LoginAdd_GotFocus(object sender, RoutedEventArgs e)
        {
            if (LoginAdd.Text == "Введите Логин")
            {
                LoginAdd.Clear();
            }
        }

        private void LoginAdd_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginAdd.Text))
            {
                LoginAdd.Text = "Введите Логин";         
            }
        }

        private void PasswordAdd_GotFocus(object sender, RoutedEventArgs e)
        {
            if(PasswordAdd.Text == "Введите Пароль")
            {
                PasswordAdd.Clear();
            }
        }

        private void PasswordAdd_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PasswordAdd.Text))
            {
                PasswordAdd.Text = "Введите Пароль";

            }
        }
    }

}
