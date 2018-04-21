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
    /// MyWindow22.xaml の相互作用ロジック
    /// </summary>
    public partial class MyWindow22 : Window
    {
        //private int sample01 = 0;

        public MyWindow22()
        {
            InitializeComponent();
            AddMyEvent();
        }

        private void AddMyEvent()
        {
            myButton01.Click += (sender, e) => MyButton01_Click();
            myButton02.Click += (sender, e) => MyButton02_Click();
        }

        #region typeof
        private void MyButton01_Click()
        {
            //===================
            //      typeof
            //===================

            //型を取得する時に使用する
            System.Type type = typeof(int);

            //変数から取得
            int i = 0;
            System.Type type01 = i.GetType();

            //
            Type t = typeof(MyWindow22);

            Console.WriteLine("Methods:");
            System.Reflection.MethodInfo[] methodInfo = t.GetMethods();

            foreach (System.Reflection.MethodInfo mInfo in methodInfo)
                Console.WriteLine(mInfo.ToString());

            Console.WriteLine("Members:");
            System.Reflection.MemberInfo[] memberInfo = t.GetMembers();

            foreach (System.Reflection.MemberInfo mInfo in memberInfo)
                Console.WriteLine(mInfo.ToString());
        }
        #endregion

        #region nameof
        private void MyButton02_Click()
        {
            Guid guidValue = Guid.NewGuid();
            string sample01 = guidValue.ToString();

            string variableName = nameof(sample01);

            Console.WriteLine(variableName);
        }
        #endregion
    }
}