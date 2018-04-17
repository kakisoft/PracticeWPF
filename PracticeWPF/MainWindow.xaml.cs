using System;
using System.Windows;

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
                var myWindow02 = new MyWindow02();
                //myWindow02.Show();
                myWindow02.ShowDialog(); //別ウィンドウが閉じるまで、呼び出し元は非アクティブ
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MyWindow03button_Click(object sender, RoutedEventArgs e)
        {
            new MyWindow03().ShowDialog();
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
            MyWindow14button.Click += (sender, e) => new MyWindow14().ShowDialog();
            MyWindow15button.Click += (sender, e) => new MyWindow15().ShowDialog();
            MyWindow16button.Click += (sender, e) => new MyWindow16().ShowDialog();
            MyWindow17button.Click += (sender, e) => new MyWindow17().ShowDialog();
            MyWindow18button.Click += (sender, e) => new MyWindow18().ShowDialog();
            MyWindow19button.Click += (sender, e) => new MyWindow19().ShowDialog();
            MyWindow20button.Click += (sender, e) => new MyWindow20().ShowDialog();
            MyWindow21button.Click += (sender, e) => new MyWindow21().ShowDialog();
        }

    }

}
