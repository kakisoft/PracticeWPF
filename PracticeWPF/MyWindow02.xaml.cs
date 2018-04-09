using System;
using System.Windows;
using System.Windows.Input;

namespace PracticeWPF
{
    /// <summary>
    /// ボタン背景・グリッド
    /// </summary>
    public partial class MyWindow02 : Window
    {
        #region バインディング用のデータ
        private MyData _obj;
        public class MyData
        {
            public string Name { get; set; }
            public int Size { get; set; }
            public bool IsChecked { get; set; }
        }
        #endregion

        #region イニシャライズ
        public MyWindow02()
        {
            InitializeComponent();

            //バインディング
            SetBindingData();
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

    }
}
