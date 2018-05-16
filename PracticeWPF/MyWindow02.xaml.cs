using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PracticeWPF
{
    /// <summary>
    /// ボタン背景・グリッド・ListView
    /// </summary>
    public partial class MyWindow02 : Window
    {
        #region ListView制御用クラス
        List<TextTypeControl> textList = new List<TextTypeControl>();

        private class TextTypeControl : BindableBase
        {
            public int Id { get; set; }

            private string dispText;
            public string DispText
            {
                get { return this.dispText; }
                set { this.SetProperty(ref this.dispText, value); }
            }

            private bool _isChecked;
            public bool IsChecked
            {
                get { return this._isChecked; }
                set { this.SetProperty(ref this._isChecked, value); }
            }

        }
        #endregion

        #region データ定義・パラメータ設定
        List<Product> personList = new List<Product>();
        private class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public long Price { get; set; }
            public string PriceText
            {
                get
                {
                    string _priceText = String.Empty;

                    if (Price != 0)
                    {
                        _priceText = Price.ToString("C");
                    }

                    return _priceText;
                }
            }
        }

        private void SetThisWindowsParameters()
        {
            personList = new List<Product>();
            personList.Add(new Product { Id = 1, Name = "product01", Price = 100 });
            personList.Add(new Product { Id = 2, Name = "product02", Price = 20000 });
            personList.Add(new Product { Id = 3, Name = "product03", Price = 300000 });
            personList.Add(new Product { Id = 4, Name = "product04", Price = 4000000 });


            myListView05.ItemsSource = null;
            myListView05.ItemsSource = personList;
        }
        #endregion

        #region バインディング用のデータ
        private MyData _obj;
        public class MyData
        {
            public string Name { get; set; }
            public int Size { get; set; }
            public bool IsChecked { get; set; }
        }
        #endregion

        #region 初期化
        public MyWindow02()
        {
            InitializeComponent();

            //バインディング
            SetBindingData();

            SetThisWindowsParameters();
            SetThisWindowsParameters2();
            SetThisWindowsParameters3();
            SetThisWindowsEvent3();

            SetThisWindowsParameters4();
        }
        #endregion

        #region リストビューにアイテムを追加
        public class TestProperty
        {
            public string Title { get; set; }
            public string Score { get; set; }
            public string Date { get; set; }
        }


        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddListViewItem();
        }


        private void AddListViewItem()
        {
            lv1.Items.Add(new string[] { "1", "2", "3" });
            lv1.Items.Add(new string[] { "4", "5", "6" });
            //lv1.Add(new TestProperty { Title = "title", Score = "score", Date = "data" });
            //lv1.Items.Add(new TestProperty { Title = "title", Score = "score", Date = "data" });

            string[] ss = (string[])lv1.Items[1];
            MessageBox.Show(ss[0]);
        }
        #endregion

        #region テキストボックス：イベント処理

        /// <summary>
        /// サマリー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myTextBox02_KeyDown(object sender, KeyEventArgs e)
        {
            //この要素にフォーカスがある状態でキーが押されたときに発生します。(UIElement から継承されます。)
            Console.WriteLine("KeyDownイベント発生");
        }

        private void myTextBox02_KeyUp(object sender, KeyEventArgs e)
        {
            //この要素にフォーカスがあるときにキーが離されると発生します。(UIElement から継承されます。)
            Console.WriteLine("KeyUpイベント発生");
        }

        private void myTextBox02_LostFocus(object sender, RoutedEventArgs e)
        {
            //この要素が論理フォーカスを失ったときに発生します。(UIElement から継承されます。)
            Console.WriteLine("LostFocusイベント発生");
        }

        private void myTextBox02_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            //この要素からキーボード フォーカスが離れたときに発生します。(UIElement から継承されます。)
            Console.WriteLine("LostKeyboardFocusイベント発生");

            OutputCalculationResultToMyRichTextBox();
        }

        private void myTextBox02_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //選択されているテキストが変更されたときに発生します。(TextBoxBase から継承されます。)
            Console.WriteLine("SelectionChangedイベント発生");

            //ValidateMyRichTextBox();
        }

        private void OutputCalculationResultToMyRichTextBox()
        {
            myRichTextBox01.Document.Blocks.Clear();
            string tmpParam1;
            string tmpParam2;
            int param1;
            int param2;

            try
            {
                tmpParam1 = (string)myTextBox01.Text;
                tmpParam2 = myTextBox02.Text as string;

                param1 = int.Parse(tmpParam1);
                if (int.TryParse(tmpParam2, out param2))
                {
                    //do
                }
                else
                {
                    param2 = 0;
                }

                int result1 = param1 / param2;

                myRichTextBox01.AppendText(result1.ToString());
                string result2 = result1.ToString("D");

            }
            catch (Exception ex)
            {
                myRichTextBox01.AppendText(ex.Message);
            }
        }
        #endregion

        #region バインディング１
        private void SetBindingData()
        {
            _obj = new MyData
            {
                Name = "Binding Sample",
                Size = 20,
                IsChecked = true,
            };
            this.DataContext = _obj;
        }

        private void BindSampleCheckBox_checked(object sender, RoutedEventArgs e)
        {
            SetBindText();
        }
        private void BindSampleCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SetBindText();
        }

        private void SetBindText()
        {
            const string FIX_TEXT = "Binding Sample:";
            _obj.Name = FIX_TEXT;
            if (bindSampleCheckBox.IsChecked == true)
            {
                _obj.Name += "Checked";
            }
            else if (bindSampleCheckBox.IsChecked == false)
            {
                _obj.Name += "UnChecked";
            }
            else
            {

            }
        }
        #endregion

        #region ListView
        private void addListView01_Click(object sender, RoutedEventArgs e)
        {
            AddListView01Click();
        }
        private void AddListView01Click()
        {
            TextTypeControl textElement01 = new TextTypeControl();
            textElement01.Id = myListView01.Items.Count;
            textElement01.DispText = "要素" + myListView01.Items.Count;
            textElement01.IsChecked = true;
            textList.Add(textElement01);
            myListView01.Items.Add(textElement01);


            TextTypeControl textElement02 = new TextTypeControl();
            textElement02.Id = myListView01.Items.Count;
            textElement02.DispText = "要素" + myListView01.Items.Count;
            textElement02.IsChecked = true;
            textList.Add(textElement02);
            myListView01.Items.Add(textElement02);

            TextTypeControl textElement03 = new TextTypeControl();
            textElement03.Id = myListView01.Items.Count;
            textElement03.DispText = "要素" + myListView01.Items.Count;
            textElement03.IsChecked = false;
            textList.Add(textElement03);
            myListView01.Items.Add(textElement03);
        }

        private void selectListView01_Click(object sender, RoutedEventArgs e)
        {
            //-----( 選択 )-----
            myListView01.SelectedIndex = 1;
        }

        private void editListView01_Click(object sender, RoutedEventArgs e)
        {
            TextTypeControl selectedElement = (TextTypeControl)myListView01.SelectedItem;

            if (selectedElement == null) return;


            Console.WriteLine(selectedElement.Id);
            selectedElement.DispText = "aaaaaaaaaaa";


            //グリッドの内容も変更される。
            textList[0].DispText = "nn";
        }

        private void deleteListView01_Click(object sender, RoutedEventArgs e)
        {
            //myListView01.Items

            foreach (var item in textList)
            {
                if (item.IsChecked)
                {

                }
            }

            //-----( 削除 )-----
            myListView01.Items.Remove(1);    //NG
            myListView01.Items.RemoveAt(2);  //OK
        }

        #endregion

        #region ListView2
        List<TextTypeControl> textList2 = new List<TextTypeControl>();

        private void SetThisWindowsParameters2()
        {
            textList2.Add(new TextTypeControl { Id = 1, DispText = "北海道" });
            textList2.Add(new TextTypeControl { Id = 2, DispText = "東北" });
            textList2.Add(new TextTypeControl { Id = 3, DispText = "関東" });
            textList2.Add(new TextTypeControl { Id = 4, DispText = "中部" });

            myListView02.ItemsSource = textList2;
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            //if (item != null && item.IsSelected)
            if (item != null)
            {

                Console.WriteLine("===============================");
                Console.WriteLine(item.DataContext);
                Console.WriteLine("===============================");

                var a1 = (TextTypeControl)item.DataContext;
                Console.WriteLine("===============================");
                Console.WriteLine(a1);
                Console.WriteLine("===============================");

                var a2 = a1.DispText;
                Console.WriteLine("===============================");
                Console.WriteLine(a2);
                Console.WriteLine("===============================");
            }
        }
        #endregion

        #region ListView3
        List<TextTypeControl> textList3 = new List<TextTypeControl>();

        private void SetThisWindowsParameters3()
        {
            textList3.Add(new TextTypeControl { Id = 1, DispText = "九州", IsChecked = true });
            textList3.Add(new TextTypeControl { Id = 2, DispText = "四国", IsChecked = false });
            textList3.Add(new TextTypeControl { Id = 3, DispText = "中国", IsChecked = false });
            textList3.Add(new TextTypeControl { Id = 4, DispText = "近畿", IsChecked = true });

            //Listに、別のListの要素を追加
            textList3.AddRange(textList2);

            myListView03.ItemsSource = textList3;
        }

        private void SetThisWindowsEvent3()
        {
            //-----< グリッド >-----
            myListView03.TouchDown += (sender, e) => GetSelectedElementFromMyListView03(sender);
            //myListView03.MouseDown += (sender, e) => GetSelectedElementFromMyListView03(sender);
            //myListView03.MouseDoubleClick += (sender, e) => GetSelectedElementFromMyListView03(sender);

            myListView03.PreviewMouseLeftButtonDown += (sender, e) => GetSelectedElementFromMyListView03(sender);

            //myListView03.MouseUp += (sender, e) => aaaaaaaaa(sender); //枠外でしか反応しない？

            myListView03.MouseLeftButtonUp += (sender, e) => GetSelectedElementFromMyListView03(sender);
            myListView03.MouseLeftButtonDown += (sender, e) => GetSelectedElementFromMyListView03(sender);

            //↑のうち、どれも CheckBoxの上では反応しない。
        }

        private void GetSelectedElementFromMyListView03(object sender)
        {
            //-----( 選択内容を取得 )-----
            TextTypeControl selectedElement = (TextTypeControl)((ListView)sender).SelectedValue;

            if (selectedElement == null) return;


            Console.WriteLine(selectedElement.DispText);
        }
        #endregion

        #region ListView4
        List<TextTypeControl> textList4 = new List<TextTypeControl>();

        private void SetThisWindowsParameters4()
        {
            textList4.Add(new TextTypeControl { Id = 1, DispText = "九州", IsChecked = false });
            textList4.Add(new TextTypeControl { Id = 2, DispText = "四国", IsChecked = true });
            textList4.Add(new TextTypeControl { Id = 3, DispText = "中国", IsChecked = true });
            textList4.Add(new TextTypeControl { Id = 4, DispText = "近畿", IsChecked = false });

            myListView04.ItemsSource = textList4;
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
