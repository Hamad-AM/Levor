using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LevorMapEditor
{
    class MapView
    {
        private ImageDrawing[,] map;
        private int zoom;
        private Vector position;
        private Grid grid;

        public MapView(int width, int height)
        {
            map = new ImageDrawing[100, 100];
            zoom = 100;
            position = new Vector();
            position.x = 0;
            position.y = 0;

            StartGrid();
        }

        private void StartGrid()
        {
            grid = new Grid();
            grid.Width = 500;
            grid.Height = 500;
            grid.ShowGridLines = true;

            for (int i = 0; i < zoom - position.y; i++)
            {
                ColumnDefinition coldef = new ColumnDefinition();
                grid.ColumnDefinitions.Add(coldef);
            }

            for (int i = 0; i < zoom - position.x; i++)
            {
                RowDefinition rowdef = new RowDefinition();
                grid.RowDefinitions.Add(rowdef);
            }
            for (int i = 0; i < zoom; i++)
            {
                for (int j = 0; i < zoom; i++)
                {
                    map[i, j] = new ImageDrawing();
                    map[i, j].Rect = new System.Windows.Rect(0, 0, grid.Width / zoom, grid.Height / zoom);
                    map[i, j].ImageSource = new BitmapImage(new Uri(@"Resources/GridPlaceHolder.png", UriKind.Relative));
                }
            }
        }

        public Grid GenerateGrid()
        {
            return grid;
        }

        public Grid UpdateGrid()
        {
            return grid;
        }

        struct Vector
        {
            public int x;
            public int y;
        }
    }
}
