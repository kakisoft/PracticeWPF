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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticeWPF
{
    /// <summary>
    /// Page、NavigationService
    /// </summary>
    public partial class MyWindow34 : Window
    {
        public MyWindow34()
        {
            InitializeComponent();
            _navi = this.myFrame.NavigationService;
        }

        private NavigationService _navi;

        private List<Uri> _uriList = new List<Uri>() {
            new Uri("MyPages/MyPage01.xaml",UriKind.Relative),
            new Uri("MyPages/MyPage02.xaml",UriKind.Relative),
            new Uri("MyPages/MyPage03.xaml",UriKind.Relative),
        };
        private void myFrame_Loaded(object sender, RoutedEventArgs e)
        {
            _navi.Navigate(_uriList[0]);
        }
        private void prevButton_Click(object sender, RoutedEventArgs e)
        {
            if (_navi.CanGoBack)
                _navi.GoBack();
            else
            {
                int index = _uriList.FindIndex(p => p == _navi.CurrentSource) - 1;
                _navi.Navigate(_uriList[index]);
            }
        }
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_navi.CanGoForward)
                _navi.GoForward();
            else
            {
                int index = _uriList.FindIndex(p => p == _navi.CurrentSource) + 1;
                _navi.Navigate(_uriList[index]);
            }
        }

        private void myFrame_Navigated(object sender, NavigationEventArgs e)
        {
            int index = _uriList.IndexOf(_navi.CurrentSource);
            if (index <= 0)
                prevButton.IsEnabled = false;
            else
                prevButton.IsEnabled = true;
            if (index + 1 == _uriList.Count)
                nextButton.IsEnabled = false;
            else
                nextButton.IsEnabled = true;
        }
    }
}
