using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LevorMapEditor
{
    class MapView
    {
        private Image[,] map;
        private int zoom;
        private Vector position;
        private Tool activeTool;
        //private Grid grid;

        public MapView(int width, int height)
        {
            map = new Image[50, 50];
            zoom = 50;
            position = new Vector();
            position.x = 0;
            position.y = 0;

            //StartGrid();
        }

        public void StartGrid(ref Grid grid)
        {
            grid.Width = 500;
            grid.Height = 500;
            //grid.ShowGridLines = true;

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
                for (int j = 0; j < zoom; j++)
                {
                    Button btn = new Button();

                    map[i, j] = new Image();
                    //map[i, j].Rect = new Rect(0, 0, grid.Width / zoom, grid.Height / zoom);
                    map[i, j].Source = new BitmapImage(new Uri(@"Resources/GridPlaceHolder.png", UriKind.Relative));

                    btn.Content = map[i, j];
                    btn.Click += new RoutedEventHandler(MapCellClicked);
                    btn.Style = new Style();

                    Grid.SetRow(btn, j);
                    Grid.SetColumn(btn, i);

                    grid.Children.Add(btn);
                }
            }
        }

        private void MapCellClicked(object sender, RoutedEventArgs e)
        {
            switch (activeTool)
            {
                case Tool.Brush:
                    Button cell = sender as Button;
                    int row = (int)cell.GetValue(Grid.RowProperty);
                    int column = (int)cell.GetValue(Grid.ColumnProperty);

                    map[column, row].Source = Palette.currentBrush.Source;
                    cell.Content = null;
                    cell.Content = map[column, row];
                case Erase:

            }

        }

        //public Grid GenerateGrid()
        //{
        //    return grid;
        //}

        //public Grid UpdateGrid()
        //{
        //    return grid;
        //}

        public void SetActiveTool(Tool newTool)
        {
            activeTool = newTool;
        }

        public void ClearMap()
        {

        }

        struct Vector
        {
            public int x;
            public int y;
        }
    }
}
