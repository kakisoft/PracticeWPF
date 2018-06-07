using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Action・Func
    /// </summary>
    public partial class MyWindow40 : Window
    {
        #region 初期化
        public MyWindow40()
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
            myButton02.Click += (sender, e) => MyButton02_Click();
        }
        #endregion

        #region －１－
        private void MyButton01_Click()
        {
            ActionSample01(this.ArgMethod01);
        }

        private void ArgMethod01()
        {
            MessageBox.Show("message!");
        }


        private void ActionSample01(Action execute)
        {
            execute();
        }
        #endregion

        #region －２－
        private void MyButton02_Click()
        {
            ActionSample02(this.ArgMethod02, 1);  //引数が必要なメソッドも、渡し方は同様。
        }

        private void ArgMethod02(int i)
        {
            MessageBox.Show("message!  ：" + i);
        }

        private void ActionSample02(Action<int> execute, int i)
        {
            execute(i);
        }
        #endregion
    }
}
