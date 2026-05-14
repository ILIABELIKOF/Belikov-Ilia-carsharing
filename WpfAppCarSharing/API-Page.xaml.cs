using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    /// Interaction logic for API_Page.xaml
    /// </summary>
    public partial class API_Page : Page
    {
        public API_Page()
        {
            InitializeComponent();
            //StartTransferSimulator();
        }
 
        private string newValue;

        /* 
         * private void StartTransferSimulator()
         * {
                 * var processInfo = new ProcessStartInfo 
                 * {
                     *  FileName = "TransferSimulator.exe",
                     *  UseShellExecute = true,
                     *  Verb = ""
                 * };
                 * var process = Process.Start(processInfo);
                 * System.Threading.Thread.Sleep(1000);
         * } 
         */
      
 

        private void SendResultButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async Task TakeDataButton_ClickAsync(object sender, RoutedEventArgs e)

        {
            var client = new HttpClient();
            string url = "";
            HttpResponseMessage httpResponseMessage = await client.GetAsync(url);
            HttpResponseMessage response = httpResponseMessage;
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                //var fullName = JsonConvert.DeserializeObject<FullNameData>(responseBody);
                //newValue = fullName.Value;    
                //dataHere.Text = newValue;
            }
            else {

                MessageBox.Show("ошибка!");
            }

        }

        private void CatchDAta_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(newValue)) 
            {
                MessageBox.Show("Сначала получить данные"); 
            }
            bool flag = false;
            string pattern = @"[\d!@#?=$:%^&*()]";
            pattern.ToList().ForEach(x =>
            {
                if (newValue.Contains(x)) {
                    flag = true;
                }
            });
            UpdateTestDocument(!flag ? "Успешно" : "Неуспешно");
            //GoodOrNot.Text = flag ? "Найдены запрещенные символы" : "Данные корректны";

        }

        private void UpdateTestDocument(string v)
        {
            throw new NotImplementedException();
        }

        public class FullNameData 
        { 
            public string value { get; set; }
        }

       
    }
}
