using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// MyWindow42.xaml の相互作用ロジック
    /// </summary>
    public partial class MyWindow42 : Window
    {
        #region 初期化
        public MyWindow42()
        {
            InitializeComponent();

            InitializeThisWindowsParameters();
            AddMyEvent();
        }

        private void InitializeThisWindowsParameters()
        {

        }

        private void AddMyEvent()
        {
            myButton01.Click += (sender, e) => MyButton01_Click();
        }
        #endregion

        #region CallerMemberName
        /// <summary>
        /// 【 参考サイト 】
        /// http://goodtech.co.jp/it/asp/obtain-caller-infomation/
        /// 
        /// ソースファイルの完全パス名の取得 ： CallerFilePathAttribute
        /// メソッド名（プロパティ名）       ： CallerMemberNameAttribute
        /// ソースファイルの行番号           ： CallerLineNumberAttribute
        /// </summary>
        private void MyButton01_Click()
        {

            CallerSample01("MessageSample01");

        }

        public static bool CallerSample01(
                                           string message,
                                           [CallerFilePath]   string callerFilePath = "",
                                           [CallerMemberName] string callerMemberName = "",
                                           [CallerLineNumber] int callerLineNumber = 0
                                         )
        {

            Console.WriteLine(callerFilePath);    // F:\Csharp\WPF\PracticeWPF\PracticeWPF\MyWindow42.xaml.cs
            Console.WriteLine(callerMemberName);  // MyButton01_Click
            Console.WriteLine(callerLineNumber);  // 55

            return true;
        }
        #endregion
    }
}
