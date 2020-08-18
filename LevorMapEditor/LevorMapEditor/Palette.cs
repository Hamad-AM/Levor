using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LevorMapEditor
{
    class Palette
    {
        public static List<BitmapImage> palette = new List<BitmapImage>();
        public static Image currentBrush = new Image();

        public static void loadFile(string fileName)
        {
            palette.Add(new BitmapImage(new Uri(fileName)));
        }
    }
}
