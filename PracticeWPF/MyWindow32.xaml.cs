using System;
using System.Collections.Generic;
using System.Linq;
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

namespace PracticeWPF
{
    /// <summary>
    /// ファイルエクスプローラ関連
    /// https://www.ipentec.com/document/csharp-shell-namespace-create-explorer-tree-view-control-and-linked-list-view
    /// https://www.doraxdora.com/blog/2018/02/14/post-3933/
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
        }
        #endregion

        #region －１．ファイルエクスプローラ－
        private void MyButton01_Click()
        {
            System.Diagnostics.Process.Start("EXPLORER.EXE", @"C:\My Documents\My Pictures");
        }
        #endregion

        #region －２．－
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

    }
}
