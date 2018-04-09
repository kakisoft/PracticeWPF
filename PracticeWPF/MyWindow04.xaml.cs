﻿using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PracticeWPF
{
    /// <summary>
    /// テキストボックスの双方向バインディング１
    /// </summary>
    public partial class MyWindow04 : Window
    {
        public MyWindow04()
        {
            InitializeComponent();
        }
    }


    public class Person : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void SetProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
        {
            field = value;
            var h = this.PropertyChanged;
            if (h != null) { h(this, new PropertyChangedEventArgs(propertyName)); }
        }

        private int age;

        public int Age
        {
            get { return this.age; }
            set { this.SetProperty(ref this.age, value); }
        }

        private string name;

        public string Name
        {
            get { return this.name; }
            set { this.SetProperty(ref this.name, value); }
        }
    }

}
