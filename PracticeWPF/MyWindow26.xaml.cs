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
        private const string BASE_URL = "http://geoapi.heartrails.com/api/";
        private const string FORMAT_OPTION = "json?method=getStations/";  //json形式で取得 

        //------------------------------
        //
        //------------------------------
        enum RequestTypeId : int
        {
            Area = 0,
            Prefecture = 1,
            Municipality = 2
        }

        private List<RequestType> RequestTypeList = new List<RequestType>();
        private class RequestType
        {
            public int Id { get; set; }
            public string Summary { get; set; }
            public string URL { get; set; }
        }

        private void SetRequestType()
        {
            RequestTypeList = new List<RequestType>();
            RequestTypeList.Add(new RequestType { Id = (int)RequestTypeId.Area        , Summary = "エリア情報取得 API"　 , URL = "http://geoapi.heartrails.com/api/json?method=getAreas" });
            RequestTypeList.Add(new RequestType { Id = (int)RequestTypeId.Prefecture  , Summary = "都道府県情報取得 API" , URL = "http://geoapi.heartrails.com/api/json?method=getPrefectures" });
            RequestTypeList.Add(new RequestType { Id = (int)RequestTypeId.Municipality, Summary = "市区町村情報取得 API" , URL = "http://geoapi.heartrails.com/api/json?method=getCities" });
            apiType.ItemsSource = RequestTypeList;
        }
        #endregion

        #region プライベート変数
        RequestType selectedItem = new RequestType();
        TextTypeControl baseURLCluster = new TextTypeControl();
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

            apiType.SelectionChanged += (sender, e) => SetSelectedRequestType();
        }
        private void SetThisConstParameters()
        {
            SetRequestType();
        }
        private void SetBindConfig()
        {
            baseURL.DataContext = baseURLCluster;
            apiText.DataContext = requestTypeURLCluster;
        }
        private void SetInitializeParameter()
        {
            baseURLCluster.DispText = BASE_URL;

            apiType.SelectedIndex = 0;
        }

        #endregion

        #region リクエストタイプを設定
        private void SetSelectedRequestType()
        {
            selectedItem = (RequestType)apiType.SelectedItem;

            if (selectedItem == null) return;

            requestTypeURLCluster.DispText = selectedItem.URL;
            ClearDiscriptionArea();
        }
        #endregion

        #region ボタン押下時の処理
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
        private async Task HttpGetRequestAsync(string targetURL, string subParameter)
        {
            string fullUrl = targetURL;
            string resultContents;

            try
            {
                //-----< データ取得 >-----
                using (var _httpClient = new HttpClient())
                {
                    Task<string> response = _httpClient.GetStringAsync(fullUrl);
                    resultContents = await response;
                }

                //-----< デシリアライズ >-----
                dynamic responseData = JsonConvert.DeserializeObject(resultContents);


                //-----< 出力 >-----
                ShowResponseData(responseData);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 取得タイプごとの分岐
        private bool ShowResponseData(dynamic responseData)
        {
            switch (selectedItem.Id)
            {
                case (int)RequestTypeId.Area:
                    return ExpressAreaData(responseData);

                case (int)RequestTypeId.Prefecture:
                    return ExpressPrefectureData(responseData);

                case (int)RequestTypeId.Municipality:
                    break;
                default:
                    return false;
            }

            return true;
        }
        #endregion

        #region エリア情報取得
        private bool ExpressAreaData(dynamic responseData)
        {
            try
            {
                //-----< 出力 >-----
                List<dynamic> expressList = new List<dynamic>();
                foreach (var item in responseData.response.area)  //area
                {
                    expressList.Add(item);
                }
                myListView01.ItemsSource = expressList;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
        #endregion

        #region 都道府県情報取得
        private bool ExpressPrefectureData(dynamic responseData)
        {
            try
            {
                //-----< 出力 >-----
                List<dynamic> expressList = new List<dynamic>();
                foreach (var item in responseData.response.prefecture)  //prefecture
                {
                    expressList.Add(item);
                }
                myListView01.ItemsSource = expressList;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
        #endregion

        #region 表示領域をクリア
        private void ClearDiscriptionArea()
        {
            var dummyList = new List<string>();
            myListView01.ItemsSource = dummyList;
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
