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
            try
            {
                AddUserToDataBase();
                MessageBox.Show("Пользователь успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearFields();
                PageNavigator.frm.Navigate(new AdminPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении пользователя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        private void AddUserToDataBase() 
        {

            using (var context = new CarSharingEntities()) 
            {
                var addedUser = new User
                {
                    First_Name = FirstNameAdd.Text.Trim(),
                    Last_Name = LastNameAdd.Text.Trim(),
                    Phone_Number = PhoneNumber.Text.Trim(), 
                    Adres = Adres.Text.Trim(),
                    INN = string.IsNullOrEmpty(INN.Text) ? (int?)null : int.Parse(INN.Text)
                    // Если TextBox пустой — записываем null, иначе — конвертируем в int

                };
            
                context.Users.Add(addedUser);
                context.SaveChanges();
            
            
            }
        
        
        
        
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

        private void PhoneNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PhoneNumber.Text == "Введите Номер телефона")
            {
                PhoneNumber.Clear();
            }
        }

        private void PhoneNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PhoneNumber.Text))
            {
                PhoneNumber.Text = "Введите Номер телефона";         
            }
        }

        private void Adres_GotFocus(object sender, RoutedEventArgs e)
        {
            if(Adres.Text == "Введите Адрес")
            {
                Adres.Clear();
            }
        }

        private void Adres_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Adres.Text))
            {
                Adres.Text = "Введите Адрес";

            }
        }





        //РАБОТА С ЗАПОЛНЕНИЕМ INT В ПОЛЕ ИНН
        

        private void INN_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);//Возвращение введенного значения если символ численный
        }


        //Блокировка вставки текста, содержащего буквы (через Ctrl+V или контекстное меню)
        private void INN_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));

                if (!text.All(char.IsDigit))
                {
                    e.CancelCommand();

                }

            }
            else { e.CancelCommand(); }
        }

        private void ClearFields()
        {
            FirstNameAdd.Clear();
            LastNameAdd.Clear();
            PhoneNumber.Clear();
            Adres.Clear();
            INN.Clear();

        }
    }

}
