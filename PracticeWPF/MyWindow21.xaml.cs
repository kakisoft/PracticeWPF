using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// 全角・半角の変換、カレントディレクト、リソース取得
    /// </summary>
    public partial class MyWindow21 : Window
    {
        #region 変換テーブル
        // 変換テーブル（全角→半角）
        Dictionary<char, char> ConvertToHankaku = new Dictionary<char, char>() {
                {'１','1'},{'２','2'},{'３','3'},{'４','4'},{'５','5'},
                {'６','6'},{'７','7'},{'８','8'},{'９','9'},{'０','0'},
                {'Ａ','A'},{'Ｂ','B'},{'Ｃ','C'},{'Ｄ','D'},{'Ｅ','E'},
                {'Ｆ','F'},{'Ｇ','G'},{'Ｈ','H'},{'Ｉ','I'},{'Ｊ','J'},
                {'Ｋ','K'},{'Ｌ','L'},{'Ｍ','M'},{'Ｎ','N'},{'Ｏ','O'},
                {'Ｐ','P'},{'Ｑ','Q'},{'Ｒ','R'},{'Ｓ','S'},{'Ｔ','T'},
                {'Ｕ','U'},{'Ｖ','V'},{'Ｗ','W'},{'Ｘ','X'},{'Ｙ','Y'},
                {'Ｚ','Z'},
                {'ａ','a'},{'ｂ','b'},{'ｃ','c'},{'ｄ','d'},{'ｅ','e'},
                {'ｆ','f'},{'ｇ','g'},{'ｈ','h'},{'ｉ','i'},{'ｊ','j'},
                {'ｋ','k'},{'ｌ','l'},{'ｍ','m'},{'ｎ','n'},{'ｏ','o'},
                {'ｐ','p'},{'ｑ','q'},{'ｒ','r'},{'ｓ','s'},{'ｔ','t'},
                {'ｕ','u'},{'ｖ','v'},{'ｗ','w'},{'ｘ','x'},{'ｙ','y'},
                {'ｚ','z'},
                {'　',' '},
            };

        // 変換テーブル（半角→全角）
        Dictionary<char, char> ConvertToZenkaku = new Dictionary<char, char>() {
                {'1','１'},{'2','２'},{'3','３'},{'4','４'},{'5','５'},
                {'6','６'},{'7','７'},{'8','８'},{'9','９'},{'0','０'},
                {'A','Ａ'},{'B','Ｂ'},{'C','Ｃ'},{'D','Ｄ'},{'E','Ｅ'},
                {'F','Ｆ'},{'G','Ｇ'},{'H','Ｈ'},{'I','Ｉ'},{'J','Ｊ'},
                {'K','Ｋ'},{'L','Ｌ'},{'M','Ｍ'},{'N','Ｎ'},{'O','Ｏ'},
                {'P','Ｐ'},{'Q','Ｑ'},{'R','Ｒ'},{'S','Ｓ'},{'T','Ｔ'},
                {'U','Ｕ'},{'V','Ｖ'},{'W','Ｗ'},{'X','Ｘ'},{'Y','Ｙ'},
                {'Z','Ｚ'},
                {'a','ａ'},{'b','ｂ'},{'c','ｃ'},{'d','ｄ'},{'e','ｅ'},
                {'f','ｆ'},{'g','ｇ'},{'h','ｈ'},{'i','ｉ'},{'j','ｊ'},
                {'k','ｋ'},{'l','ｌ'},{'m','ｍ'},{'n','ｎ'},{'o','ｏ'},
                {'p','ｐ'},{'q','ｑ'},{'r','ｒ'},{'s','ｓ'},{'t','ｔ'},
                {'u','ｕ'},{'v','ｖ'},{'w','ｗ'},{'x','ｘ'},{'y','ｙ'},
                {'z','ｚ'},
                {' ','　'},
            };
        #endregion

        #region 初期化
        public MyWindow21()
        {
            InitializeComponent();
            AddMyEvent();
        }

        private void AddMyEvent()
        {
            myButton01.Click += (sender, e) => MyButton01_Click();
            myButton02.Click += (sender, e) => MyButton02_Click();
            myButton03.Click += (sender, e) => MyButton03_Click();
        }
        #endregion

        #region 全角⇔半角
        private void MyButton01_Click()
        {
            //===============================
            //     変換テーブルを使用
            //===============================

            //-----< 全角→半角 >-----
            string text01 =
                            "一方、日本で「ＶＡＩＯ　Ｐｈｏｎｅ」が正式に発表された際、" +
                            "パナソニックのスマホ「ＥＬＵＧＡ　Ｕ２」と「ロゴが違うだけ" +
                            "ではないか」、「ＥＬＵＧＡ　Ｕ２のコピーに近いにもかかわら" +
                            "ず、価格はＥＬＵＧＡ　Ｕ２より高い」などと批判の声があがっ" +
                            "たことを伝えた。";



            string s1 = new string(text01.Select(n => (ConvertToHankaku.ContainsKey(n) ? ConvertToHankaku[n] : n)).ToArray());
            Console.WriteLine(s1);


            //-----< 半角→全角 >-----
            string text02 =
                            "aあbいcうdえeおfg" +
                            "hかiきjくklけこmn" +
                            "opqさrstuそ";


            string s2 = new string(text02.Select(n => (ConvertToZenkaku.ContainsKey(n) ? ConvertToZenkaku[n] : n)).ToArray());
            Console.WriteLine(s2); // 出力結果を表示



            //===============================
            //       正規表現を使用
            //===============================
            int d1 = 20180621;
            string d2 = d1.ToString("0000/00/00"); // "2018/06/21"
            DateTime d3 = DateTime.Parse(d2);
            string d4 = d3.ToString("M月d日 (ddd)");
            string d5 = Regex.Replace(d4, "[0-9]", p => ((char)(p.Value[0] - '0' + '０')).ToString());

            Console.WriteLine(d5);

        }
        #endregion

        #region カレントディレクトリ所得・カレントディレクトリ変更
        private void MyButton02_Click()
        {
            //カレントディレクトリ取得
            string currentCurrentDirectory01 = System.IO.Directory.GetCurrentDirectory();  //（プロジェクトのルート）\PracticeWPF\bin\Debug
            string currentCurrentDirectory02 = System.Environment.CurrentDirectory;

            // カレントディレクトリを「C:\Hoge\」に設定する
            System.IO.Directory.SetCurrentDirectory(@"F:\Csharp\");
            // カレントディレクトリを「C:\Hoge\」に設定する
            System.Environment.CurrentDirectory = @"F:\Csharp\";


            //相対パス
            string currentCurrentDirectory03 = currentCurrentDirectory01 + "\\..\\" + "\\..\\"　+ "Resources\\";
            System.IO.Directory.SetCurrentDirectory(currentCurrentDirectory03);

            Console.WriteLine(System.Environment.CurrentDirectory);  // ../../Resources 　に移動している
        }
        #endregion

        #region リソース取得
        private void MyButton03_Click()
        {
            //=====================================
            //  リソースの設定
            //     プロジェクトのルート→プロパティ→[リソース]タブ
            //     →追加したいリソースをドラッグ＆ドロップ。
            //
            //
            //  リソース名の変更は、ソリューションエクスプローラからでなく、↑の画面でやった方が良さげ。
            //=====================================

            //リソース取得
            var xmlFile01 = Properties.Resources.myXMLFile01;

        }
        #endregion
    }
}
