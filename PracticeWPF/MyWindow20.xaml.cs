using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using System.Windows.Shapes;

namespace PracticeWPF
{
    /// <summary>
    /// 超簡単な async/await
    /// </summary>
    public partial class MyWindow20 : Window
    {
        public MyWindow20()
        {
            InitializeComponent();
        }

        private void Button01_Click(object sender, RoutedEventArgs e)
        {
            Button01_ClickContentAsync();
        }
        private async void Button01_ClickContentAsync()
        {
            string targetURL = "http://www.google.co.jp/";

            await HttpGetRequestAsync(targetURL);
        }

        private async Task HttpGetRequestAsync(string targetURL)
        {
            using (var _httpClient = new HttpClient())
            {
                Task<string> response = _httpClient.GetStringAsync(targetURL);
                string contents = await response;

                Console.WriteLine(contents);
            }
        }
    }
}
