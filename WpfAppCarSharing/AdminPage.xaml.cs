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
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
            LoadData(); 
        }

        public class DisplayItems 
        {
        
            public string Last_Name { get; set; }
            public string First_Name { get; set; }
            public string Adres { get; set; }
            public string Phone_Number { get; set; }
            public int INN { get; set; }
            public bool IsBlock  { get; set; }
            
        }


        private void LoadData() 
        {
            using (var db = new CarSharingEntities())
            {
                // Загружаем пользователей вместе с их аккаунтами
                // Проверьте точное имя свойства в вашем классе User (обычно System_Accounts)
                var users = db.Users.Include("System_Accounts").ToList();

                var userList = new List<DisplayItems>();

                foreach (var user in users)
                {
                    // Получаем связанный аккаунт для текущего пользователя
                    var account = user.System_Accounts.FirstOrDefault();

                    userList.Add(new DisplayItems
                    {
                        // Данные берутся из таблицы Users согласно схеме
                        Last_Name = user.Last_Name ?? "",
                        First_Name = user.First_Name ?? "",
                        Adres = user.Adres ?? "",
                        Phone_Number = user.Phone_Number ?? "",
                        INN = user.INN ?? 0,

                        // Данные берутся из связанной таблицы System_Accounts
                        // Если аккаунта нет (null), подставляются значения по умолчанию
                       
                        IsBlock = account?.IsBlock ?? false,
                    
                    });
                }

                TableOfUsers.ItemsSource = userList;
            }

        }
        
        private void ToAdd_Click(object sender, RoutedEventArgs e)
        {    
            PageNavigator.frm.Navigate(new AddUser())
        
        }

        private void ForUnban_Click(object sender, RoutedEventArgs e)
        {
            if (TableOfUsers.SelectedItem != null) 
            {
                var selectedU = TableOfUsers.SelectedItem as DisplayItems;
                selectedU.IsBlock = false;  
                TableOfUsers.Items.Refresh();
            
            }
            else
            {
                MessageBox.Show("Выберите пользователя для разблокировки", "Информация",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ChangePasw_Click(object sender, RoutedEventArgs e)
        {
            PageNavigator.frm.Navigate(new PasswordChager());
        }

        private void TableOfUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TableOfUsers.SelectedItem != null) { 
                
                var selectedUser = TableOfUsers.SelectedItem as DisplayItems;
                
            }
        }

        private void ConfirmChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new CarSharingEntities())
                {
                    var updatedUsers = TableOfUsers.ItemsSource as List<DisplayItems>;

                    if (updatedUsers != null)
                    {
                        foreach (var displayUser in updatedUsers)
                        {
                            var dbUser = db.Users.FirstOrDefault();
                            
                            if (dbUser != null)
                            {
                                dbUser.Last_Name = displayUser.Last_Name;
                                dbUser.First_Name = displayUser.First_Name;
                                dbUser.Adres = displayUser.Adres;
                                dbUser.Phone_Number = displayUser.Phone_Number;
                                dbUser.INN = displayUser.INN;
                            }
                        }

                        db.SaveChanges();
                        MessageBox.Show("Изменения успешно сохранены!", "Успех",
                                      MessageBoxButton.OK, MessageBoxImage.Information);


                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
