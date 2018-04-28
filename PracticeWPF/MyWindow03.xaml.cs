using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PracticeWPF
{
    /// <summary>
    /// Window.Resources を参照してプロパティを設定
    /// </summary>
    public partial class MyWindow03 : Window
    {
        #region Enum定義
        /// <summary>
        /// 確保条件
        /// </summary>
        enum Signal:int
        {
            Red = 0,
            Yellow = 1,
            Blue = 2
        }
        Dictionary<int, string> _signalDict = new Dictionary<int, string>() {
            {(int)Signal.Red   , "赤"},
            {(int)Signal.Yellow, "黄色"},
            {(int)Signal.Blue  , "青" + Environment.NewLine + "と緑"},
        };
        #endregion

        public MyWindow03()
        {
            InitializeComponent();

        }

        private void enumForeach()
        {
            //-----< enum を foreachで回す >-----
            foreach (int r in Enum.GetValues(typeof(Signal)))
            {
                var t = new RadioButton();
                t.Name = "enumRadioButton_" + r;

                var b = new TextBlock();
                b.Text = _signalDict[r];
                b.TextWrapping = TextWrapping.Wrap;
                t.Content = b;

                t.Tag = r;
            }
        }
    }
}
