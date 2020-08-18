using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

                Image img = new Image();
                img.Source = Palette.palette[i];

                pltPicker.Content = img;
                pltPicker.Click += new RoutedEventHandler(ChooseTile);
                pltPicker.Width = 50;
                pltPicker.Height = 50;

                panel.Children.Add(pltPicker);
            }
        }

        private static void ChooseTile(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Image img = (Image)btn.Content;
            Palette.currentBrush.Source = img.Source;
        }

        public static void AddToPalette(ref WrapPanel panel)
        {

        }
    }
}
