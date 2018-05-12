using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Xps;

namespace PracticeWPF
{
    /// <summary>
    /// ファイルエクスプローラ
    /// https://www.ipentec.com/document/csharp-shell-namespace-create-explorer-tree-view-control-and-linked-list-view
    /// https://www.doraxdora.com/blog/2018/02/14/post-3933/
    /// 
    /// 
    /// 印刷
    /// 　参照の追加：System.Printing、ReachFramework
    /// 　（ソリューションエクスプローラの「参照」にて、右クリック→参照の追加）
    /// 
    /// </summary>
    public partial class MyWindow32 : Window
    {
        #region 初期化
        public MyWindow32()
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
        }
        #endregion

        #region －１．ファイルエクスプローラ－
        private void MyButton01_Click()
        {
            System.Diagnostics.Process.Start("EXPLORER.EXE", @"C:\My Documents\My Pictures");
        }
        #endregion

        #region －２．OpenFileDialog －
        private void MyButton02_Click()
        {
            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();

            //はじめのファイル名を指定する
            //はじめに「ファイル名」で表示される文字列を指定する
            ofd.FileName = "default.html";
            //はじめに表示されるフォルダを指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            ofd.InitialDirectory = @"C:\";
            //[ファイルの種類]に表示される選択肢を指定する
            //指定しないとすべてのファイルが表示される
            ofd.Filter = "HTMLファイル(*.html;*.htm)|*.html;*.htm|すべてのファイル(*.*)|*.*";
            //[ファイルの種類]ではじめに選択されるものを指定する
            //2番目の「すべてのファイル」が選択されているようにする
            ofd.FilterIndex = 2;
            //タイトルを設定する
            ofd.Title = "開くファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            ofd.RestoreDirectory = true;
            //存在しないファイルの名前が指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckFileExists = true;
            //存在しないパスが指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckPathExists = true;

            ////ダイアログを表示する
            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
            //    //OKボタンがクリックされたとき、選択されたファイル名を表示する
            //    Console.WriteLine(ofd.FileName);
            //}


            //ダイアログを表示する
            string selectedFileName = String.Empty;
            if (ofd.ShowDialog().ToString().Equals("OK"))
            {
                myTextBox01.Text = ofd.FileName;
            }
            //this.DialogResult = true;

        }
        #endregion

        #region －３－　XpsDocumentWriterを使った印刷
        //https://qiita.com/tricogimmick/items/133e9b6bcbcaa6c07d63
        private void MyButton03_Click()
        {
            // 1.各種オブジェクトの生成
            LocalPrintServer lps = new LocalPrintServer();
            PrintQueue queue = lps.DefaultPrintQueue;
            XpsDocumentWriter writer = PrintQueue.CreateXpsDocumentWriter(queue);

            // 2. 用紙サイズの設定
            PrintTicket ticket = queue.DefaultPrintTicket;
            ticket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4);
            ticket.PageOrientation = PageOrientation.Portrait;

            // 3. FixedPage の生成
            FixedPage page = new FixedPage();

            // 4. 印字データの作成
            Canvas canvas = new Canvas();
            TextBlock tb = new TextBlock();
            tb.Text = "TEST";
            tb.FontSize = 24;
            Canvas.SetTop(tb, 100);
            Canvas.SetLeft(tb, 100);
            canvas.Children.Add(tb);
            page.Children.Add(canvas);

            // 5. 印刷の実行
            writer.Write(page, ticket);
        }
        #endregion

        #region －４－　FixedDocumenを使った印刷
        //http://shen7113.blog.fc2.com/blog-entry-50.html
        private void MyButton04_Click()
        {
            // 印刷ダイアログを作成。
            var dPrt = new System.Windows.Controls.PrintDialog();

            // 印刷ダイアログを表示して、プリンタ選択と印刷設定を行う。
            if (dPrt.ShowDialog() == true)
            {
                // ここから印刷を実行する。

                // 印刷可能領域を取得する。
                var area = dPrt.PrintQueue.GetPrintCapabilities().PageImageableArea;

                // 上と左の余白を含めた印刷可能領域の大きさのCanvasを作る。
                var canv = new Canvas();
                canv.Width = area.OriginWidth + area.ExtentWidth;
                canv.Height = area.OriginHeight + area.ExtentHeight;

                // ここでCanvasに描画する。
                TextBlock tb = new TextBlock();
                tb.Text = "sample02";
                tb.FontSize = 24;
                Canvas.SetTop(tb, 100);
                Canvas.SetLeft(tb, 100);
                canv.Children.Add(tb);

                /* ここで単ページのVisualを直接印刷する場合、PrintVisual()と言うメソッドもある。
                dPrt.PrintVisual(canv, "PrintTest1");
                */

                // FixedPageを作って印刷対象（ここではCanvas）を設定する。
                var page = new FixedPage();
                page.Children.Add(canv);

                // PageContentを作ってFixedPageを設定する。
                var cont = new PageContent();
                cont.Child = page;

                // FixedDocumentを作ってPageContentを設定する。
                var doc = new FixedDocument();
                doc.Pages.Add(cont);

                // 印刷する。
                dPrt.PrintDocument(doc.DocumentPaginator, "Print1");
            }
        }
        #endregion
    }
}
