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
    /// Prism
    /// 
    /// ツール→NuGetパッケージマネージャー→ソリューションのNuGetパッケージマネージャーの管理
    /// より「Prism.WPF」を検索し、インストール。
    /// 
    /// ⇒公式チュートリアルがやる気なさ過ぎて中断。
    /// https://docs.microsoft.com/en-us/previous-versions/msp-n-p/gg430857(v=pandp.40)
    /// 意味不明な中略すんなや。
    /// </summary>
    public partial class MyWindow30 : Window
    {
        public MyWindow30()
        {
            InitializeComponent();

            //this.DataContext = new CalcViewModel();
        }
    }
}
