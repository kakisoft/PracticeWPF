using System.Windows;
using System.Windows.Controls;

namespace PracticeWPF
{
    /// <summary>
    /// Gridの指定位置に要素を追加
    /// </summary>
    public partial class MyWindow19 : Window
    {
        public MyWindow19()
        {
            InitializeComponent();

            SetGridItems();
        }

        private void SetGridItems()
        {
            Button b0_0 = new Button();
            b0_0.Content = "0-0";
            b0_0.SetValue(Grid.RowProperty, 0);
            b0_0.SetValue(Grid.ColumnProperty, 0);
            myGrid01.Children.Add(b0_0);

            Button b0_2 = new Button();
            b0_2.Content = "0-2";
            b0_2.SetValue(Grid.RowProperty, 0);
            b0_2.SetValue(Grid.ColumnProperty, 2);
            myGrid01.Children.Add(b0_2);

            Button b1_1 = new Button();
            b1_1.Content = "1-1";
            b1_1.SetValue(Grid.RowProperty, 1);
            b1_1.SetValue(Grid.ColumnProperty, 1);
            myGrid01.Children.Add(b1_1);

            Button b2_2 = new Button();
            b2_2.Content = "2-2";
            b2_2.SetValue(Grid.RowProperty, 2);
            b2_2.SetValue(Grid.ColumnProperty, 2);
            myGrid01.Children.Add(b2_2);
        }
    }
}
