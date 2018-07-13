using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticeWPF
{
    /// <summary>
    /// テキストボックスの双方向バインディング１
    /// </summary>
    public partial class MyWindow04 : Window
    {
        #region 初期化
        public MyWindow04()
        {
            InitializeComponent();

            AddMyEvent();
        }

        private void AddMyEvent()
        {
            SetUsersList();
            myButton01.Click += (sender, e) => MyButton01_Click();

        }
        #endregion



        #region ダウンキャスト
        //親クラス
        class Users
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Position { get; set; }
        }
        //子クラス
        class ExtendUsers : Users
        {
            public bool IsChecked { get; set; } //画面の制御用として、子だけで使いたいプロパティ

            public ExtendUsers(Users value) //コンストラクタの引数に、親のインスタンスを渡す
            {
                //---------------------
                //  親要素の全プロパティをリストアップし、子に同じ値を設定
                //---------------------
                PropertyInfo[] propertyInfoinfoArray = value.GetType().GetProperties();  //プロパティをリストアップ
                //foreach (PropertyInfo item in propertyInfoinfoArray) //リストアップしたプロパティをループで回す
                foreach (PropertyInfo item in propertyInfoinfoArray.Where(x => !(x.SetMethod is null)))  //リストアップしたプロパティをループで回す（Setterを実装したプロパティのみを対象とする。）
                {
                    var property = value.GetType().GetProperty(item.Name);  //プロパティを取得
                    property.SetValue(this, item.GetValue(value));          //子に親と同じ値をセット
                }
            }
        }

        //Model定義されたクラスのリスト（ここに書いてると、そう見えないけど、そういう事にしといて下さい。）
        private List<Users> _users;
        //Model定義されたクラスを拡張した要素を詰め込んだリスト
        private List<ExtendUsers> _extendUsers;

        private void MyButton01_Click()
        {
            _extendUsers = new List<ExtendUsers>();
            foreach (var item in _users) //Model定義された要素が入ったリストをループ回す
            {
                ExtendUsers el = new ExtendUsers(item); //子のインスタンス作成時、親のインスタンスを引数に渡す。（詳細は上記を参照）
                _extendUsers.Add(el); //親のインスタンスと同じ値を設定した子を、リストに追加
            }
        }

        //============================
        // サンプルソースを動かすために用意した。本当はDBから引っ張ってくる
        //============================
        private void SetUsersList()
        {
            _users = new List<Users>();
            _users.Add(new Users { Id = 1, Name = "Tanaka", Position = 1 });
            _users.Add(new Users { Id = 2, Name = "Yamada", Position = 1 });
            _users.Add(new Users { Id = 3, Name = "Watanabe", Position = 2 });
        }
        #endregion

    }

    #region 双方向バインド用のクラス定義
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
    #endregion


}
