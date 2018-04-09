using System;
using System.Windows;

namespace PracticeWPF
{
    /// <summary>
    /// デリゲート・イベント
    /// </summary>
    public partial class MyWindow17 : Window
    {
        #region 初期設定
        public MyWindow17()
        {
            InitializeComponent();

            SetEvent();
        }

        private void SetEvent()
        {
            this.Button01.Click += (sender, e) => button01_Click_addedEvent();
            this.Button02.Click += (sender, e) => button02_Click_addedEvent();
        }
        #endregion

        #region デリゲート
        static class DelegateSample01
        {
            public delegate void sumDelegate(int a, int b);

            static void DelegateMethod01()
            {
                // メソッドをデリゲートに代入
                sumDelegate sumDele = Sum;
                // 引数を入れて、Sumメソッドを使う
                sumDele(1, 2);
            }

            /// 引数を足し合わせて、コンソールに表示します。
            public static void Sum(int a, int b)
            {
                int sum = a + b;
                Console.WriteLine(sum.ToString());
            }
        }

        private void button01_Click_addedEvent()
        {
            DelegateSample01.Sum(1, 3);
        }
        #endregion

        #region イベントハンドラ
        private void button02_Click_addedEvent()
        {

        }

        #endregion
    }
}
