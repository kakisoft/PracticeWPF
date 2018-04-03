using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
    /// コントロールを動的に配置、および選択パラメータを取得
    /// </summary>
    public partial class MyWindow12 : Window
    {

        private const int TOGGLE_BUTTON_COUNT = 20;
        RadioButton ToggleButtonBaseModel = new RadioButton();

        List<ForCustomToggleButtonCluster> paymentMethodRecordSet = new List<ForCustomToggleButtonCluster>();
        List<RadioButton> paymentMethodElementList = new List<RadioButton>();

        //List<TextBox> tList = new List<TextBox>();

        #region 初期処理
        public MyWindow12()
        {
            InitializeComponent();

            InitializeThisWindowsParameter();
        }

        /// <summary>
        /// 画面固有の内容の初期設定
        /// </summary>
        private void InitializeThisWindowsParameter()
        {
            //SetBindConfig();
            //InitializeBindParameter();
            //SetKoenListGrid();
            ExhibitMasterData();
        }
        #endregion

        #region 各種マスタのデータを画面上に配置
        /// <summary>
        /// 各種マスタのデータを画面上に配置
        /// </summary>
        private void ExhibitMasterData()
        {
            //---( ベースとなるコントロールを設定 )---
            ToggleButtonBaseModel.Style = ToggyeTypeRadioButtonModel.Style;
            ToggleButtonBaseModel.Width = ToggyeTypeRadioButtonModel.Width;
            ToggleButtonBaseModel.Height = ToggyeTypeRadioButtonModel.Height;


            //-----< 選択されたXX情報に紐づく各種マスタ >-----
            ExhibitTicketMstData_Grid();
            ExhibitTicketMstData_StackPanel01();
            ExhibitTicketMstData_StackPanel02();
        }

        /// <summary>
        /// グリッドに配置
        /// </summary>
        private void ExhibitTicketMstData_Grid()
        {
            List<RadioButton> RadioList = new List<RadioButton>();
            for (int i = 0; i < TOGGLE_BUTTON_COUNT; i++)
            {
                var r = new RadioButton();
                r.Style = ToggleButtonBaseModel.Style;
                r.Width = ToggleButtonBaseModel.Width;
                r.Height = ToggleButtonBaseModel.Height;

                r.Name = "GridExhibitedButton" + i;
                r.Content = "G-B" + i;

                RadioList.Add(r);
            }

            foreach (var r in RadioList)
            {
                PaymentType_Grid.Children.Add(r);
                PaymentType_Grid.RegisterName(r.Name, r); //アクセスできるように、名前を登録する。
            }
        }



        /// <summary>
        /// スタックパネルに配置１
        /// </summary>
        private void ExhibitTicketMstData_StackPanel01()
        {
            List<RadioButton> RadioList = new List<RadioButton>();
            for (int i = 0; i < TOGGLE_BUTTON_COUNT; i++)
            {
                var t = new RadioButton();
                t.Style = ToggleButtonBaseModel.Style;
                t.Width = ToggleButtonBaseModel.Width;
                t.Height = ToggleButtonBaseModel.Height;

                t.Name = "StackPanel01ExhibitedButton" + i;
                t.Content = "SP1-B" + i;

                RadioList.Add(t);
            }

            foreach (var t in RadioList)
            {
                PaymentType_SstackPanel01.Children.Add(t);
                PaymentType_SstackPanel01.RegisterName(t.Name, t); //アクセスできるように、名前を登録する。
            }
        }


        /// <summary>
        /// スタックパネルに配置２
        /// </summary>
        private void ExhibitTicketMstData_StackPanel02()
        {
            //横方向のスクロールバーを非表示に設定
            PaymentType_ScrollViewer02.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;


            paymentMethodRecordSet.Add(new ForCustomToggleButtonCluster { Id = 0, Text = "現金", Value = "1", IsChecked = false, });
            paymentMethodRecordSet.Add(new ForCustomToggleButtonCluster { Id = 0, Text = "クレジット", Value = "2", IsChecked = false, });
            paymentMethodRecordSet.Add(new ForCustomToggleButtonCluster { Id = 0, Text = "電子マネー", Value = "3", IsChecked = false, });


            //-----< レコードセットから配置データ設定 >-----
            foreach (var record in paymentMethodRecordSet)
            {
                //ボタンの属性を編集
                var t = new RadioButton();
                t.Style = ToggleButtonBaseModel.Style;
                t.Width = ToggleButtonBaseModel.Width;
                t.Height = ToggleButtonBaseModel.Height;

                t.Name = "StackPanel02ExhibitedButton_" + record.Value;
                t.Content = record.Text;
                t.Tag = record.Value;

                //配置するボタンを、リストに登録。
                paymentMethodElementList.Add(t);
            }


            //-----< 画面上に配置 >-----
            foreach (var element in paymentMethodElementList)
            {
                PaymentType_SstackPanel02.Children.Add(element);
                PaymentType_SstackPanel02.RegisterName(element.Name, element); //アクセスできるように、名前を登録する。
            }

        }
        #endregion

        #region イベント処理
        private void SwipeLeft(object sender, RoutedEventArgs e)
        {
            SwipeLeft_StackPanel();
        }

        private void SwipeRight(object sender, RoutedEventArgs e)
        {

            /*
            //コントロールへのアクセス
            var r0 = (RadioButton)this.MyDockPanel01.FindName("Button0");
            r0.Content = "aaa";
            //r0.Visibility = Visibility.Hidden;
            r0.Visibility = Visibility.Collapsed;
            */



            //コントロールの生成
            //var tbox = new TextBox(); //ここでは例としてTextBox
            //tbox.Name = "tbox1"; //Name
            //MySstackPanel01.Children.Add(tbox); //StackPanel等に追加
            //MySstackPanel01.RegisterName(tbox.Name, tbox); //StackPanel等に登録

            ////コントロールへのアクセス
            //var tbox2 = (TextBox)this.MySstackPanel01.FindName("tbox1");
            //tbox2.Text = "ほげええええ";


            SwipeRight_StackPanel();
        }

        private void SwipeRight_StackPanel()
        {
            PaymentType_ScrollViewer01.LineRight();
            PaymentType_ScrollViewer02.LineRight();
        }

        private void SwipeLeft_StackPanel()
        {
            PaymentType_ScrollViewer01.LineLeft();
            PaymentType_ScrollViewer02.LineLeft();
        }
        #endregion

        #region 動的に配置した要素の値を取得
        private void button_do1Click(object sender, RoutedEventArgs e)
        {
            paymentMethodElementList[0].Content = "あああ";
            paymentMethodElementList[0].IsChecked = true;
        }
        private void button_do2Click(object sender, RoutedEventArgs e)
        {
            ////その他
            //if (paymentMethodElementList.Where(x => x.IsChecked == true).Select(x => x.Content).FirstOrDefault() != null)
            //{
            //}


            //こう書かないと、未選択時に落ちる。
            string selectedItemsValue1 = paymentMethodElementList
                             .Where(x => x.IsChecked == true)
                             .Select(x => x.Content).FirstOrDefault() == null ? "" : paymentMethodElementList
                             .Where(x => x.IsChecked == true)
                             .Select(x => x.Content).FirstOrDefault().ToString();


            //戻り値がObjectとなるため、NG
            var selectedItemsValue2 = paymentMethodElementList
                                         .Where(x => x.IsChecked == true)
                                         .Select(x => x.Tag.ToString());




            //foreachで実現するパターン
            string selectedItemsValue3_1 = "";
            string selectedItemsValue3_2 = "";
            foreach (var el in paymentMethodElementList)
            {
                if (el.IsChecked == true)
                {
                    selectedItemsValue3_1 = el.Content.ToString();
                    selectedItemsValue3_2 = el.Tag.ToString();
                }
            }


            //現実的なパターン
            string selectedItemsValue4 = "";
            foreach (var el in paymentMethodElementList.Where(x => x.IsChecked == true))
            {
                selectedItemsValue4 = el.Tag.ToString();
            }



            Console.WriteLine("selectedItemsValue3_1：" + selectedItemsValue3_1);
            Console.WriteLine("selectedItemsValue3_2：" + selectedItemsValue3_2);

            Console.WriteLine("selectedItemsValue4：" + selectedItemsValue4);



            dynamic testobj;
            testobj = DateTime.Now;
            Console.WriteLine(testobj.Month + "月");
        }

        private void button_do3Click(object sender, RoutedEventArgs e)
        {
        }
        #endregion

        private void PaymentType_ScrollViewer01_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            Console.WriteLine("aaa");

            /*
            if (PaymentType_ScrollViewer01.ContentHorizontalOffset == 0)  
            {
                SwipeLeft_Button.IsEnabled = false;
            }
            else
            {
                SwipeLeft_Button.IsEnabled = true;
            }


            if (PaymentType_ScrollViewer01.ContentHorizontalOffset == PaymentType_ScrollViewer01.ScrollableWidth)
            {
                SwipeRight_Button.IsEnabled = false;
            }
            else
            {
                SwipeRight_Button.IsEnabled = true;
            }
            */


            SwipeLeft_Button.IsEnabled = (PaymentType_ScrollViewer01.ContentHorizontalOffset == 0) ? false : true;
            SwipeRight_Button.IsEnabled = (PaymentType_ScrollViewer01.ContentHorizontalOffset == PaymentType_ScrollViewer01.ScrollableWidth) ? false : true;


            ScrollStatus.Content = (PaymentType_ScrollViewer01.ContentHorizontalOffset + 1)
                                   + "/"
                                   + (PaymentType_ScrollViewer01.ScrollableWidth + 1);
        }

        //#region スクロールボタンイベント処理定義
        //private void SetScrollButtonEventProcess()
        //{
        //    //-----< スクロールイベント追加 >-----
        //    LineLeft_TicketMstStackPanel.Click += (sender, e) => TicketMstScrollViewer.LineLeft();
        //    LineRight_TicketMstStackPanel.Click += (sender, e) => TicketMstScrollViewer.LineRight();

        //    LineLeft_AreaMstStackPanel.Click += (sender, e) => AreaMstScrollViewer.LineLeft();
        //    LineRight_AreaMstStackPanel.Click += (sender, e) => AreaMstScrollViewer.LineRight();

        //    LineLeft_BlockMstStackPanel.Click += (sender, e) => BlockMstScrollViewer.LineLeft();
        //    LineRight_BlockMstStackPanel.Click += (sender, e) => BlockMstScrollViewer.LineRight();

        //    LineLeft_TicketPriceMstStackPanel.Click += (sender, e) => TicketPriceMstScrollViewer.LineLeft(); TicketPriceMstScrollViewer_PlusButton.LineLeft(); TicketPriceMstScrollViewer_MinusButton.LineLeft();
        //    LineRight_TicketPriceMstStackPanel.Click += (sender, e) => TicketPriceMstScrollViewer.LineRight(); TicketPriceMstScrollViewer_PlusButton.LineRight(); TicketPriceMstScrollViewer_MinusButton.LineRight();

        //    LineLeft_SettlementMstStackPanel.Click += (sender, e) => SettlementMstScrollViewer.LineLeft();
        //    LineRight_SettlementMstStackPanel.Click += (sender, e) => SettlementMstScrollViewer.LineRight();

        //    LineLeft_ReceiptMstStackPanel.Click += (sender, e) => ReceiptMstScrollViewer.LineLeft();
        //    LineRight_ReceiptMstStackPanel.Click += (sender, e) => ReceiptMstScrollViewer.LineRight();
        //}
        //#endregion

        /***********************************************/
        #region カスタムクラス
        /// <summary>
        /// トグルボタン制御用
        /// </summary>
        private class ForCustomToggleButtonCluster : BindableBase
        {
            /// <value>一意の識別子</value>
            private int id;
            public int Id
            {
                get { return this.id; }
                set { this.SetProperty(ref this.id, value); }
            }

            /// <value>画面上の値（画面上は「2018年3月」で、内部的には「"201803"」といったケースに対応するため。）</value>
            private string text;
            public string Text
            {
                get { return this.text; }
                set { this.SetProperty(ref this.text, value); }
            }

            /// <value>内部ロジックにて使用する値</value>
            private string value;
            public string Value
            {
                get { return this.value; }
                set { this.SetProperty(ref this.value, value); }
            }

            /// <value>ON/OFF を表す値</value>
            private bool isChecked;
            public bool IsChecked
            {
                get { return this.isChecked; }
                set { this.SetProperty(ref this.isChecked, value); }
            }
        }
        #endregion

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