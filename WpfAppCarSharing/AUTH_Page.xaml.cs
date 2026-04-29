using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

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
            var pices = Enumerable.Range(1, 4).OrderBy(x => new Random().Next()).ToList();
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

        private void CheckPuzzle()
        {
            var expectedImageOrder = new int[] { 2, 1, 3, 4 };
            if (PuzzleGrid.Children.OfType<Image>() 
                    .Select((img,i) => expectedImageOrder[i] ==(int)img.Tag)
                    .All(x=>x))
            { MessageBox.Show("Решено!");  }


        }
    }
}

      