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
        private BitmapImage[,] imageMap;
        private bool[,] collisionMap;
        private Tool activeTool;
        private bool eraseModifier = false;
        private int mapWidth;
        private int mapHeight;
        private int currentLayer;


        public MapView(int width, int height)
        {
            imageMap = new BitmapImage[width, height];
            collisionMap = new bool[width, height];

            mapWidth = width;
            mapHeight = height;

            currentLayer = 0;

            //StartGrid();
        }

        public void StartGrid(ref Grid grid)
        {
            grid.Width = 16;
            grid.Height = 16;
            //grid.ShowGridLines = true;

            for (int i = 0; i <  mapWidth; i++)
            {
                ColumnDefinition coldef = new ColumnDefinition();
                grid.ColumnDefinitions.Add(coldef);
            }

            for (int i = 0; i < mapHeight; i++)
            {
                RowDefinition rowdef = new RowDefinition();
                grid.RowDefinitions.Add(rowdef);
            }
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    Button btn = new Button();
                    //map[i, j].Rect = new Rect(0, 0, grid.Width / zoom, grid.Height / zoom);
                    imageMap[i, j] = new BitmapImage(new Uri(@"Resources/GridPlaceHolder.png", UriKind.Relative));
                    RenderOptions.SetBitmapScalingMode(imageMap[i, j], BitmapScalingMode.NearestNeighbor);
                    //RenderOptions.BitmapScalingModeProperty = "NearestNeighbor";

                    //btn.Content = imageMap[i, j];
                    ImageBrush img = new ImageBrush();
                    img.ImageSource = imageMap[i, j];


                    RenderOptions.SetBitmapScalingMode(img, BitmapScalingMode.NearestNeighbor);

                    btn.Background = img;

                    btn.Click += new RoutedEventHandler(MapCellClicked);
                    btn.BorderThickness = new Thickness();

                    Grid.SetRow(btn, j);
                    Grid.SetColumn(btn, i);

                    grid.Children.Add(btn);

                    collisionMap[i, j] = false;

                    //TileMap.AddTile(new Tile() { });
                }
            }
        }

        private void MapCellClicked(object sender, RoutedEventArgs e)
        {

            Button cell = sender as Button;
            int row = (int)cell.GetValue(Grid.RowProperty);
            int column = (int)cell.GetValue(Grid.ColumnProperty);

            switch (activeTool)
            {
                case Tool.Brush:
                    if (eraseModifier == true)
                        imageMap[column, row] = Palette.palette[0];
                    else
                        imageMap[column, row] = Palette.currentBrush;
                    ImageBrush img = new ImageBrush();
                    img.ImageSource = imageMap[column, row];
                    img.Stretch = Stretch.UniformToFill;
                    RenderOptions.SetBitmapScalingMode(img, BitmapScalingMode.NearestNeighbor);
                    cell.Background = img;
                    break;

                case Tool.Collision:
                    if (eraseModifier == true)
                    {
                        collisionMap[column, row] = false;
                        cell.BorderThickness = new Thickness();
                    }
                    else
                    {
                        collisionMap[column, row] = true;
                        cell.BorderThickness = new Thickness(2);
                    }
                    break;
            }
        }

        public void ScrollHorz_ValueChanged(RoutedPropertyChangedEventArgs<double> e)
        {

        }

        public void ScrollVert_ValueChanged(RoutedPropertyChangedEventArgs<double> e)
        {

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

        public void SelectErase()
        {
            eraseModifier = !eraseModifier;
        }

        public void ClearMap()
        {

        }

        public void ChangeLayer(int newLayer)
        {
            currentLayer = newLayer;
        }
    }

    struct EVector
    {
        public int x;
        public int y;

        public EVector(int inX, int inY)
        {
            x = inX;
            y = inY;
        }
    }
}
