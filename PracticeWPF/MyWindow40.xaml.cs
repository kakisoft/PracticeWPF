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
            myButton03.Click += (sender, e) => MyButton03_Click();
        }
        #endregion

        #region － Action １－
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

        #region － Action ２－
        private void MyButton02_Click()
        {
            ActionSample02(this.ArgMethod02, 1);  //引数が必要なメソッドでも、渡し方は同様。
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

        #region － Func １－
        //Func<T, TResult> デリゲート
        //
        //Encapsulates a method that has one parameter and returns a value of the type specified by the TResult paramete
        //1 つのパラメーターを受け取って TResult パラメーターに指定された型の値を返すメソッドをカプセル化します。
        private void MyButton03_Click()
        {
            //戻り値がboolのFuncをラムダで生成
            Func<bool> func01 = () => {
                MessageBox.Show("execute func01");  //この時点では実行されない
                return true;
            };


            //この時点で実行される
            func01.Invoke();



            Func<string, int, bool> func02 = (name, age) => {  // Func<> 最後に指定した型が、戻り値になる(TResult)
                MessageBox.Show("name:" + name + "   age:" + age);
                return true;
            };


            func02.Invoke("yamada", 20);


            Func<string, int> func03;
            func03 = (aa) =>
            {
                return 0;
            };
        }
        #endregion
    }
}
