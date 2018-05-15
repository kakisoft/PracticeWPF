using PracticeWPF.Common;
using PracticeWPF.SubWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PracticeWPF
{
    /// <summary>
    /// 各種パネルの配置
    /// </summary>
    public partial class MyWindow01 : Window
    {

        #region データ定義１
        private class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public int position_id { get; set; }
        }
        #endregion

        #region データ定義２
        List<Position> positionList = new List<Position>();

        private class Position
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool IsDeleted { get; set; }
        }

        private void SetPositionCode()
        {
            positionList = new List<Position>();
            positionList.Add(new Position { Id = 0, Name = "（役職なし）", IsDeleted = false });
            positionList.Add(new Position { Id = 1, Name = "主任"        , IsDeleted = false });
            positionList.Add(new Position { Id = 2, Name = "課長"        , IsDeleted = false });
            positionList.Add(new Position { Id = 3, Name = "部長"        , IsDeleted = false });
            positionList.Add(new Position { Id = 4, Name = "神"          , IsDeleted = true  });
        }
        #endregion

        #region データ定義３
        public class HikitoriTxZasekiForDisp
        {
            public string DispText { get; set; }
            public long Price { get; set; }
            public string PriceText
            {
                get
                {
                    string _priceText = String.Empty;

                    _priceText = Price.ToString("C");

                    return _priceText;
                }
            }
        }
        #endregion

        #region バインディングデータ定義
        public SubConfigParameters subConfigParametersCluster = new SubConfigParameters();

        public class SubConfigParameters : BindableBase
        {
            private string _stringParam1;
            private string _stringParam2;
            private string _stringParam3;
            private DateTime? _dateTimeParam1;
            private DateTime? _dateTimeParam2;

            public string StringParam1
            {
                get { return this._stringParam1; }
                set { this.SetProperty(ref this._stringParam1, value); }
            }

            public string StringParam2
            {
                get { return this._stringParam2; }
                set { this.SetProperty(ref this._stringParam2, value); }
            }

            public string StringParam3
            {
                get { return this._stringParam3; }
                set { this.SetProperty(ref this._stringParam3, value); }
            }

            public DateTime? DateTimeParam1
            {
                get { return this._dateTimeParam1; }
                set { this.SetProperty(ref this._dateTimeParam1, value); }
            }

            public DateTime? DateTimeParam2
            {
                get { return this._dateTimeParam2; }
                set { this.SetProperty(ref this._dateTimeParam2, value); }
            }
        }
        #endregion

        #region バインディング設定
        private void SetBindConfig()
        {
            subConfigParametersPanel.DataContext = subConfigParametersCluster;

            //positionComboBox.ItemsSource = positionList;
            positionComboBox.ItemsSource = positionList.Where(x=> x.IsDeleted == false);
        }
        private void ClearBindParameters()
        {
            subConfigParametersCluster.StringParam1 = "";
            subConfigParametersCluster.StringParam2 = "";
            subConfigParametersCluster.StringParam3 = "";
            subConfigParametersCluster.DateTimeParam1 = DateTime.Today;
            subConfigParametersCluster.DateTimeParam2 = null;


            dateTypeUserControl01.Content = new Components.MyUserControl01();
            //dateTypeUserControl01.Content = new Components.MyUserControl01(targetParam1);


            //取得
            var date1 = (Components.MyUserControl01)dateTypeUserControl01.Content;
            //var setData1 = date1.GetDate();

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
            SetBindConfig();
            AddThisWindowsEvent();

            SetDefaultValue();
        }

        private void SetThisWindowsInitializeParameter()
        {
            SetMyComboboxItems();
            DispAppConfigParameters();
            SetPositionCode();
        }

        private void AddThisWindowsEvent()
        {
            resizeButton.Click += (sender, e) => GetSeledtedParameterFromMyCombobox();

            showUserConfig_button.Click += (sender, e) => ShowUserConfig();
            saveUserConfig_button.Click += (sender, e) => SaveUserConfig();


            showSelectedpositionButton.Click += (sender, e) => ShowSelectedposition();

            openSubWindowButton.Click += (sender, e) => OpenSubWindow();
            resetSubWindowsParametersButton.Click += (sender, e) => ClearBindParameters();
        }

        private void SetDefaultValue()
        {
            //positionComboBox.SelectedValue = _selectedValue ?? 0;
            positionComboBox.SelectedValue = 0;

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
            var msgResult = MessageBox.Show("画面を閉じます。よろしいですか？", "確認", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (msgResult != System.Windows.MessageBoxResult.Yes)
            {
                e.Cancel = true;
                return;
            }
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

        #region コンボボックス２
        private void ShowSelectedposition()
        {
            try
            {
                Position selectedItem = (Position)positionComboBox.SelectedItem;

                if (selectedItem == null) return;

                string message = String.Empty;

                message += "Id　：" + selectedItem.Id   + System.Environment.NewLine
                        +  "Name：" + selectedItem.Name + System.Environment.NewLine
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




            DateTime axisTime = DateTime.Now.Date; //本日の、日付のみ取得

            DateTime? startDate = DateTime.Parse("2018/5/11");
            DateTime? endDate = DateTime.Parse("2018/7/11");

            startDate = myDatePicker01.SelectedDate;
            endDate = myDatePicker02.SelectedDate;

            if (startDate <= axisTime && axisTime <= endDate)
            {
                Console.WriteLine("期間内");
            }
            else
            {
                Console.WriteLine("外");
            }
        }
        #endregion

        #region App.configから値を取得
        private void DispAppConfigParameters()
        {
            myAppConfig01.Text = Properties.Settings.Default.AppStringConfig01;
            myAppConfig02.Text = Properties.Settings.Default.AppBoolConfig01.ToString();
            myAppConfig03.Text = Properties.Settings.Default.AppIntConfig01.ToString();
        }

        private void ShowUserConfig()
        {
            myUserConfig01.Text = Properties.Settings.Default.UserStringConfig01;
            myUserConfig02.Text = Properties.Settings.Default.UserBoolConfig01.ToString();
            myUserConfig03.Text = Properties.Settings.Default.UserIntConfig01.ToString();
        }

        private void SaveUserConfig()
        {
            try
            {
                Properties.Settings.Default.UserStringConfig01 = myUserConfig01.Text;
                Properties.Settings.Default.UserBoolConfig01 = Convert.ToBoolean(myUserConfig02.Text);
                Properties.Settings.Default.UserIntConfig01 = Convert.ToInt16(myUserConfig03.Text);
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 別ウィンドウを開く
        private void OpenSubWindow()
        {
            //openSubWindowButton.Click += (sender, e) => new SubWindow01().ShowDialog();
            //openSubWindowButton.Click += (sender, e) => new SubWindow01().Show();

            try
            {
                var _subWindow01 = new SubWindow01(subConfigParametersCluster);

                _subWindow01.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}
