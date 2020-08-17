using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace LevorMapEditor
{
    class Palette
    {
        private static List<BitmapImage> palette = new List<BitmapImage>();

        public static void loadFile(string fileName)
        {
            palette.Add(new BitmapImage(new Uri(fileName)));
        }
    }
}
