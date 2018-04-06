using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private DataTable userList05 = new DataTable();


        // Enum だと、コンボボックス


        public class User
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public bool IsMarried { get; set; }
        }
        #endregion

        #region イニシャライズ
        public MyWindow15()
        {
            InitializeComponent();

            SetButtonEvent();

            //SetGridItemsSource01();  //Listだと、先にバインド設定するとダメっぽい。
            SetGridItemsSource02();

            InitializeDataset03();
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

    }
}
