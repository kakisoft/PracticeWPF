using System;
using System.Collections.Generic;
using System.Windows;

namespace PracticeWPF
{
    /// <summary>
    /// リスト要素のデータとのバインディング１
    /// </summary>
    public partial class MyWindow06 : Window
    {
        public MyWindow06()
        {
            InitializeComponent();

            //DataContext.Name = "ああ";
            CreateSampleInstance();
        }

        #region サンプルクラス
        private User _user1;
        //private User _user2;
        public class User
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public bool Ismarried { get; set; }
        }
        #endregion

        #region コンテキストをセット
        private void CreateSampleInstance()
        {
            //===========================
            //       Sample05-1
            //===========================
            _user1 = new User
            {
                Name = "Kaki",
                Age = 30,
                Ismarried = true,
            };
            //this.DataContext = _user;
            MyStackPanel01.DataContext = _user1;

            //===========================
            //       Sample05-2
            //===========================
            User _user2 = new User
            {
                Name = "Ogawa",
                Age = 28,
                Ismarried = true,
            };
            MyStackPanel02.DataContext = _user2;
            //子コントロールのコンテキストに値をセット
            MyTextBox13.DataContext = new User { Name = "mi", Age = 35, Ismarried = false };

            //===========================
            //       Sample05-3
            //===========================
            List<User> userGroup1 = new List<User>();
            userGroup1.Add(_user1);
            userGroup1.Add(new User { Name = "Yamato", Age = 21, Ismarried = false });
            MyStackPanel03.DataContext = userGroup1;

        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(_user1.Ismarried);
        }
    }

}
