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
    /// Dictionaryをソースにした GridやListView
    /// </summary>
    public partial class MyWindow33 : Window
    {
        #region 定義情報
        //Dictionary hogeTable = new Dictionary<string, string>();

        // newするときに一緒に内容を指定する
        //Dictionary hogeTable_2 = new Dictionary<string, string>()
        //{
        //    { "one", "abc" },
        //    { "tow", "def" },
        //    { "three", "hij" }
        //}
        #endregion

        #region イニシャライズ
        public MyWindow33()
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
            myButton01.Click += (sender, e) => MyButton01_Click();
            myButton02.Click += (sender, e) => MyButton02_Click();
        }
        #endregion

        #region 辞書→Grid、List
        private void MyButton01_Click()
        {
            var myDictionary01 = new Dictionary<string, string>()
            {
                { "one"  , "abc" },
                { "tow"  , "def" },
                { "three", "hij" },
            };


            // 値の追加(1)：既にキーがあったら上書き(例外発生しない)
            myDictionary01["aaa"] = "10aaa";

            // 値の追加(2)：既にキーが存在すると ArgumentException が発生
            myDictionary01.Add("bbb", "10bbb");


            // キーと値の削除
            // キーを指定してテーブル内からキーと値を削除。
            // 戻り値は削除出来たらtrue、存在せず削除できない場合 false 
            // 存在しないキーを指定しても例外は出ない
            bool removed = myDictionary01.Remove("aaa");


            // 内容を全部削除
            //myDictionary01.Clear();


            // ◆存在確認
            // 戻り値が true の場合、テーブル内に存在する、false の場合存在しない
            bool contains = myDictionary01.ContainsKey("aaa");

            // データの個数を取得
            int count = myDictionary01.Count;



            foreach (KeyValuePair<string, string> item in myDictionary01)
            {
                // KeyValurPair<TKey, TValue> という型となるので　.Key と .Valur を使用してキーと値にアクセスする
                Console.WriteLine($"{item.Key} {item.Value}, "); // 0 None, 1 One, 2 Tow, と表示される
            }

            myDataGrid01.ItemsSource = myDictionary01;
            myListView01.ItemsSource = myDictionary01;
        }
        #endregion

        #region 辞書→Grid、List
        private void MyButton02_Click()
        {
            List<string> myStringList01 = new List<string>();

            myStringList01.Add("北海道");
            myStringList01.Add("東北");
            myStringList01.Add("関東");


            myDataGrid01.ItemsSource = myStringList01;
            myListView01.ItemsSource = myStringList01;
        }
        #endregion
    }
}
