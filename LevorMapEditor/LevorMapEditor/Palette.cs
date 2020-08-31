using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace LevorMapEditor
{
    class Palette
    {
        public static List<BitmapImage> palette = new List<BitmapImage>() { new BitmapImage(new Uri(@"Resources/GridPlaceHolder.png", UriKind.Relative)) };
        public static BitmapImage currentBrush = new BitmapImage(new Uri(@"Resources/GridPlaceHolder.png", UriKind.Relative));
        public static BitmapImage placeHolder = new BitmapImage(new Uri(@"Resources/GridPlaceHolder.png", UriKind.Relative));

        public static void loadFile(string fileName)
        {
            palette.Add(new BitmapImage(new Uri(fileName)));
        }
    }
}
