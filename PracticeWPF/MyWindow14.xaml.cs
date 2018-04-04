using System;
using System.Windows;
using System.Windows.Controls;

namespace PracticeWPF
{
    /// <summary>
    /// 動的に配置したボタンにイベントを定義
    /// </summary>
    public partial class MyWindow14 : Window
    {
        public MyWindow14()
        {
            InitializeComponent();

            SetButtonDynamicEvent();
            SetElementDynamicEvent();
        }
        private void SetButtonDynamicEvent()
        {
            Button b1 = new Button();
            b1.Content = "動的に配置したボタン１";
            b1.Name = "dynamicButton01";
            b1.Click += (sender, e) => ButtonDynamicEvent(sender);
            myStackPanel01.Children.Add(b1);

            Button b2 = new Button();
            b2.Content = "動的に配置したボタン２";
            b2.Name = "dynamicButton02";
            b2.Click += (sender, e) => ButtonDynamicEvent(sender);
            myStackPanel01.Children.Add(b2);
        }

        private void ButtonDynamicEvent(object sender)
        {
            Console.WriteLine(((Button)sender).Name + "がクリックされました。");
        }





        private void SetElementDynamicEvent()
        {
            CheckBox e1 = new CheckBox();
            e1.Content = "Dynamic Element01";
            e1.Name = "e1";
            e1.Click += (sender, e) => ElementDynamicEvent(sender);
            myStackPanel01.Children.Add(e1);

            RadioButton e2 = new RadioButton();
            e2.Content = "Dynamic Element02";
            e2.Name = "e2";
            e2.Click += (sender, e) => ElementDynamicEvent(sender);
            myStackPanel01.Children.Add(e2);

            TextBox e3 = new TextBox();
            e3.Text = "Dynamic Element03";
            e3.Name = "e3";
            e3.MouseEnter += (sender, e) => ElementDynamicEvent(sender);
            myStackPanel01.Children.Add(e3);
        }

        private void ElementDynamicEvent(object sender)
        {
            Console.WriteLine(((FrameworkElement)sender).Name + "がクリックされました。");
        }

    }
}
