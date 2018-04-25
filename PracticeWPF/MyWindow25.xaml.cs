using Newtonsoft.Json;
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
using System.Windows.Shapes;

namespace PracticeWPF
{
    /// <summary>
    /// MyWindow25.xaml の相互作用ロジック
    /// </summary>
    public partial class MyWindow25 : Window
    {
        #region 定義情報
        private const string GET_POSTAL_CODE_BASE_URL = "http://zipcloud.ibsnet.co.jp/api/search";
        private const string ZIP_CODE_KEYNAME = "zipcode";
        #endregion

        #region 初期化
        public MyWindow25()
        {
            InitializeComponent();

            InitializeThisWindowsParameters();
            AddMyEvent();
        }

        private void InitializeThisWindowsParameters()
        {
            textGetPostalCodeBaseURL.Text = GET_POSTAL_CODE_BASE_URL;
            textPostalCode.Text = "7830060";
        }

        private void AddMyEvent()
        {
            myButton01.Click += (sender, e) => MyButton01_Click();
            //myButton02.Click += (sender, e) => MyButton02_Click();
            //myButton03.Click += (sender, e) => MyButton03_Click();
            //myButton04.Click += (sender, e) => MyButton04_Click();
            //myButton05.Click += (sender, e) => MyButton05_Click();
        }
        #endregion

        #region aaa
        private void MyButton01_Click()
        {
            Button01_ClickContentAsync();
        }
        private async void Button01_ClickContentAsync()
        {
            string targetURL  = textGetPostalCodeBaseURL.Text;
            string postalCode = textPostalCode.Text;


            await HttpGetRequestAsync(targetURL, postalCode);
        }

        private async Task HttpGetRequestAsync(string targetURL, string postalCode)
        {
            string fullUrl = targetURL + "?" + ZIP_CODE_KEYNAME + "="+ postalCode;
            string contents;

            using (var _httpClient = new HttpClient())
            {
                Task<string> response = _httpClient.GetStringAsync(fullUrl);
                contents = await response;

                Console.WriteLine(contents);
            }

            //デシリアライズ
            dynamic responseData = JsonConvert.DeserializeObject(contents);
            //dynamic 

            Console.WriteLine(responseData.status);
            foreach (var item in responseData)
            {

            }

            gridResult.ItemsSource = responseData.results;
        }
        #endregion
    }
}
