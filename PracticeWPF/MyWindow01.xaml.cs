using System;
using System.Linq;
using System.Windows;

namespace PracticeWPF
{
    /// <summary>
    /// 各種パネルの配置
    /// </summary>
    public partial class MyWindow01 : Window
    {

        #region データ定義
        private class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
        #endregion

        #region 初期処理
        /// <summary>
        /// 初期処理
        /// </summary>

        public MyWindow01()
        {
            InitializeComponent();

            InitializeThisWindowsParameters();
        }

        private void InitializeThisWindowsParameters()
        {
            SetThisWindowsInitializeParameter();
            AddThisWindowsEvent();
        }

        private void SetThisWindowsInitializeParameter()
        {
            SetMyComboboxItems();
        }

        private void AddThisWindowsEvent()
        {
            resizeButton.Click += (sender, e) => GetSeledtedParameterFromMyCombobox();
        }
        #endregion

        #region 画面ロード
        /// <summary>
        /// 画面ロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //do something
        }
        #endregion

        #region 画面終了時
        /// <summary>
        /// 画面終了時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //do something
        }

        #endregion

        #region 閉じるボタン
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion

        #region リサイズイベント
        private void window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Console.WriteLine("Height：" + this.Height);
            Console.WriteLine("Width：" + this.Width);

            label_ToggleButtonWidth.Content = ToggleStyleButton1_Copy1.ActualWidth;

            resizeButton.Height = ToggleStyleButton1_Copy1.ActualHeight;
            resizeButton.Width = ToggleStyleButton1_Copy1.ActualWidth;

            int EXHIBIT_BUTTON_COUNT = 3;
            //double stackPanelRsbWidth = StackPanel_rsb.ActualWidth; //StackPanelは動的にサイズが変わらないため、上手く行かず。
            double gridRsbWidth = Grid_rsb.ActualWidth;

            rsb1.Width = gridRsbWidth / EXHIBIT_BUTTON_COUNT;
            rsb2.Width = gridRsbWidth / EXHIBIT_BUTTON_COUNT;
            rsb3.Width = gridRsbWidth / EXHIBIT_BUTTON_COUNT;

            Console.WriteLine(gridRsbWidth);
        }
        #endregion

        #region コンボボックス
        private void SetMyComboboxItems()
        {
            var items = Enumerable.Range(1, 10)
                .Select(i => new Person { Name = "やまだ" + i, Age = 20 + i })
                .ToList();

            this.myComboBox01.ItemsSource = items;
            this.myComboBox02.ItemsSource = items;
        }

        private void GetSeledtedParameterFromMyCombobox()
        {
            try
            {
                int selectedIndex = myComboBox01.SelectedIndex;
                string selectedItemsText = myComboBox01.Text;
                Person selectedItem = (Person)myComboBox01.SelectedItem;

                if (selectedItem == null) return;

                string message = String.Empty;
                message += "SelectedIndex　：　" + selectedIndex.ToString() + System.Environment.NewLine
                        + "Text　：　"           + selectedItemsText        + System.Environment.NewLine
                        + "selectedItem　：　"   + selectedItem.Name        + System.Environment.NewLine
                        + "selectedItem　：　"   + selectedItem.Age         + System.Environment.NewLine
                        ;

                MessageBox.Show(message);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region DatePickerから値を取得
        private void resizeButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt1 = new DateTime();
            var m1 = dt1.ToBinary();

            DateTime? dt2 = myDatePicker01.SelectedDate;
            int numTypeDate = dt2.HasValue ? Convert.ToInt32(dt2.Value.ToString("yyyyMMdd")) : 0;

        }
        #endregion
    }
}
