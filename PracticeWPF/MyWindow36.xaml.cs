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

        //パス指定
        //string targetDirectory = System.Environment.CurrentDirectory + @"\..\..\Resources\";
        //string targetSource = "Sample01.db";
        string ConnectionString = System.Environment.CurrentDirectory + @"\..\..\Resources\Sample01.db";


        public MyWindow36()
        {
            InitializeComponent();
        }


    }
}
