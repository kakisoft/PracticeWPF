using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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
    /// SQLite １
    /// 
    /// =================================================
    /// NuGetで「SQLite」
    /// 　・System.Data.SQLite
    /// 　　　　全部入り。
    /// 　　　　Entity Frameworkや、SQLiteのLINQ拡張などが全部入ったセット。  
    /// 　　　　実行ファイルのディレクトリにファイルがいっぱい増える。   
    /// 
    /// 　・System.Data.SQLite.Core
    /// 　　　　コンパクトでGoodらしい。
    /// =================================================
    /// 
    /// ここでは、System.Data.SQLite.Core を扱ってます。
    /// -----------------------------------
    /// 【 NuGet 】
    /// ツール→NuGetパッケージマネージャー→パッケージマネージャーコンソール
    /// 
    /// Install-Package System.Data.SQLite.Core
    /// -----------------------------------
    /// ＜参考サイト＞
    /// http://dotnetcsharptips.seesaa.net/category/26325844-1.html
    /// </summary>
    public partial class MyWindow36 : Window
    {
        //パスを指定しない場合、bin/debug または bin/Release 
        //private const string ConnectionString = @"Data Source=Sample.db";

        //-----< パス指定 >-----
        //string targetDirectory = System.Environment.CurrentDirectory + @"\..\..\Resources\";
        //string targetSource = "Sample01.db";
        string ConnectionString = @"Data Source=" + System.Environment.CurrentDirectory + @"\..\..\Resources\Sample01.db";


        #region 初期化
        public MyWindow36()
        {
            InitializeComponent();

            InitializeThisWindowsParameters();
            AddMyEvent();
        }
        private void InitializeThisWindowsParameters()
        {

        }

        private void AddMyEvent()
        {
            buttonCreate.Click += (sender, e) => buttonCreate_Click();
            buttonInsert.Click += (sender, e) => buttonInsert_Click();
            buttonSelect.Click += (sender, e) => buttonSelect_Click();
            buttonSelect2.Click += (sender, e) => buttonSelect2_Click();
        }
        #endregion

        #region テーブルの作成
        private void buttonCreate_Click()
        {
            using (var con = new SQLiteConnection(ConnectionString))
            {
                con.Open();

                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE T_MEMO(ID INTEGER PRIMARY KEY AUTOINCREMENT,MEMO TEXT) ";

                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region データの挿入
        private void buttonInsert_Click()
        {
            using (var con = new SQLiteConnection(ConnectionString))
            {
                con.Open();

                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO T_MEMO (MEMO) VALUES (@p_memo)";
                    cmd.Parameters.Add(new SQLiteParameter("@p_memo", DateTime.Now.ToLongTimeString()));

                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region テーブルの作成
        private void buttonSelect_Click()
        {
            var list = new List<string>();

            using (var con = new SQLiteConnection(ConnectionString))
            {
                con.Open();

                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT MEMO FROM T_MEMO";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(reader.GetString(0));
                        }
                    }
                }
            }

            MessageBox.Show(string.Join(",", list));
        }
        #endregion

        #region 非接続型の読み出し
        private void buttonSelect2_Click()
        {
            var adapter = new SQLiteDataAdapter("SELECT MEMO FROM T_MEMO", ConnectionString);

            var ds = new DataSet();

            adapter.Fill(ds, "T_MEMO");

            MessageBox.Show(ds.Tables[0].Rows.Count.ToString());
        }
        #endregion

    }
}
