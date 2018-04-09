using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace PracticeWPF
{
    /// <summary>
    /// チェックボックスとの双方向バインディング（応用）
    /// </summary>
    public partial class MyWindow09 : Window
    {
        DateFormatCluster _dateFormatCluster = new DateFormatCluster();

        private User _user1;
        private List<User> _userList1;

        List<User> SearchKogyoNameHeadCharacters = new List<User>();

        public MyWindow09()
        {
            InitializeComponent();

            InitializeThisWindowsParameter();
        }

        #region バインド設定
        /// <summary>
        /// 画面固有の内容の初期設定
        /// </summary>
        private void InitializeThisWindowsParameter()
        {
            SetBindMyStackPanel01();
            SetBindMyStackPanel02();
            SetBindMyStackPanel03();
        }

        private void SetBindMyStackPanel01()
        {
            _user1 = new User();
            _user1.Name = "kaki";
            MyStackPanel01.DataContext = _user1;
        }

        private void SetBindMyStackPanel02()
        {
            _userList1 = new List<User>();
            _userList1.Add(new User { Name = "tanaka", Age = 22, IsMarried = false, });
            _userList1.Add(new User { Name = "sawada", Age = 35, IsMarried = true, });
            _userList1.Add(new User { Name = "saitou", Age = 41, IsMarried = false, });
            _userList1.Add(new User { Name = "harada", Age = 22, IsMarried = false, });
            _userList1.Add(new User { Name = "kitano", Age = 26, IsMarried = false, });

            MyStackPanel02.DataContext = _userList1;
        }

        private void SetBindMyStackPanel03()
        {
            _dateFormatCluster.BaseText = "20180621";
            MyStackPanel03.DataContext = _dateFormatCluster;
        }

        #endregion


        #region バインディング用クラス
        private class User : BindableBase
        {
            private string name;
            public string Name
            {
                get { return this.name; }
                set { this.SetProperty(ref this.name, value); }
            }

            private int age;
            public int Age
            {
                get { return this.age; }
                set { this.SetProperty(ref this.age, value); }
            }

            private bool isMarried;
            public bool IsMarried
            {
                get { return this.isMarried; }
                set { this.SetProperty(ref this.isMarried, value); }
            }

            /// <summary>
            /// クエリスタイルで取得
            /// </summary>
            public string GetNameOfQueryStyle
            {
                get
                {
                    string nemeOfQueryStyle = "'" + name + "'";

                    return nemeOfQueryStyle;
                }
            }

        }

        private class DateFormatCluster : BindableBase
        {
            private string baseText;
            public string BaseText
            {
                get { return this.baseText; }
                set { this.SetProperty(ref this.baseText, value); }
            }

            public string GetFormat1
            {
                get
                {
                    string format1 = String.Empty;
                    format1 += baseText;


                    return format1;
                }
            }
            public string GetFormat2
            {
                get
                {
                    string format2 = String.Empty;
                    format2 += "na";

                    return format2;
                }
            }

        }

        #endregion

        #region バンディング用ベースクラス
        public class BindableBase : INotifyPropertyChanged
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

        /// <summary>
        /// 双方向バインド動作確認：テキストボックス
        /// </summary>
        private void MyButton01_Click(object sender, RoutedEventArgs e)
        {
            _user1.Name += "x";
        }

        /// <summary>
        /// 双方向バインド動作確認：チェックボックスをすべてON
        /// </summary>
        private void MyButton02_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var user in _userList1)
                {
                    user.IsMarried = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 双方向バインド動作確認：チェックボックスをすべてOFF
        /// </summary>
        private void MyButton03_Click(object sender, RoutedEventArgs e)
        {
            foreach (var user in _userList1)
            {
                user.IsMarried = false;
            }
        }

        /// <summary>
        /// チェック対象の値を取得１
        /// </summary>
        private void MyButton04_Click(object sender, RoutedEventArgs e)
        {
            var targetParamList1 = _userList1
                                      .Where(x => x.IsMarried)
                                      .Select(x => x.GetNameOfQueryStyle);

            var targetParam1 = string.Join(",", targetParamList1);

            Console.WriteLine(targetParam1);
        }

        /// <summary>
        /// チェック対象の値を取得２
        /// </summary>
        private void MyButton05_Click(object sender, RoutedEventArgs e)
        {
            var targetParamList2 = _userList1
                                      .Where(x => x.IsMarried)
                                      .Select(x => "'" + x.Name + "'");


            var targetParam2 = string.Join(",", targetParamList2);

            Console.WriteLine(targetParam2);

        }

        private void MyButton06_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //  2018/03/23 04:14:39
                Console.WriteLine(String.Format("{0:yyyy/MM/dd hh:mm:ss}", DateTime.Now));

                //  2018/03/23 (金) 16:14:44
                DateTime dtNow = DateTime.Now;
                string stPrompt1 = dtNow.ToString("yyyy/MM/dd (ddd) HH:mm:ss");
                Console.WriteLine(stPrompt1);

                //  2018年03月23日 (金曜日) 午後 04時14分44秒
                string stPrompt2 = dtNow.ToString("yyyy年MM月dd日 (dddd) tt hh時mm分ss秒");
                Console.WriteLine(stPrompt2);

                //  2018年3月23日 (金)
                string stPrompt3 = dtNow.ToString("yyyy年M月d日 (ddd)");
                Console.WriteLine(stPrompt3);

                //  2017/02/16 12:15:12
                string sa1 = "2017/2/16 12:15:12";
                DateTime dt1 = DateTime.Parse(sa1);
                Console.WriteLine(dt1);

                //  2018/02/16 0:00:00
                string sa2 = "2018/2/16";
                DateTime dt2 = DateTime.Parse(sa2);
                Console.WriteLine(dt2);

                // エラー
                //string s3 = "201802016";
                //DateTime dt3 = DateTime.Parse(s3);
                //Console.WriteLine(dt3);

                // あいうえおカキクケコｻｼｽｾｿnaninuneno１２３４５６７８９０
                string sb1 = "あいうえおカキクケコｻｼｽｾｿnaninuneno1234567890";
                var sbx1 = Regex.Replace(sb1, "[0-9]", p => ((char)(p.Value[0] - '0' + '０')).ToString());
                Console.WriteLine(sbx1);

                //  ２０１８/０３/２３
                string sb2 = "2018/03/23";
                var sbx2 = Regex.Replace(sb2, "[0-9]", p => ((char)(p.Value[0] - '0' + '０')).ToString());
                Console.WriteLine(sbx2);

                // "20180621" → "２０１８年６月１日 (金)"
                string date1 = "20180601";
                string date2 = date1.Substring(0, 4) + "/" + date1.Substring(4, 2) + "/" + date1.Substring(6, 2);
                DateTime date3 = DateTime.Parse(date2);
                string date4 = date3.ToString("yyyy年M月d日 (ddd)");
                string date5 = Regex.Replace(date4, "[0-9]", p => ((char)(p.Value[0] - '0' + '０')).ToString());

                Console.WriteLine(date5);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

}
