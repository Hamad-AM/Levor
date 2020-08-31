using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LevorMapEditor
{
    class PaletteView
    {
        public static void CreatePalette(ref WrapPanel panel)
        {
            panel.Children.Clear();

            for (int i = 0; i < Palette.palette.Count; i ++)
            {
                Button pltPicker = new Button();

                ImageBrush img = new ImageBrush();
                img.ImageSource = Palette.palette[i];

                RenderOptions.SetBitmapScalingMode(img.ImageSource, BitmapScalingMode.NearestNeighbor);

                pltPicker.Background = img;
                pltPicker.Click += new RoutedEventHandler(ChooseTile);
                pltPicker.Width = 50;
                pltPicker.Height = 50;

                panel.Children.Add(pltPicker);
            }
        }

        private static void ChooseTile(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ImageBrush img = (ImageBrush)btn.Background;
            Palette.currentBrush = (BitmapImage)img.ImageSource;
        }

        public static void AddToPalette(ref WrapPanel panel)
        {

        }
    }
}
