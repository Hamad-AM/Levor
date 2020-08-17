using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.IO;

namespace LevorMapEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MapView mapView;

        public MainWindow()
        {
            InitializeComponent();

            //WindowState = WindowState.Maximized;
        }

        public void MenuNewClick(object sender, RoutedEventArgs e)
        {
            mapView = new MapView(100, 100);
            MapViewGrid = mapView.GenerateGrid();
        }

        public void MenuOpenClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "xml files (*.xml)|*.xml|PNG files (*.png)|*.png|All files (*.*)|*.*";
            openDlg.RestoreDirectory = true;

            if (openDlg.ShowDialog() == true)
            {
                string fileExtension = System.IO.Path.GetExtension(openDlg.FileName);
                if (fileExtension == "xml")
                {
                    XMLProcessor.loadFile(openDlg.FileName);
                }
                else if (fileExtension == "png")
                {
                    Palette.loadFile(openDlg.FileName);
                }
            }
            else
            {
                Debug.WriteLine("ERROR : Could not open file ");
            }

        }

        public void MenuSaveClick(object sender, RoutedEventArgs e)
        {

        }

        public void MenuSaveAsClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "xml files (*.xml)|*.xml";
            saveDlg.RestoreDirectory = true;
            saveDlg.ShowDialog();
        }

        public void MenuQuitClick(object sender, RoutedEventArgs e)
        {
            
        }

        public void BrushBtnClick(object sender, RoutedEventArgs e)
        {
            
        }

        public void EraseBtnClick(object sender, RoutedEventArgs e)
        {
            
        }

        public void FillBtnClick(object sender, RoutedEventArgs e)
        {
            
        }

        public void ClearBtnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void MapScrollHorz_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
