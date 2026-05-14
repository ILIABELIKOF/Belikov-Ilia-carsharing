using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Security.Principal;
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
        private Image firstButton;
        public AUTH_Page()
        {
            InitializeComponent();

            LoadPuzzle();
        }

     
        

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password;



            CheckPuzzle();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин  и пароль!");
                return;
            }

            //if (!isCaptchaPassed)
            //{
            //    MessageBox.Show("Сначала решите капчу!");
            //    return;
            //}

            try
            {
                using (var context = new CarSharingEntities())
                {

                    var SysUser = await context.System_Accounts
                        .FirstOrDefaultAsync(u => u.Login == username && u.Password == password);
                    if (SysUser == null)
                    {
                        MessageBox.Show("Неверный логин или пароль", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                   else
                    {

                        
                        MessageBox.Show("Вы авторизованы ", "Welcome!", MessageBoxButton.OK, MessageBoxImage.Information);

                        //if (SysUser.System_role == 0)
                        //{
                            PageNavigator.frm.Navigate(new AdminPage());
                        //}
                    }


                }


            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void GetApiButton_Click(object sender, RoutedEventArgs e)
        {
            PageNavigator.frm.Navigate(new API_Page());
        }

        //ВИЗУЛЬНАЯ РАБОТА ЛОГИНА В 2 МЕТОДА
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

        //ВИЗУАЛЬНАЯ РАБОТА ПАРОЛЯ
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

       
        
        
        
        //СОЗДАНИЕ CAPTCHA
        private void LoadPuzzle()
        {
            var rnd = new Random();
            var pices = Enumerable.Range(1, 4).ToList();
            pices = pices.OrderBy(x => rnd.Next()).ToList();
            pices.ForEach(x =>
            {
                var img = new Image
                {
                    Source = new BitmapImage(new Uri($"images/{x}.jpg", UriKind.Relative)),
                    Tag = x,
                    Stretch = Stretch.Fill
                };
                img.MouseLeftButtonUp += Pices_Click;

                PuzzleGrid.Children.Add(img);
            });
        }

        private void Pices_Click( object sender, RoutedEventArgs e)
        {
            if (sender is Image clicked)
            {
                if (firstButton == null)
                {
                    firstButton = clicked;
                    firstButton.Opacity = 0.5;
                    return;
                }
                if (clicked != firstButton)
                {
                    (firstButton.Source, clicked.Source) = (clicked.Source, firstButton.Source);
                    (firstButton.Tag, clicked.Tag) = (clicked.Tag, firstButton.Tag);

                }
                firstButton.Opacity = 1;
                firstButton = null;
                CheckPuzzle();
            }

        }

        private bool isCaptchaPassed = false;
        private void CheckPuzzle()
        { 
            var expectedImageOrder = new int[] { 2, 1, 3, 4 };
            if (PuzzleGrid.Children.OfType<Image>()
                    .Select((img, i) => expectedImageOrder[i] == (int)img.Tag)
                    .All(x => x))
            {
                MessageBox.Show("Решено!");
                isCaptchaPassed = PuzzleGrid.Children.OfType<Image>().Select((img, i) => expectedImageOrder[i] == (int)img.Tag).All(x => x);
            }
            else
            {
                MessageBox.Show("Сначала решите капчу!");
                return;
            }
            }


        }
}

//Разные решения по проверке капчи:
/* добавляем флаг для хранения состояния капчи 
 private bool isCaptchaPassed = false; 

 В коде перемещения картинок добавьте строчку:csharpCheckPuzzle();
// Сразу после этого проверяем условие еще раз для нашего флага
var expectedImageOrder = new int[] { 2, 1, 3, 4 };
isCaptchaPassed = PuzzleGrid.Children.OfType<Image>().Select((img, i) => expectedImageOrder[i] == (int)img.Tag).All(x => x);

Тогда в кнопке LoginButton_Click достаточно будет написать:csharpif (!isCaptchaPassed)
{
    MessageBox.Show("Сначала решите капчу!");
    return;
}
// код авторизации...
*/

/*
 private async void LoginButton_Click(object sender, RoutedEventArgs e)
{
    // Копируем условие из CheckPuzzle один в один
    var expectedImageOrder = new int[] { 2, 1, 3, 4 };
    bool isSolved = PuzzleGrid.Children.OfType<Image>() 
                        .Select((img, i) => expectedImageOrder[i] == (int)img.Tag)
                        .All(x => x);

    // 1. Проверяем результат
    if (!isSolved)
    {
        MessageBox.Show("Капча не пройдена! Соберите пазл.");
        return; // Прерываем авторизацию
    }

    // 2. Дальнейший ваш код авторизации
    string username = UsernameTextBox.Text;
    // ... логика входа
}

 */