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
    /// 特殊記号
    /// -----------------------------------------------------------
    ///  @ - エスケープ文字を使わずに、「/」や改行が表現できる。
    /// -----------------------------------------------------------
    /// </summary>
    public partial class MyWindow35 : Window
    {
        #region 初期化
        public MyWindow35()
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
            myButton03.Click += (sender, e) => MyButton03_Click();
            myButton04.Click += (sender, e) => MyButton04_Click();
            myButton05.Click += (sender, e) => MyButton05_Click();
            myButton06.Click += (sender, e) => MyButton06_Click();
        }
        #endregion

        #region @ verbatim １
        //https://docs.microsoft.com/ja-jp/dotnet/csharp/language-reference/tokens/verbatim
        //正直、何言ってるのかさっぱり分からん。
        private void MyButton01_Click()
        {
            string[] @for = { "John", "James", "Joan", "Jamie" };
            for (int ctr = 0; ctr < @for.Length; ctr++)
            {
                Console.WriteLine($"Here is your gift, {@for[ctr]}!");
            }
        }
        #endregion

        #region @ verbatim ２
        //http://code-examples-ja.hateblo.jp/entry/2015/01/18/verbatim%E6%96%87%E5%AD%97%E5%88%97%E3%83%AA%E3%83%86%E3%83%A9%E3%83%AB%E3%81%AF%40%E3%81%8B%E3%82%89%E5%A7%8B%E3%81%BE%E3%82%8B%E3%80%82%E8%AA%AD%E3%81%BF%E6%96%B9%E3%81%AF%E3%80%8C%E3%83%90
        /// <summary>
        /// C#のすべての方は、値型、参照型、ポインタ型の３つのうち、どれかに分類される。
        /// 　・bool型
        /// 　　　論理値。trueまたはfalseを格納できる。
        /// 　　　
        /// 　・object型
        /// 　　　値型と参照型の両方の基礎となる基本型。
        /// 　　　
        /// 　・string型
        /// 　　　C#の文字列は普遍のUnicode文字シーケンスで、System.Stringクラスの別名を表している。
        ///
        /// 
        /// verbatim文字列リテラルは@で始まる。
        /// 複数行にわたっている場合や、\などのエスケープ文字を含む場合でも、文字列はverbatimとして扱われる。
        ///     
        /// </summary>
        private void MyButton02_Click()
        {
            string str1 = "C:\\xampp\\htdocs\\sample";
            string str2 = @"C:\xampp\htdocs\sample";

            Console.WriteLine(str1 == str2); //True

            string str3 = "1Line\r\n2Line";
            string str4 = @"1Line
2Line";       //True

            Console.WriteLine(str3 == str4);


        }
        #endregion

        #region @ verbatim ３
        private void MyButton03_Click()
        {
            string path1 = "\\windows\\system32\\drivers\\etc\\hosts";  //エスケープ文字を使って表現
            string path2 = @"\windows\system32\drivers\etc\hosts";      //verbatim文字列を使って表現

            Console.WriteLine(path1 == path2); // True
        }
        #endregion

        #region $  文字列補間
        private void MyButton04_Click()
        {
            string name = "Mark";
            var date = DateTime.Now;

            // Composite formatting:
            Console.WriteLine("Hello, {0}! Today is {1}, it's {2:HH:mm} now.", name, date.DayOfWeek, date);
            // String interpolation:
            Console.WriteLine($"Hello, {name}! Today is {date.DayOfWeek}, it's {date:HH:mm} now.");
        }
        #endregion

        #region IsNullOrWhiteSpace
        private void MyButton05_Click()
        {
            string s1 = "";
            if (String.IsNullOrWhiteSpace(s1))
            {
                Console.WriteLine("s1 is IsNullOrWhiteSpace");
            }
            else
            {
                Console.WriteLine("s1 is not IsNullOrWhiteSpace");
            }




            string s2 = "   "; //半角スペース
            if (String.IsNullOrWhiteSpace(s2))
            {
                Console.WriteLine("s2 is IsNullOrWhiteSpace");
            }
            else
            {
                Console.WriteLine("s2 is not IsNullOrWhiteSpace");
            }



            string s3 = "　　"; //全角スペース
            if (String.IsNullOrWhiteSpace(s3))
            {
                Console.WriteLine("s3 is IsNullOrWhiteSpace");
            }
            else
            {
                Console.WriteLine("s3 is not IsNullOrWhiteSpace");
            }




            string s4 = "  　  　  　"; //全角と半角が混ざったパターン
            if (String.IsNullOrWhiteSpace(s4))
            {
                Console.WriteLine("s4 is IsNullOrWhiteSpace");
            }
            else
            {
                Console.WriteLine("s4 is not IsNullOrWhiteSpace");
            }




            string s5 = null;
            if (String.IsNullOrWhiteSpace(s5))
            {
                Console.WriteLine("s5 is IsNullOrWhiteSpace");
            }
            else
            {
                Console.WriteLine("s5 is not IsNullOrWhiteSpace");
            }

        }
        #endregion


        #region double.NaN
        private void MyButton06_Click()
        {
            double a1 = 1;
            a1 = double.NaN;

            if (double.IsNaN(a1))
            {
                // 整数
            }
            else
            {
                // null, 0, 0D
            }
        }
        #endregion
    }
}
