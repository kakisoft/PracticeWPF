﻿using PracticeWPF.ViewModelSample03;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PracticeWPF
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //----< メッセージを表示 >-----
            MessageBox.Show("入力情報をクリアしました。", "", MessageBoxButton.OK, MessageBoxImage.Information);


            var resut01 = MessageBox.Show("YesNo", "", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (resut01 == MessageBoxResult.OK)
            {

            }

            var resut02 = MessageBox.Show("YesNoCancel", "", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);
            if (resut01 == MessageBoxResult.Cancel)
            {

            }

        }


        #region サンプルクラス
        private User _user;
        public class User
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public bool IsMarried { get; set; }
        }

        private void CreateSampleInstance()
        {
            _user = new User
            {
                Name = "Kaki",
                Age = 30,
                IsMarried = true,
            };
            //this.DataContext = _user;
        }
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            //バインディング
            this.DataContext = new { X = 10, Y = 20 };

            //画面遷移ボタンのイベント定義
            SetScreenTransitionBottonEvent();
        }

        private void MyWindow01button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var myWindow01 = new MyWindow01();
                myWindow01.Show(); //呼び出し元はアクティブ状態
                //myWindow01.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MyWindow02button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //呼び出し元の画面を非表示
                //((MainWindow)((Button)sender).DataContext).Visibility = Visibility.Hidden;


                var myWindow02 = new MyWindow02();
                //myWindow02.Show();
                myWindow02.ShowDialog(); //別ウィンドウが閉じるまで、呼び出し元は非アクティブ


                //呼び出し元の画面を表示
                //((MainWindow)((Button)sender).DataContext).Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MyWindow03button_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;


            new MyWindow03().ShowDialog();


            this.Visibility = Visibility.Visible;
        }

        private void MyWindow04button_Click(object sender, RoutedEventArgs e) => new MyWindow04().ShowDialog();
        private void MyWindow05button_Click(object sender, RoutedEventArgs e) => new MyWindow05().ShowDialog();
        private void MyWindow06button_Click(object sender, RoutedEventArgs e) => new MyWindow06().ShowDialog();
        private void MyWindow07button_Click(object sender, RoutedEventArgs e) => new MyWindow07().ShowDialog();
        private void MyWindow08button_Click(object sender, RoutedEventArgs e) => new MyWindow08().ShowDialog();
        private void MyWindow09button_Click(object sender, RoutedEventArgs e) => new MyWindow09().ShowDialog();
        private void MyWindow10button_Click(object sender, RoutedEventArgs e) => new MyWindow10().ShowDialog();
        private void MyWindow11button_Click(object sender, RoutedEventArgs e) => new MyWindow11().ShowDialog();
        private void MyWindow12button_Click(object sender, RoutedEventArgs e) => new MyWindow12().ShowDialog();
        private void MyWindow13button_Click(object sender, RoutedEventArgs e) => new MyWindow13().ShowDialog();

        private void SetScreenTransitionBottonEvent()
        {
            MVVM_SingleFileStyle01.Click += (sender, e) => new SingleFileStyle01().ShowDialog();
            MVVM_SingleFileStyle02.Click += (sender, e) => new SingleFileStyle02().ShowDialog();
            MVVM_SingleFileStyle03.Click += (sender, e) => new SingleFileStyle03().ShowDialog();
            MVVM_SingleFileStyle05.Click += (sender, e) => new SingleFileStyle05().ShowDialog();
            MVVM_SingleFileStyle06.Click += (sender, e) => new SingleFileStyle06().ShowDialog();



            MyWindow14button.Click += (sender, e) => new MyWindow14().ShowDialog();
            MyWindow15button.Click += (sender, e) => new MyWindow15().ShowDialog();
            MyWindow16button.Click += (sender, e) => new MyWindow16().ShowDialog();
            MyWindow17button.Click += (sender, e) => new MyWindow17().ShowDialog();
            MyWindow18button.Click += (sender, e) => new MyWindow18().ShowDialog();
            MyWindow19button.Click += (sender, e) => new MyWindow19().ShowDialog();
            MyWindow20button.Click += (sender, e) => new MyWindow20().ShowDialog();
            MyWindow21button.Click += (sender, e) => new MyWindow21().ShowDialog();
            MyWindow22button.Click += (sender, e) => new MyWindow22().ShowDialog();
            MyWindow23button.Click += (sender, e) => new MyWindow23().ShowDialog();
            MyWindow24button.Click += (sender, e) => new MyWindow24().ShowDialog();
            MyWindow25button.Click += (sender, e) => new MyWindow25().ShowDialog();
            MyWindow26button.Click += (sender, e) => new MyWindow26().ShowDialog();
            MyWindow27button.Click += (sender, e) => new MyWindow27().ShowDialog();
            MyWindow28button.Click += (sender, e) => new MyWindow28().ShowDialog();
            MyWindow29button.Click += (sender, e) => new MyWindow29().ShowDialog();
            MyWindow30button.Click += (sender, e) => new MyWindow30().ShowDialog();

            MyWindow31button.Click += (sender, e) => new MyWindow31().ShowDialog();
            MyWindow32button.Click += (sender, e) => new MyWindow32().ShowDialog();
            MyWindow33button.Click += (sender, e) => new MyWindow33().ShowDialog();
            MyWindow34button.Click += (sender, e) => new MyWindow34().ShowDialog();
            MyWindow35button.Click += (sender, e) => new MyWindow35().ShowDialog();
            MyWindow36button.Click += (sender, e) => new MyWindow36().ShowDialog();
            MyWindow37button.Click += (sender, e) => new MyWindow37().ShowDialog();
            MyWindow38button.Click += (sender, e) => new MyWindow38().ShowDialog();
            MyWindow39button.Click += (sender, e) => new MyWindow39().ShowDialog();
            MyWindow40button.Click += (sender, e) => new MyWindow40().ShowDialog();
            MyWindow41button.Click += (sender, e) => new MyWindow41().ShowDialog();
            MyWindow42button.Click += (sender, e) => new MyWindow42().ShowDialog();
        }

    }

}

/*
TextBlock：横中央揃えができる、縦方向の中央揃えができない
TextBox：横・縦の中央揃えができる
Label：横・縦の中央揃えができる
*/
