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
    /// typeof・nameof・GUID・キャスト
    /// </summary>
    public partial class MyWindow22 : Window
    {
        #region 初期化
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
            myButton03.Click += (sender, e) => MyButton03_Click();
        }
        #endregion

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

        #region キャスト
        /*
               +------------+
               |  親クラス  |
               +------------+
                     |  ↑アップキャスト
                     |
                     |
                     |  ↓ダウンキャスト
               +------------+
               |  子クラス  |
               +------------+


            ・アップキャストは、常に安全に行える
            ・ダウンキャストは、エラーが発生する事がある。

        　　※キャストは型を変更するだけで、中身が消える訳ではない。（アクセスできる範囲が変わるだけで、実態は存在している）
          　※インスタンス作成時、メモリに領域が割り当てられる。
            　子クラスのインスタンスは、親クラスのインスタンスより多くの領域を必用とする。
            　親→子　とキャストした場合、子クラスが必要とする領域に、メモリが割り当てられていない状態となる。
            　（そのため、エラーとして弾いている。）
        */

        //親クラス
        class ParentClass { }
        //子クラス
        class ChildClass : ParentClass { }

        private void MyButton03_Click()
        {
            //===========================
            //      アップキャスト
            //===========================
            ParentClass parent01;
            ChildClass child01;

            //子クラスのインスタンスを生成
            child01 = new ChildClass();

            //子クラスのインスタンスを、親クラスに代入可能。
            parent01 = child01;


            //===========================
            //  ダウンキャスト（エラー）
            //===========================
            ParentClass parent02;
            ChildClass child02;

            //親クラスのインスタンスを生成
            parent02 = new ParentClass();

            //キャスト
            if (parent02 is ChildClass)
            {
                //キャスト時にエラーが発生する
                child02 = (ChildClass)parent02;
            }

            //asを使えば nullが入る
            child02 = parent02 as ChildClass;


            //===========================
            //     ダウンキャスト
            //===========================
            ParentClass parent03;
            ChildClass child03;

            //親クラスのインスタンスを、子クラスで生成
            parent03 = new ChildClass();

            //ダウンキャスト可。
            child03 = (ChildClass)parent03;
        }
        #endregion

        #region ダウンキャスト

        #endregion
    }
}