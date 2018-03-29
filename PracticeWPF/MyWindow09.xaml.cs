using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PracticeWPF
{
    /// <summary>
    /// MyWindow09.xaml の相互作用ロジック
    /// </summary>
    public partial class MyWindow09 : Window
    {
        private User _user1;
        private List<User> _userList1;

        public MyWindow09()
        {
            InitializeComponent();

            InitializeThisWindowsParameter();
        }

        #region バインド設定
        /// <summary>
        /// 画面固有の内容の初期設定
        /// </summary>
        private void InitializeThisWindowsParameter()
        {
            SetBindMyStackPanel01();
            SetBindMyStackPanel02();
        }

        private void SetBindMyStackPanel01()
        {
            _user1 = new User();
            _user1.Name = "kaki";
            MyStackPanel01.DataContext = _user1;
        }

        private void SetBindMyStackPanel02()
        {
            _userList1 = new List<User>();
            _userList1.Add(new User { Name = "tanaka", Age = 22, IsMarried = false, });
            _userList1.Add(new User { Name = "sawada", Age = 35, IsMarried = true, });
            _userList1.Add(new User { Name = "saitou", Age = 41, IsMarried = false, });

            MyStackPanel02.DataContext = _userList1;
        }
        #endregion


        #region バインディング用クラス
        private class User : BindableBase
        {
            private string name;
            public string Name
            {
                get { return this.name; }
                set { this.SetProperty(ref this.name, value); }
            }

            private int age;
            public int Age
            {
                get { return this.age; }
                set { this.SetProperty(ref this.age, value); }
            }

            private bool isMarried;
            public bool IsMarried
            {
                get { return this.isMarried; }
                set { this.SetProperty(ref this.isMarried, value); }
            }

        }
        #endregion

        #region バンディング用ベースクラス
        public class BindableBase : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
            {
                if (Equals(field, value)) { return false; }
                field = value;
                var h = this.PropertyChanged;
                if (h != null) { h(this, new PropertyChangedEventArgs(propertyName)); }
                return true;
            }
        }
        #endregion

        private void MyButton01_Click(object sender, RoutedEventArgs e)
        {
            _user1.Name += "x";
        }

        private void MyButton02_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var user in _userList1)
                {
                    user.IsMarried = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void MyButton03_Click(object sender, RoutedEventArgs e)
        {
            foreach (var user in _userList1)
            {
                user.IsMarried = false;
            }
        }



    }
}
