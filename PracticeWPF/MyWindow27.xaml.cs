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
    /// お天気 API
    /// http://weather.livedoor.com/weather_hacks/webservice
    /// </summary>
    public partial class MyWindow27 : Window
    {
        #region 定義情報
        private const string TMP_TARGET_URL = "http://weather.livedoor.com/forecast/webservice/json/v1?city=400010";
        private const string TMP_TARGET_AREA_NAME = "福岡県　福岡市";
        #endregion

        #region JSONクラス定義
        public partial class RootObject
        {
            [JsonProperty("pinpointLocations")]
            public PinpointLocation[] PinpointLocations { get; set; }

            [JsonProperty("link")]
            public string Link { get; set; }

            [JsonProperty("forecasts")]
            public Forecast[] Forecasts { get; set; }

            [JsonProperty("location")]
            public Location Location { get; set; }

            [JsonProperty("publicTime")]
            public string PublicTime { get; set; }

            [JsonProperty("copyright")]
            public Copyright Copyright { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("description")]
            public Description Description { get; set; }
        }

        public partial class Copyright
        {
            [JsonProperty("provider")]
            public PinpointLocation[] Provider { get; set; }

            [JsonProperty("link")]
            public string Link { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("image")]
            public Image Image { get; set; }
        }

        public partial class Image
        {
            [JsonProperty("width")]
            public long Width { get; set; }

            [JsonProperty("link", NullValueHandling = NullValueHandling.Ignore)]
            public string Link { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("height")]
            public long Height { get; set; }
        }

        public partial class PinpointLocation
        {
            [JsonProperty("link")]
            public string Link { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }

        public partial class Description
        {
            [JsonProperty("text")]
            public string Text { get; set; }

            [JsonProperty("publicTime")]
            public string PublicTime { get; set; }
        }

        public partial class Forecast
        {
            [JsonProperty("dateLabel")]
            public string DateLabel { get; set; }

            [JsonProperty("telop")]
            public string Telop { get; set; }

            [JsonProperty("date")]
            public DateTimeOffset Date { get; set; }

            [JsonProperty("temperature")]
            public Temperature Temperature { get; set; }

            [JsonProperty("image")]
            public Image Image { get; set; }
        }

        public partial class Temperature
        {
            [JsonProperty("min")]
            public Max Min { get; set; }

            [JsonProperty("max")]
            public Max Max { get; set; }
        }

        public partial class Max
        {
            [JsonProperty("celsius")]
            public string Celsius { get; set; }

            [JsonProperty("fahrenheit")]
            public string Fahrenheit { get; set; }
        }

        public partial class Location
        {
            [JsonProperty("city")]
            public string City { get; set; }

            [JsonProperty("area")]
            public string Area { get; set; }

            [JsonProperty("prefecture")]
            public string Prefecture { get; set; }
        }
        #endregion

        #region 初期化
        public MyWindow27()
        {
            InitializeComponent();

            InitializeThisWindowsConfig();
        }
        private void InitializeThisWindowsConfig()
        {
            SetInitParameters();

            AddMyEvent();
        }

        private void SetInitParameters()
        {
            targetAreaName.Text = TMP_TARGET_AREA_NAME;
            targetURL.Text = TMP_TARGET_URL;
        }

        private void AddMyEvent()
        {
            myButton01.Click += (sender, e) => MyButton01_Click();
        }
        #endregion

        #region API
        private async void MyButton01_Click()
        {
            string fullUrl = TMP_TARGET_URL;
            string resultContents;

            try
            {
                //-----< データ取得 >-----
                using (var _httpClient = new HttpClient())
                {
                    Task<string> response = _httpClient.GetStringAsync(fullUrl);
                    resultContents = await response;
                }

                //デシリアライズ
                RootObject jsonRoot = JsonConvert.DeserializeObject<RootObject>(resultContents);





                //-----< 出力 >-----

                myTextBox01.Clear();

                //出力１
                foreach (var item in jsonRoot.PinpointLocations)
                {
                    Console.WriteLine(item.Link + System.Environment.NewLine);
                    Console.WriteLine(item.Name + System.Environment.NewLine);

                    myTextBox01.AppendText(item.Link + System.Environment.NewLine);
                    myTextBox01.AppendText(item.Name + System.Environment.NewLine);
                }

                //出力２
                foreach (var item in jsonRoot.Link)
                {
                    Console.WriteLine(item);
                    myTextBox01.AppendText(item.ToString() + System.Environment.NewLine);
                }

                //出力３
                foreach (var item in jsonRoot.Forecasts)
                {
                    Console.WriteLine(item);
                    myTextBox01.AppendText(item.ToString());


                    Console.WriteLine(item.DateLabel + System.Environment.NewLine);
                    Console.WriteLine(item.Telop + System.Environment.NewLine);
                    Console.WriteLine(item.Date + System.Environment.NewLine);
                    Console.WriteLine(item.Temperature.Min + System.Environment.NewLine);
                    Console.WriteLine(item.Temperature.Max + System.Environment.NewLine);

                     myTextBox01.AppendText(item.DateLabel + System.Environment.NewLine);
                     myTextBox01.AppendText(item.Telop + System.Environment.NewLine);
                     myTextBox01.AppendText(item.Date + System.Environment.NewLine);
                     myTextBox01.AppendText(item.Temperature.Min + System.Environment.NewLine);
                     myTextBox01.AppendText(item.Temperature.Max + System.Environment.NewLine);
                }

                //出力４
                Console.WriteLine(jsonRoot.Location.City);
                Console.WriteLine(jsonRoot.Location.Area);
                Console.WriteLine(jsonRoot.Location.Prefecture);

                myTextBox01.AppendText(jsonRoot.Location.City);
                myTextBox01.AppendText(jsonRoot.Location.Area);
                myTextBox01.AppendText(jsonRoot.Location.Prefecture);


                //出力５
                Console.WriteLine(jsonRoot.PublicTime);
                myTextBox01.AppendText(jsonRoot.PublicTime);

                //出力６
                foreach (var item in jsonRoot.Copyright.Provider)
                {
                    Console.WriteLine(item.Link + System.Environment.NewLine);
                    Console.WriteLine(item.Name + System.Environment.NewLine);

                    myTextBox01.AppendText(item.Link + System.Environment.NewLine);
                    myTextBox01.AppendText(item.Name + System.Environment.NewLine);
                }

                //出力７
                Console.WriteLine(jsonRoot.Title);
                myTextBox01.AppendText(jsonRoot.Title);

                //出力８
                Console.WriteLine(jsonRoot.Description.Text);
                Console.WriteLine(jsonRoot.Description.PublicTime);

                myTextBox01.AppendText(jsonRoot.Description.Text);
                myTextBox01.AppendText(jsonRoot.Description.PublicTime);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

    }
}
