using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using ytSubscriber.ViewModels;
using System.Collections;
using ytSubscriber.Models;

namespace ytSubscriber
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel MainViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            MainViewModel = DataContext as MainViewModel;
            FileNameTextBlock.Text = "No file loaded";
        }

        private void OpenFileDialog_OnClick(object sender, RoutedEventArgs e)
        {
            YoutubeApi api = new YoutubeApi();
            api.OAuthConnect();

//            var dlg = new OpenFileDialog();
//            var selected = dlg.ShowDialog();
//            if (selected == true)
//            {
//                MainViewModel.FileName = dlg.FileName;
//                FileNameTextBlock.Text = dlg.FileName;
//            }
//            else
//            {
//                FileNameTextBlock.Text = "No file loaded";
//            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void UploadersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var uploaderListBox = sender as ListBox;
            if (uploaderListBox != null)
            {
                var selectedUploaders = uploaderListBox.SelectedItems as IList;
                MainViewModel.FilterSubscriptionList(selectedUploaders);
            }
        }
    }
}
