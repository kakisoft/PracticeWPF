﻿using Newtonsoft.Json;
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
                }

                //-----< デシリアライズ >-----
                dynamic responseData = JsonConvert.DeserializeObject(resultContents);


                //-----< 出力 >-----
                List<dynamic> areaList = new List<dynamic>();
                foreach (var item in responseData.response.area)
                {
                    areaList.Add(item);
                }
                myListView01.ItemsSource = areaList;

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
