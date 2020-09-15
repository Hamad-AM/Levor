using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataType;

namespace LevorMapEditor
{
    /// <summary>
    /// Interaction logic for NewMapDialog.xaml
    /// </summary>
    public partial class NewMapDialog : Window
    {
        public string InputMapName { get; private set; }
        public int InputMapWidth { get; private set; }
        public int InputMapHeight { get; private set; }

        public NewMapDialog()
        {
            InitializeComponent();
        }

        private void OnOkClick(object sender, RoutedEventArgs e)
        {
            InputMapName = mapName.Text;
            int inMapWidth;
            int inMapHeight;

            if (Int32.TryParse(mapWidth.Text, out inMapWidth))
            {
                InputMapWidth = inMapWidth;
            }
            else
            {
                Console.WriteLine("Invalid Input");
            }

            if (Int32.TryParse(mapHeight.Text, out inMapHeight))
            {
                InputMapHeight = inMapHeight;
            }
            else
            {
                Console.WriteLine("Invalid Input");
            }
            this.Close();
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
