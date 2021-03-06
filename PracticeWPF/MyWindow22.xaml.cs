﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
            myButton04.Click += (sender, e) => MyButton04_Click();
            myButton05.Click += (sender, e) => MyButton05_Click();
            myButton06.Click += (sender, e) => MyButton06_Click();
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
            Console.WriteLine(type);  //出力結果：System.Int32

            //変数から取得
            int i = 0;
            System.Type type01 = i.GetType();
            Console.WriteLine(type01);  //出力結果：System.Int32

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
        //親クラス
        class BaseClass
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Position { get; set; }
        }
        //子クラス
        class ExtendClass : BaseClass
        {
            public bool IsChecked { get; set; } //

            public ExtendClass(BaseClass value)
            {
                PropertyInfo[] propertyInfoinfoArray = value.GetType().GetProperties();
                foreach (PropertyInfo item in propertyInfoinfoArray)
                {
                    Console.WriteLine(item.Name + ": " + item.GetValue(this, null));
                    Console.WriteLine(item.Name + ": " + item.GetValue(this));

                    //-------OK
                    var property1 = typeof(BaseClass).GetProperty("Name");

                    //インスタンスの値を取得
                    var beforeName = property1.GetValue(item);

                    // インスタンスに値を設定
                    property1.SetValue(this, "newName");

                    //-----------------
                    //
                    //-----------------
                    var property = value.GetType().GetProperty(item.Name);

                    // インスタンスに値を設定
                    property.SetValue(this, item.GetValue(value));

                }

                this.Id = value.Id;
            }
        }

        private List<BaseClass> BaseClasses;
        private List<ExtendClass> ExtendClasses;

        private void MyButton04_Click()
        {
            BaseClasses = new List<BaseClass>();
            BaseClasses.Add(new BaseClass { Id = 1, Name = "Tanaka"  , Position = 1 });
            BaseClasses.Add(new BaseClass { Id = 2, Name = "Yamada"  , Position = 1 });
            BaseClasses.Add(new BaseClass { Id = 3, Name = "Watanabe", Position = 2 });

            ExtendClasses = new List<ExtendClass>();
            foreach (var item in BaseClasses)
            {
                ExtendClass el = new ExtendClass(item);
                ExtendClasses.Add(el);
            }
        }
        #endregion

        #region List
        List<RadioButton> RadioButtonElementList = new List<RadioButton>();
        List<CheckBox> CheckBoxElementList = new List<CheckBox>();
        List<Button> ButtonElementList = new List<Button>();

        List<ToggleButton> UpperRadioButtonElementList = new List<ToggleButton>();
        List<FrameworkElement> UpperElementList = new List<FrameworkElement>();
        List<UIElement> MostUpperElementList = new List<UIElement>();
        private void MyButton05_Click()
        {
            //ToggleButton の Listには、RadioButtonとCheckBoxを格納できる
            UpperRadioButtonElementList.Add(new RadioButton());
            UpperRadioButtonElementList.Add(new CheckBox());


            //FrameworkElement の Listには、ButtonやTextBoxも格納できる
            UpperElementList.Add(new Button());
            UpperElementList.Add(new TextBox());


            //Elementなら、もう何でも
            MostUpperElementList.Add(new Window());
            MostUpperElementList.Add(new Page());

        }
        #endregion

        #region 仮想メソッド（virtual method）
        class Parent01
        {
            //【 virtual 】
            //子クラスで override されるのが前提のメソッドに付ける修飾子、という認識でいいのだろうか。
            //abstract との違いは、直接コールもできる、という点。（ base.ParentMethodName() という感じで。）
            public virtual void SampleVirtualMethod01()
            {
                Console.WriteLine("Parent01.SampleVirtualMethod01()");
            }
        }

        class Child01 : Parent01
        {
            public override void SampleVirtualMethod01()
            {
                Console.WriteLine("Child01.SampleVirtualMethod01()");
            }

            public void CallParentsVirtualMethod01()
            {
                base.SampleVirtualMethod01();
            }
        }

        private void MyButton06_Click()
        {
            Parent01 _parent01 = new Parent01();
            _parent01.SampleVirtualMethod01();       // Parent01 の SampleVirtualMethod01 がコールされる。

            Parent01 _inChildParent = new Child01();
            _inChildParent.SampleVirtualMethod01();  // Child01 の SampleVirtualMethod01 がコールされる。

            Child01 _chilc01 = new Child01();
            _chilc01.SampleVirtualMethod01();        // Child01 の SampleVirtualMethod01 がコールされる。
            _chilc01.CallParentsVirtualMethod01();   // Child01 の CallParentsVirtualMethod01 を経由して、Parent01 の SampleVirtualMethod01() がコールされる。
        }
        #endregion

    }
}