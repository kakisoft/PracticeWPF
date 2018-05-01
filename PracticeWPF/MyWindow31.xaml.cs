using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PracticeWPF
{
    /// <summary>
    /// ReactivePropertyを使った双方向バインド
    /// </summary>
    public partial class MyWindow31 : Window
    {
        //=========================
        // NuGet:
        //   Install-Package ReactiveProperty
        //=========================

        #region 初期化
        public MyWindow31()
        {
            InitializeComponent();

            InitializeThisWindowsParameters();
            AddMyEvent();
        }

        private void InitializeThisWindowsParameters()
        {
            //myStackPanel01.DataContext = new MainWindowViewModel();
            this.DataContext = new MainWindowViewModel();

            //※ パラメータの設定に、「.Value」が必要。
        }

        private void AddMyEvent()
        {
            //myButton01.Click += (sender, e) => MyButton01_Click();
            //myButton02.Click += (sender, e) => MyButton02_Click();
        }
        #endregion


        public class MainWindowViewModel
        {
            public ReactiveProperty<string> Input { get; private set; }
            public ReactiveProperty<string> Output { get; private set; }

            public ReactiveCommand ClearCommand { get; private set; }

            public MainWindowViewModel()
            {
                this.Input = new ReactiveProperty<string>(""); // デフォルト値を指定してReactivePropertyを作成
                this.Output = this.Input
                    //.Delay(TimeSpan.FromSeconds(1)) // 1秒間待機して
                    .Select(x => x.ToUpper()) // 大文字に変換して
                    .ToReactiveProperty(); // ReactiveProperty化する

                this.ClearCommand = this.Input
                    .Select(x => !string.IsNullOrWhiteSpace(x)) // Input.Valueが空じゃないとき
                    .ToReactiveCommand(); // 実行可能なCommandを作る
                this.ClearCommand.Subscribe(_ => this.Input.Value = "");
            }
        }
    }
}
