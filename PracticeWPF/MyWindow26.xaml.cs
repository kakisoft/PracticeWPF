using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
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
    ///geoapi
    /// http://geoapi.heartrails.com/
    /// http://geoapi.heartrails.com/api.html
    /// </summary>
    public partial class MyWindow26 : Window
    {
        #region 定義情報
        private const string GET_POSTAL_CODE_BASE_URL = "http://geoapi.heartrails.com/api/";
        private const string FORMAT_OPTION = "json?method=getStations/";  //json形式で取得 
        private const string ZIP_CODE_KEYNAME = "zipcode";


        //http://geoapi.heartrails.com/api/json?method=getAreas


        //------------------------------
        //
        //------------------------------
        private class RequestType
        {
            public int Id { get; set; }
            public string Summary { get; set; }
            public string URL { get; set; }
        }
        private List<RequestType> RequestTypeList = new List<RequestType>();
        private void SetRequestType()
        {
            RequestTypeList = new List<RequestType>();
            RequestTypeList.Add(new RequestType { Id = 0, Summary = "エリア情報取得 API"　 , URL = "http://geoapi.heartrails.com/api/json?method=getAreas" });
            RequestTypeList.Add(new RequestType { Id = 1, Summary = "都道府県情報取得 API" , URL = "http://geoapi.heartrails.com/api/json?method=getPrefectures" });
            RequestTypeList.Add(new RequestType { Id = 2, Summary = "市区町村情報取得 API" , URL = "http://geoapi.heartrails.com/api/json?method=getCities" });
            myComboBox02.ItemsSource = RequestTypeList;
        }
        #endregion

        #region バンディングリスト
        TextTypeControl requestTypeURLCluster = new TextTypeControl();
        private class TextTypeControl : BindableBase
        {
            private string dispText;
            public string DispText
            {
                get { return this.dispText; }
                set { this.SetProperty(ref this.dispText, value); }
            }
        }
        #endregion


        #region イニシャライズ
        public MyWindow26()
        {
            InitializeComponent();

            InitializeThisContents();
        }

        private void InitializeThisContents()
        {

            //textTargetBaseURL.Text = GET_POSTAL_CODE_BASE_URL;
            //textSubParameter.Text = "7830060";

            AddThisEvent();
            SetThisConstParameters();
            SetBindConfig();
            SetInitializeParameter();
        }
        private void AddThisEvent()
        {
            myButton01.Click += (sender, e) => MyButton01_Click();
            //myButton02.Click += (sender, e) => MyButton02_Click();
            //myButton03.Click += (sender, e) => MyButton03_Click();
            //myButton04.Click += (sender, e) => MyButton04_Click();
            //myButton05.Click += (sender, e) => MyButton05_Click();

            myComboBox02.SelectionChanged += (sender, e) => SetSelectedRequestType();
        }
        private void SetThisConstParameters()
        {
            SetRequestType();
        }
        private void SetBindConfig()
        {
            myTextBox02.DataContext = requestTypeURLCluster;
        }
        private void SetInitializeParameter()
        {
            myComboBox02.SelectedIndex = 0;
        }

        #endregion

        #region リクエストタイプを設定
        private void SetSelectedRequestType()
        {
            RequestType selectedItem = (RequestType)myComboBox02.SelectedItem;

            if (selectedItem == null) return;

            requestTypeURLCluster.DispText = selectedItem.URL;

        }
        #endregion

        #region ボタン
        private void MyButton01_Click()
        {
            Button01_ClickContentAsync();
        }
        private async void Button01_ClickContentAsync()
        {
            string targetURL = requestTypeURLCluster.DispText;
            //string postalCode = textSubParameter.Text;


            await HttpGetRequestAsync(targetURL, "");
        }






        public class Response
        {
            public List<string> area { get; set; }
        }

        public class AreaTypeResponse
        {
            public Response response { get; set; }
        }


        private async Task HttpGetRequestAsync(string targetURL, string subParameter)
        {
            string fullUrl = targetURL; // + "?" + ZIP_CODE_KEYNAME + "=" + subParameter;
            string resultContents;

            try
            {
                //-----< データ取得 >-----
                using (var _httpClient = new HttpClient())
                {
                    Task<string> response = _httpClient.GetStringAsync(fullUrl);
                    resultContents = await response;

                    Console.WriteLine(resultContents);
                }

                //-----< デシリアライズ >-----
                dynamic responseData = JsonConvert.DeserializeObject(resultContents);

                //Response r1 = responseData;
                AreaTypeResponse rootObject = JsonConvert.DeserializeObject<AreaTypeResponse>(resultContents);
                //gridResult.ItemsSource = rootObject.response;

                //gridResult.ItemsSource = rootObject.response.area;


                List<string> areaList = new List<string>();
                List<string> areaList2 = new List<string>();

                var hogeTable_2 = new Dictionary<string, string>()
{
    { "one", "abc" },
    { "tow", "def" },
    { "three", "hij" },
};

                var hogeTable_3 = new Dictionary<int, string>();
                hogeTable_3.Add(1, "10ab");
                hogeTable_3.Add(2, "10ab");

                foreach (var item in rootObject.response.area)
                {
                    Console.WriteLine(item);
                    areaList.Add(item);
//                    areaList.Add(item);
                }
                //gridResult.ItemsSource = hogeTable_2;
                //gridResult.ItemsSource = hogeTable_3;

                //gridResult.ItemsSource = areaList;
                gridResult.ItemsSource = rootObject.response.area;


                //-----< 戻り値が nullだった場合 >-----
                //if (responseData.results == null)
                //{
                //    MessageBox.Show("該当するデータがありません。");
                //    return;
                //}

                //-----< コンソールに出力 >-----
                foreach (var item in responseData.response)
                {
                    Console.WriteLine(item);
                    Console.WriteLine("-------------------------------------");
                }

                Console.WriteLine(responseData.response.area);



                //List<dynamic> areaList = responseData.response.area;
                //List<dynamic> areaList = new List<dynamic>();

                //dynamic areaList = responseData.response.area;

                //Sample s1 = new Sample();
                //s1.response = (List<string>)responseData.response.area;

                foreach (var item in responseData.response.area)
                {
                    //areaList.Add(item);
                    //areaList.Add(item.Value);
                    //areaList.Add(item.Value.ToString());
                    //Console.WriteLine(areaList.Add(item.Value));


                    //areaList.Add(item.ToString());
                    //areaList.Add(item.ToList());

                    //foreach (var item2 in item1)
                    //{
                    //    areaList.Add(item2);
                    //}
                }


                foreach (var item in responseData.response)
                {
                    //areaList.Add(item["area"]);


                    //areaList.Add(item);
                    //areaList.Add(item.Value);
                    //areaList.Add(item.Value.ToString());
                    //Console.WriteLine(areaList.Add(item.Value));


                    //areaList.Add(item.ToString());
                    //areaList.Add(item.ToList());

                    //foreach (var item2 in item1)
                    //{
                    //    areaList.Add(item2);
                    //}
                }


                JObject o = JObject.Parse(resultContents);
                //string name = (string)o["response"];
                //var name = (string)o["response"];
                //var name = (List<String>)o["response"];
                var name = o["response"];

                foreach (var item in name)
                {
                    Console.WriteLine(item);

                    //gridResult.ItemsSource = item;
                }

                //var s2 = responseData.response.are.ToList();


                //-----< グリッドにに出力 >-----
                //gridResult.ItemsSource = responseData.response.area;
                //gridResult.ItemsSource = areaList;
                //gridResult.ItemsSource = areaList.Select( x => x.Value);
                //gridResult.ItemsSource = areaList.Select(x => x.Length());

                //gridResult.ItemsSource = responseData.response.area;



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion


        //*******************************************//
        #region バンディング用ベースクラス
        protected class BindableBase : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
            {
                if (Equals(field, value)) { return false; }
                field = value;
                var h = this.PropertyChanged;
                if (h != null) { h(this, new PropertyChangedEventArgs(propertyName)); }
                return true;
            }
        }
        #endregion
    }
}
