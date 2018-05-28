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
        public class Product
        {
            public long Id { get; set; }
            public string ProductName { get; set; }
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
            SetDctionaryParameters();
        }

        private void AddThisWindowsEvent()
        {
            resizeButton.Click += (sender, e) => GetSeledtedParameterFromMyCombobox();

            showUserConfig_button.Click += (sender, e) => ShowUserConfig();
            saveUserConfig_button.Click += (sender, e) => SaveUserConfig();


            showSelectedpositionButton.Click += (sender, e) => ShowSelectedposition();

            openSubWindowButton.Click += (sender, e) => OpenSubWindow();
            resetSubWindowsParametersButton.Click += (sender, e) => ClearBindParameters();

            showSeatListButton01.Click += (sender, e) => ShowSeatListFromButton01();
            showSeatListButton02.Click += (sender, e) => ShowSeatListFromButton02();
            showSeatListButton03.Click += (sender, e) => ShowSeatListFromButton03();

            forEach01.Click += (sender, e) => ForEach01Button_Click();
            Dictionary01.Click += (sender, e) => Dictionary01Button_Click();
            Dictionary02.Click += (sender, e) => Dictionary02Button_Click();
            Dictionary03.Click += (sender, e) => Dictionary03Button_Click();
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

        #region 何かの順番を変則的に表示する
        List<Seats> SeatList = new List<Seats>();
        //JsonだとNextという便利な構文が使えるみたいだね・・・
        public class Seats
        {
            public long Id         { get; set; }
            public long SeatType   { get; set; }
            public long SalesType  { get; set; }
            public long Floor      { get; set; }
            public long RowNo      { get; set; }
            public long ColumnNo   { get; set; }
            public long SequenceNo { get; set; }
            public long Price      { get; set; }

            public string SeatTypeName { get; set; }
            public string SalesTypeName { get; set; }

            //-----( 表示用 )-----
            public string FloorText    { get { return Floor    + "階"; } }
            public string RowNoText    { get { return RowNo    + "列"; } }
            public string ColumnNoText { get { return ColumnNo + "番"; } }
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

        private static class ManageDispSeatSwitchKey
        {
            private static long AxisColumnNo { get; set; }

            private static long SeatType;
            private static long SalesType;
            private static long Floor;
            private static long RowNo;
            private static long ColumnNo;
            private static long Price;

            private static string SalesTypeName;
            private static string SeatTypeName;

            public static void ResetManageParameters()
            {
                //DB上に存在しえない値を初期値として設定
                SeatType  = -1;
                SalesType = -1;
                Floor     = -1;
                RowNo     = -1;
                ColumnNo  = -1;
                Price     = -1;

                SalesTypeName = "";
                SeatTypeName = "";
            }

            public static void SetManageParameters(Seats _seat)
            {
                //変更キーが異なっていた場合、表示時の起点とする値を変更する。
                if (IsSwitchKeyChanged(_seat) == true)
                {
                    AxisColumnNo = _seat.ColumnNo;
                }

                SeatType      = _seat.SeatType;
                SalesType     = _seat.SalesType;
                Floor         = _seat.Floor;
                RowNo         = _seat.RowNo;
                ColumnNo      = _seat.ColumnNo;
                Price         = _seat.Price;
                SeatTypeName  = _seat.SeatTypeName;
                SalesTypeName = _seat.SalesTypeName;
            }

            public static bool IsSwitchKeyChanged(Seats _seat)
            {
                if (SeatType  != _seat.SeatType)  return true;
                if (SalesType != _seat.SalesType) return true;
                if (Floor     != _seat.Floor)     return true;
                if (RowNo     != _seat.RowNo)     return true;
                if (ColumnNo + 1 != _seat.ColumnNo) return true; //連番となっているか

                return false;
            }

            //-----( 表示用 )-----
            public static string DispText
            {
                get
                {
                    string _dispText = String.Empty;

                    //階・列
                    _dispText = ""
                                +        Floor + "階"
                                + "　" + RowNo + "列"
                    ;

                    //番
                    if (ColumnNo == AxisColumnNo)
                    {
                        _dispText += "　" + ColumnNo + "番";
                    }
                    else
                    {
                        _dispText += "　" + AxisColumnNo + "～" + ColumnNo + "番";
                    }

                    //座席タイプ
                    _dispText += "　" + SeatTypeName;

                    //販売形態
                    _dispText += "　" + SalesTypeName;

                    //数量
                    _dispText += "　×" + ( (ColumnNo - AxisColumnNo) + 1);

                    //値段
                    _dispText += "　" + PriceText;

                    return _dispText;
                }
            }

            public static string PriceText
            {
                get
                {
                    string _priceText = String.Empty;
                    long subTotalPrice;

                    subTotalPrice = Price * ((ColumnNo - AxisColumnNo) + 1);

                    _priceText = subTotalPrice.ToString("C");

                    return _priceText;
                }
            }
        }

        private void SetSeatList01()
        {
            SeatList = new List<Seats>();
            //Floor 1：Row 1
            SeatList.Add(new Seats { Id = 0, Floor = 1, RowNo = 1, ColumnNo = 1 });
            SeatList.Add(new Seats { Id = 1, Floor = 1, RowNo = 1, ColumnNo = 2 });
            SeatList.Add(new Seats { Id = 2, Floor = 1, RowNo = 1, ColumnNo = 3 });

            SeatList.Add(new Seats { Id = 3, Floor = 1, RowNo = 1, ColumnNo = 5 });

            SeatList.Add(new Seats { Id = 4, Floor = 1, RowNo = 1, ColumnNo = 7 });
            SeatList.Add(new Seats { Id = 5, Floor = 1, RowNo = 1, ColumnNo = 8 });

            SeatList.Add(new Seats { Id = 6, Floor = 1, RowNo = 1, ColumnNo = 15 });

            //Floor 1：Row 2
            SeatList.Add(new Seats { Id = 7, Floor = 1, RowNo = 2, ColumnNo = 16 });

            SeatList.Add(new Seats { Id = 8, Floor = 1, RowNo = 2, ColumnNo = 17 });
            SeatList.Add(new Seats { Id = 9, Floor = 1, RowNo = 2, ColumnNo = 18 });

            //Floor 2：Row 1
            SeatList.Add(new Seats { Id = 10, Floor = 2, RowNo = 1, ColumnNo = 1 });
            SeatList.Add(new Seats { Id = 11, Floor = 2, RowNo = 1, ColumnNo = 2 });

            //Floor 3～：Row 1
            SeatList.Add(new Seats { Id = 12, Floor = 3, RowNo = 1, ColumnNo = 2 });
            SeatList.Add(new Seats { Id = 13, Floor = 4, RowNo = 1, ColumnNo = 2 });

            //Last Floor
            SeatList.Add(new Seats { Id = 99, Floor = 99, RowNo = 99, ColumnNo = 99 });


            foreach (var item in SeatList)
            {
                item.SeatType = 1;
                item.SeatTypeName = "S席";

                item.SalesType = 1;
                item.SalesTypeName = "定価";

                item.Price = 8000;
            }
        }

        private void SetSeatList02()
        {
            SeatList = new List<Seats>();
            //Floor 1：Row 1
            SeatList.Add(new Seats { Id = 0, Floor = 1, RowNo = 1, ColumnNo = 1 });
            SeatList.Add(new Seats { Id = 1, Floor = 1, RowNo = 1, ColumnNo = 2 });
            SeatList.Add(new Seats { Id = 2, Floor = 1, RowNo = 1, ColumnNo = 3 });
            SeatList.Add(new Seats { Id = 3, Floor = 1, RowNo = 1, ColumnNo = 4 });
            SeatList.Add(new Seats { Id = 4, Floor = 1, RowNo = 1, ColumnNo = 5 });


            foreach (var item in SeatList)
            {
                item.SeatType = 2;
                item.SeatTypeName = "A席";

                item.SalesType = 2;
                item.SalesTypeName = "前売り券";

                item.Price = 5000;
            }

        }

        private void SetSeatList03()
        {
            SeatList = new List<Seats>();
            //Floor 1：Row 1
            SeatList.Add(new Seats { Id = 0, SeatType = 1, SeatTypeName = "S席", SalesType = 1, SalesTypeName = "定価", Price = 8000, Floor = 1, RowNo = 1, ColumnNo = 1 });
            SeatList.Add(new Seats { Id = 0, SeatType = 1, SeatTypeName = "S席", SalesType = 1, SalesTypeName = "定価", Price = 8000, Floor = 1, RowNo = 1, ColumnNo = 2 });
            SeatList.Add(new Seats { Id = 0, SeatType = 2, SeatTypeName = "A席", SalesType = 1, SalesTypeName = "定価", Price = 7000, Floor = 1, RowNo = 1, ColumnNo = 3 });
            SeatList.Add(new Seats { Id = 0, SeatType = 2, SeatTypeName = "A席", SalesType = 1, SalesTypeName = "定価", Price = 7000, Floor = 1, RowNo = 1, ColumnNo = 4 });
            SeatList.Add(new Seats { Id = 0, SeatType = 2, SeatTypeName = "A席", SalesType = 2, SalesTypeName = "前売", Price = 6000, Floor = 1, RowNo = 1, ColumnNo = 5 });
            SeatList.Add(new Seats { Id = 0, SeatType = 2, SeatTypeName = "A席", SalesType = 2, SalesTypeName = "前売", Price = 6000, Floor = 1, RowNo = 1, ColumnNo = 6 });
        }

        private void ShowSeatList()
        {
            try
            {
                var dispTextList = new List<string>();
                string dispTextElement = String.Empty;


                ManageDispSeatSwitchKey.ResetManageParameters();
                for (int i = 0; i < SeatList.Count; i++)
                {
                    ManageDispSeatSwitchKey.SetManageParameters(SeatList[i]);

                    //最後の要素もしくは、次の要素と比較して SwitchKeyが異なっていた場合、出力する。
                    if (i == SeatList.Count - 1 || (ManageDispSeatSwitchKey.IsSwitchKeyChanged(SeatList[i + 1]) == true))
                    {
                        dispTextList.Add(ManageDispSeatSwitchKey.DispText);
                    }
                }


                seatListView.ItemsSource = null;
                seatListView.ItemsSource = dispTextList;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowSeatListFromButton01()
        {
            SetSeatList01();
            ShowSeatList();
        }
        private void ShowSeatListFromButton02()
        {
            SetSeatList02();
            ShowSeatList();
        }
        private void ShowSeatListFromButton03()
        {
            SetSeatList03();
            ShowSeatList();
        }
        #endregion

        #region foreachでループのindexを取得
        private void ForEach01Button_Click()
        {
            // テスト用配列
            var array = new[] { "aaa", "bbb", "ccc" };

            // Selectメソッドで値をインデックスのペアを作ってそれをforeachでまわす
            foreach (var item in array.Select((v, i) => new { v, i }))
            {
                // 匿名型から値とインデックスを取り出して使える
                Console.WriteLine("value = {0}, index = {1}", item.v, item.i);
            }
        }
        #endregion

        #region Dictionaryの色々な使い方

        #region ＿【Dict】データ定義１
        /// 【参考】
        /// http://mocotan.hatenablog.com/entry/2017/10/31/064738
        /// 
        /// 1.まずGetHashCodeメソッドで、ハッシュ値が同じかを調べる
        /// 2.ハッシュ値が同じときは、Equalsメソッドでオブジェクトの同値性を調べる
        /// 1と2が同じだったら、同じと判断する。
        Dictionary<Employee, EmployeeDispContent> employeeDctionary = new Dictionary<Employee, EmployeeDispContent>();

        private class Employee
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public int Position { get; set; }
            public string Note { get; set; }

            //public Employee(string name, int age, int position)
            //{
            //    this.Name = name;
            //    this.Age = age;
            //    this.Position = position;
            //}


            //パっと見、別に無くてもいいんじゃね？と思えるが、使用上 Equals を実行させるために必要。高速化のため、らしい。
            public override int GetHashCode()
            {
                int _hashCode;

                _hashCode = Name.GetHashCode()
                           + Age.GetHashCode()
                           + Position.GetHashCode()
                ;

                return _hashCode;
            }


            public override bool Equals(object obj)
            {
                //キャストできない場合、一致しないと判定する。
                var compareObject = obj as Employee;
                if (compareObject == null)
                {
                    return false;
                }

                // 何が同じときに、「同じ」と判断してほしいかを記述する。
                bool isSame;

                isSame = this.Name == compareObject.Name
                        && this.Age == compareObject.Age
                        && this.Position == compareObject.Position
                ;


                return isSame;
            }

        }

        private class EmployeeDispContent
        {
            public string No { get; set; }
            public string ContentForDisplay { get; set; }
        }
        #endregion

        #region ＿【Dict】データ定義情報２（IEquatableを使う場合）
        Dictionary<User, UserDispContent> userDctionary = new Dictionary<User, UserDispContent>();

        private class User : IEquatable<User>
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public int Position { get; set; }
            public string Note { get; set; }


            public bool Equals(User compareObject)
            {
                bool isSame;

                isSame = this.Name == compareObject.Name
                        && this.Age == compareObject.Age
                        && this.Position == compareObject.Position
                ;

                return isSame;
            }


            //パっと見、別に無くてもいいんじゃね？と思えるが、使用上 Equals を実行させるために必要。高速化のため、らしい。
            public override int GetHashCode()
            {
                //int _hashCode;

                //_hashCode = Name.GetHashCode()
                //           + Age.GetHashCode()
                //           + Position.GetHashCode()
                //;

                return 0;
            }

        }

        private class UserDispContent
        {
            public string No { get; set; }
            public string ContentForDisplay { get; set; }
        }
        #endregion

        #region ＿【Dict】パラメータセット
        private void SetDctionaryParameters()
        {
            //-----< hashで判定するパターン >-----
            var iniElKey01 = new Employee { Name = "Oda", Age = 35, Position = 1 };
            var iniElVal01 = new EmployeeDispContent { No = "1", ContentForDisplay = "1番" };
            employeeDctionary.Add(iniElKey01, iniElVal01);

            var iniElKey02 = new Employee { Name = "Toyotomi", Age = 28, Position = 2 };
            var iniElVal02 = new EmployeeDispContent { No = "2", ContentForDisplay = "2番" };
            employeeDctionary.Add(iniElKey02, iniElVal02);

            var iniElKey03 = new Employee { Name = "Tokugawa", Age = 42, Position = 3 };
            var iniElVal03 = new EmployeeDispContent { No = "3", ContentForDisplay = "3番" };
            employeeDctionary.Add(iniElKey03, iniElVal03);



            //-----< IEquatableで判定するパターン >-----
            var iniElKeyA = new User { Name = "Oda", Age = 35, Position = 1 };
            var iniElValA = new UserDispContent { No = "1", ContentForDisplay = "1番" };
            userDctionary.Add(iniElKeyA, iniElValA);

            var iniElKeyB = new User { Name = "Toyotomi", Age = 28, Position = 2 };
            var iniElValB = new UserDispContent { No = "2", ContentForDisplay = "2番" };
            userDctionary.Add(iniElKeyB, iniElValB);

            var iniElKeyC = new User { Name = "Tokugawa", Age = 42, Position = 3 };
            var iniElValC = new UserDispContent { No = "3", ContentForDisplay = "3番" };
            userDctionary.Add(iniElKeyC, iniElValC);
        }
        #endregion

        #region ＿【Dict】Dictionaryの基本的な使い方
        private void Dictionary01Button_Click()
        {
            //------------
            // Dictionary  
            //------------
            Dictionary<string, int> registrants = new Dictionary<string, int>() {
              {"yamada", 50},
              {"tanaka", 80},
            };

            //追加
            registrants.Add("ishikawa", 40);

            //キーから取得
            var valueFromKey = registrants["tanaka"];
            Console.WriteLine(valueFromKey); // 80

            //値の変更
            registrants["yamada"] = 60;

            //ループ１
            foreach (KeyValuePair<string, int> registrant in registrants)
            {
                Console.WriteLine($"{registrant.Key}: {registrant.Value}");
            }

            //ループ２
            foreach (var item in registrants)
            {
                Console.WriteLine(item.Key);
                Console.WriteLine(item.Value);
            }


            //キーを含んでいるかチェック
            if (registrants.ContainsKey("kobayashi"))
            {
                Console.WriteLine("kobayashi is Contained.");
            }
            else
            {
                Console.WriteLine("kobayashi is not Contained.");
            }
        }
        #endregion

        #region ＿【Dict】キーがClassのDictionary
        private void Dictionary02Button_Click()
        {
            var tmpElement01 = new Employee { Name = "Oda", Age = 35, Position = 1 };        //キーに含まれている要素
            var tmpElement02 = new Employee { Name = "minamoto", Age = 200, Position = 99 }; //キーに含まれていない要素


            //存在する要素
            if (employeeDctionary.ContainsKey(tmpElement01))
            {
                Console.WriteLine("tmpElement01 is Contained.");
            }
            else
            {
                Console.WriteLine("tmpElement01 is not Contained.");
            }


            //存在しない要素
            if (employeeDctionary.ContainsKey(tmpElement02))
            {
                Console.WriteLine("tmpElement02 is Contained.");
            }
            else
            {
                Console.WriteLine("tmpElement02 is not Contained.");
            }



            var tmpElement03 = new Employee { Name = "Oda", Age = 999, Position = 999 };      //Nameだけ一致
            var tmpElement04 = new Employee { Name = "Oda", Age = 35, Position = 999 };      //Name と Ageだけ一致
            var tmpElement05 = new Employee { Name = "nooooo", Age = 35, Position = 1 };        //Age と Position だけ一致

            //tmpElement03
            if (employeeDctionary.ContainsKey(tmpElement03))
            {
                Console.WriteLine("tmpElement03 is Contained.");
            }
            else
            {
                Console.WriteLine("tmpElement03 is not Contained.");
            }

            //tmpElement04
            if (employeeDctionary.ContainsKey(tmpElement04))
            {
                Console.WriteLine("tmpElement04 is Contained.");
            }
            else
            {
                Console.WriteLine("tmpElement04 is not Contained.");
            }

            //tmpElement05
            if (employeeDctionary.ContainsKey(tmpElement05))
            {
                Console.WriteLine("tmpElement05 is Contained.");
            }
            else
            {
                Console.WriteLine("tmpElement05 is not Contained.");
            }

        }
        #endregion

        #region ＿【Dict】IEquatableでの比較
        private void Dictionary03Button_Click()
        {
            var tmpElement01 = new User { Name = "Oda", Age = 35, Position = 1 };        //キーに含まれている要素
            var tmpElement02 = new User { Name = "minamoto", Age = 200, Position = 99 }; //キーに含まれていない要素


            //存在する要素
            if (userDctionary.ContainsKey(tmpElement01))
            {
                Console.WriteLine("tmpElement01 is Contained.");
            }
            else
            {
                Console.WriteLine("tmpElement01 is not Contained.");
            }


            //存在しない要素
            if (userDctionary.ContainsKey(tmpElement02))
            {
                Console.WriteLine("tmpElement02 is Contained.");
            }
            else
            {
                Console.WriteLine("tmpElement02 is not Contained.");
            }

        }
        #endregion


        #endregion
    }
}
