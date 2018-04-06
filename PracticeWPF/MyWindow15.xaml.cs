using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
    /// DataGrid
    /// </summary>
    public partial class MyWindow15 : Window
    {
        #region クラス定義
        private List<User> userList01 = new List<User>();
        private ObservableCollection<User> userList02 = new ObservableCollection<User>();
        private DataTable userList03_1 = new DataTable();
        private DataTable userList03_2 = new DataTable();
        private List<Person01> personList04 = new List<Person01>();
        private List<Person02> personList05 = new List<Person02>();


        // Enum だと、コンボボックス


        public class User
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public bool IsMarried { get; set; }
        }
        #endregion

        #region クラス定義２
        public class Person01
        {
            public int No { get; set; }
            public string Name { get; set; }
            public DateTime BirthDay { get; set; }
        }


        //-----< 双方向バインディングを実現するパターン >-----
        public class Person02
        {
            //publicなsetterを作らず、コンストラクタで受け取る
            public int No { get; private set; }
            public string Name { get; private set; }
            public DateTime BirthDay { get; private set; }

            public Person02(int no, string name, DateTime birthDay)
            {
                this.No = no;
                this.Name = name;
                this.BirthDay = birthDay;
            }
        }

        //public class Person
        //{
        //    public int No { get; set; }
        //    public string Name { get; set; }
        //    public DateTime BirthDay { get; set; }
        //}

        //public event PropertyChangedEventHandler PropertyChanged;

        //private Person[] _persons;

        //public Person[] Persons
        //{
        //    get
        //    {
        //        return _persons;
        //    }
        //    private set
        //    {
        //        _persons = value;

        //        //更新通知
        //        var h = PropertyChanged;
        //        if (h != null) h(this, new PropertyChangedEventArgs("Persons"));
        //    }
        //}
        #endregion


        #region イニシャライズ
        public MyWindow15()
        {
            InitializeComponent();

            SetButtonEvent();

            //SetGridItemsSource01();  //Listだと、先にバインド設定するとダメっぽい。
            SetGridItemsSource02();

            InitializeDataset03();
            InitializeDataset04();
            InitializeDataset05();
        }
        #endregion

        #region イベント設定
        private void SetButtonEvent()
        {
            addButton01.Click   += (sender, e) => AddGridData01();
            clearButton01.Click += (sender, e) => ClearGridData01();
            editButton01.Click  += (sender, e) => EditGridData01();

            addButton02.Click   += (sender, e) => AddGridData02();
            clearButton02.Click += (sender, e) => ClearGridData02();
            editButton02.Click  += (sender, e) => EditGridData02();

            addButton03.Click   += (sender, e) => AddGridData03();
            clearButton03.Click += (sender, e) => ClearGridData03();
            editButton03.Click  += (sender, e) => EditGridData03();

            addButton04.Click   += (sender, e) => AddGridData04();
            clearButton04.Click += (sender, e) => ClearGridData04();
            editButton04.Click  += (sender, e) => EditGridData04();

            addButton05.Click   += (sender, e) => AddGridData05();
            clearButton05.Click += (sender, e) => ClearGridData05();
            editButton05.Click  += (sender, e) => EditGridData05();
        }
        #endregion

        #region グリッド制御１
        private void AddGridData01()
        {
            SetUserList01();
            SetGridItemsSource01();  //Listだと、先にバインド設定するとダメっぽいんで、データをセットした後にバインド。
        }

        private void ClearGridData01()
        {
            myDataGrid01.ItemsSource = null;
        }
        private void EditGridData01()
        {
            userList01[0].Name = "a"; //画面上は変更なし

            //myDataGrid01.ItemsSource = null;
            //myDataGrid01.ItemsSource = userList1;
        }

        private void SetUserList01()
        {
            userList01.Add(new User { Name = "tanaka", Age = 22, IsMarried = false, });
            userList01.Add(new User { Name = "sawada", Age = 35, IsMarried = true, });
            userList01.Add(new User { Name = "saitou", Age = 41, IsMarried = false, });
            userList01.Add(new User { Name = "harada", Age = 22, IsMarried = false, });
            userList01.Add(new User { Name = "kitano", Age = 26, IsMarried = false, });
        }

        private void SetGridItemsSource01()
        {
            myDataGrid01.ItemsSource = userList01;
        }
        #endregion

        #region グリッド制御２
        private void AddGridData02()
        {
            SetUserList02();
        }

        private void ClearGridData02()
        {
            userList02.Clear();
        }
        private void EditGridData02()
        {
            userList02[0].Name = "a"; //画面上は変更なし

            myDataGrid02.DataContext = userList02;

            //myDataGrid02.ItemsSource = null;
            //myDataGrid02.ItemsSource = userList02;
        }

        private void SetUserList02()
        {
            userList02.Add(new User { Name = "tanaka", Age = 22, IsMarried = false, });
            //userList02.Add(new User { Name = "sawada", Age = 35, IsMarried = true,  });
            //userList02.Add(new User { Name = "saitou", Age = 41, IsMarried = false, });
            //userList02.Add(new User { Name = "harada", Age = 22, IsMarried = false, });
            //userList02.Add(new User { Name = "kitano", Age = 26, IsMarried = false, });
        }

        private void SetGridItemsSource02()
        {
            myDataGrid02.DataContext = userList02;

//            myDataGrid02.DataContext = new DataTable();
        }
        #endregion

        #region グリッド制御３
        private void AddGridData03()
        {
            SetUserList03();
        }

        private void ClearGridData03()
        {
            userList03_1.Clear();
        }
        private void EditGridData03()
        {
            //userList03[0].Name = "a"; 

            //myDataGrid03.DataContext = userList03_1;

        }

        private void SetUserList03()
        {
            

        }

        private void SetGridItemsSource03()
        {
            //  myDataGrid03_1.DataContext = userList03_1;
            //myDataGrid03_2.DataContext = userList03_2;

            //myDataGrid03_1.ItemsSource = userList03_1;
            //myDataGrid03_1.ItemsSource = userList03_2;
        }

        private void InitializeDataset03()
        {
            //==========< 3-1 >==========
            userList03_1.Columns.Add(new DataColumn("name"));
            userList03_1.Columns.Add(new DataColumn("ruby"));
            userList03_1.Columns.Add(new DataColumn("sex"));
            userList03_1.Columns.Add(new DataColumn("age"));
            userList03_1.Columns.Add(new DataColumn("birthday"));
            userList03_1.Columns.Add(new DataColumn("married"));
            userList03_1.Columns.Add(new DataColumn("prefecture"));

            //---------------
            DataRow row = userList03_1.NewRow();
            row["name"] = "三村 瑠璃亜";
            row["ruby"] = "みむら るりあ";
            row["sex"] = "女";
            row["age"] = "32";
            row["birthday"] = "1982/7/16";
            row["married"] = "既婚";
            row["prefecture"] = "茨城県";
            userList03_1.Rows.Add(row);


            //==========< 3-2 >==========
            string[] columnList = { "name", "ruby", "sex", "age", "birthday", "married", "prefecture" };
            userList03_2.Columns.AddRange(columnList.Select(n => new DataColumn(n)).ToArray());

            //---------------
            userList03_2.Rows.Add("三村 瑠璃亜", "みむら るりあ", "女", "32", "1982/7/16", "既婚", "茨城県");
            userList03_2.Rows.Add("山崎 隆", "やまざき たかし", "男", "32", "1982/10/3", "未婚", "東京都");
            userList03_2.Rows.Add("滝 慢太郎", "たき まんたろう", "男", "31", "1983/6/30", "未婚", "福岡県");
            userList03_2.Rows.Add("石塚 浩正", "いしづか ひろま", "男", "29", "1985/3/1", "既婚", "熊本県");
            userList03_2.Rows.Add("中西 光", "なかにし ひかる", "男", "55", "1959/2/22", "既婚", "福島県");


            SetGridItemsSource03();
        }
        #endregion

        #region グリッド制御４
        private void InitializeDataset04()
        {

        }

        private void AddGridData04()
        {
            SetUserList04();
            //myDataGrid04.DataContext = person04;
            myDataGrid04.ItemsSource = personList04;
        }

        private void ClearGridData04()
        {
            //userList04.Clear();
        }
        private void EditGridData04()
        {
            //userList04[0].Name = "a"; //画面上は変更なし
            //myDataGrid02.DataContext = userList04;

        }

        private void SetUserList04()
        {
            personList04.Add(new Person01 { No = 1, Name = "Tanaka", BirthDay = new DateTime(2000, 1, 1) } );
            personList04.Add(new Person01 { No = 2, Name = "Yamada", BirthDay = new DateTime(1990, 5, 5) });
            personList04.Add(new Person01 { No = 3, Name = "Sato", BirthDay = new DateTime(2001, 12, 31) });
        }

        private void SetGridItemsSource04()
        {
            //myDataGrid04.DataContext = userList04;
        }


        #endregion

        #region グリッド制御５
        private void InitializeDataset05()
        {
            personList05.Add(new Person02(1, "Tanaka", new DateTime(2000, 1, 1)));
            personList05.Add(new Person02(2, "Yamada", new DateTime(1990, 5, 5)));
            personList05.Add(new Person02(3, "Sato", new DateTime(2001, 12, 31)));

            UpdateDispList();
        }

        private void AddGridData05()
        {
            personList05.Add(new Person02(4, "Yoshida", new DateTime(2002, 2, 2)));
            UpdateDispList();
        }

        private void ClearGridData05()
        {
            personList05.Clear();
        }
        private void EditGridData05()
        {
            var person02 = myDataGrid05.SelectedItem as Person02;
            if (person02 == null) return;

            var idx = personList05.IndexOf(person02);

            //名前だけ変えた新しいインスタンスを作って入れ替える
            var newPerson02 = new Person02(person02.No, "hayashi", person02.BirthDay);
            personList05[idx] = newPerson02;

            UpdateDispList();

            //選択行を再現
            myDataGrid05.SelectedIndex = idx;
        }


        //表示用リストを再設定
        private void UpdateDispList()
        {
            //this.dataGrid.ItemsSource = new ReadOnlyCollection<Person>(person05);
            myDataGrid05.ItemsSource = new List<Person02>(personList05);
        }
        #endregion

    }
}
