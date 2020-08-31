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
        Project project;
        List<BitmapImage[,]> imageMap;
        List<string[,]> tileIds;

        Tool activeTool;
        int currentLayer;
        bool[,] collisionMap;
        bool eraseModifier = false;
        int mapHeight;
        int mapWidth;

        public MainWindow()
        {
            InitializeComponent();

            MapViewbox.Width = 500;
            MapViewbox.Height = 500;

            project = new Project();

            //mapView = new MapView(25, 25);
            PaletteView.CreatePalette(ref PaletteViewPanel);
            //mapView.StartGrid(ref MapViewGrid);
            //WindowState = WindowState.Maximized;
        }

        public void MenuNewClick(object sender, RoutedEventArgs e)
        {
            /*
             * Open dialog to input name for map and dimensions
             */

            // Temporary width and height
            int width = 25;
            int height = 25;

            project.NewMap("UntitledMap", 25, 25);

            imageMap = new List<BitmapImage[,]>();
            tileIds = new List<string[,]>();

            ItemCollection items = LayersListBox.Items;
            for (int i = 0; i < items.Count; i++)
            {
                imageMap.Add(new BitmapImage[width, height]);
                tileIds.Add(new string[width, height]);
            }
            collisionMap = new bool[width, height];

            mapWidth = width;
            mapHeight = height;

            currentLayer = 0;

            StartGrid();
            InitializeMap();
        }

        private void InitializeMap()
        {
            foreach (string[,] layer in tileIds)
            {
                for (int i = 0; i < mapWidth; i++)
                {
                    for (int j = 0; j < mapHeight; j++)
                    {
                        layer[i, j] = "0";
                    }
                }
            }

            project.AddToTileSet(0, "");
        }

        public void MenuOpenClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "xml files (*.xml)|*.xml|PNG files (*.png)|*.png|All files (*.*)|*.*";
            openDlg.RestoreDirectory = true;

            if (openDlg.ShowDialog() == true)
            {
                string fileExtension = System.IO.Path.GetExtension(openDlg.FileName);
                if (fileExtension == ".xml")
                {
                    project.LoadMap(openDlg.FileName);
                }
                else if (fileExtension == ".png")
                {
                    Palette.loadFile(openDlg.FileName);
                    PaletteView.CreatePalette(ref PaletteViewPanel);
                    project.AddToTileSet(Palette.palette.Count - 1, openDlg.FileName);
                }
            }
            else
            {
                Debug.WriteLine("ERROR : Could not open file ");
            }
        }

        public void MenuSaveClick(object sender, RoutedEventArgs e)
        {
            project.WriteMap();
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
            Close();
        }

        public void BrushBtnClick(object sender, RoutedEventArgs e)
        {
            activeTool = Tool.Brush;
        }

        public void EraseBtnClick(object sender, RoutedEventArgs e)
        {
            eraseModifier = !eraseModifier;
        }

        public void FillBtnClick(object sender, RoutedEventArgs e)
        {
            activeTool = Tool.Fill;
        }

        public void CollisionBrushBtnClick(object sender, RoutedEventArgs e)
        {
            activeTool = Tool.Collision;
        }

        public void ClearBtnClick(object sender, RoutedEventArgs e)
        {
            ClearMap();
        }

        private void ClearMap()
        {

        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            UpdateViewBox((e.Delta > 0) ? 10 : -10);
        }

        private void UpdateViewBox(int newValue)
        {
            if ((MapViewbox.Height >= 0) && (MapViewbox.Width >= 0))
            {
                MapViewbox.Height += newValue;
                MapViewbox.Width += newValue;
            }
        }

        private void RemoveSelectedLayer(object sender, RoutedEventArgs e)
        {
            LayersListBox.Items.Remove(LayersListBox.SelectedItem);
        }

        private void AddNewLayer(object sender, RoutedEventArgs e)
        {
            LayersListBox.Items.Add(newLayerNameTB.Text);
        }

        private void LayerSelected(object sender, SelectionChangedEventArgs args)
        {
            currentLayer = LayersListBox.SelectedIndex;
            UpdateViewLayer();
        }

        private void UpdateViewLayer()
        {
            for (int i = 0; i < MapViewGrid.Children.Count; i++)
            {
                int col = (int)MapViewGrid.Children[i].GetValue(Grid.ColumnProperty);
                int row = (int)MapViewGrid.Children[i].GetValue(Grid.RowProperty);
                if (tileIds[currentLayer][col, row] != "0")
                {
                    ((ImageBrush)(MapViewGrid.Children[i] as Button).Background).ImageSource = imageMap[currentLayer][col, row];
                }
            }
        }

        private void UpdateViewCollision()
        {

        }

        private void StartGrid()
        {
            MapViewGrid.Width = 16;
            MapViewGrid.Height = 16;
            //grid.ShowGridLines = true;

            for (int i = 0; i < mapWidth; i++)
            {
                ColumnDefinition coldef = new ColumnDefinition();
                MapViewGrid.ColumnDefinitions.Add(coldef);
            }

            for (int i = 0; i < mapHeight; i++)
            {
                RowDefinition rowdef = new RowDefinition();
                MapViewGrid.RowDefinitions.Add(rowdef);
            }

            BitmapImage[,] tempImageMap = new BitmapImage[mapWidth, mapHeight];
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    Button btn = new Button();
                    //map[i, j].Rect = new Rect(0, 0, grid.Width / zoom, grid.Height / zoom);
                    tempImageMap[i, j] = new BitmapImage(new Uri(@"Resources/GridPlaceHolder.png", UriKind.Relative));
                    RenderOptions.SetBitmapScalingMode(tempImageMap[i, j], BitmapScalingMode.NearestNeighbor);
                    //RenderOptions.BitmapScalingModeProperty = "NearestNeighbor";

                    //btn.Content = imageMap[i, j];
                    ImageBrush img = new ImageBrush();
                    img.ImageSource = tempImageMap[i, j];


                    RenderOptions.SetBitmapScalingMode(img, BitmapScalingMode.NearestNeighbor);

                    btn.Background = img;

                    btn.Click += new RoutedEventHandler(MapCellClicked);
                    btn.BorderThickness = new Thickness();

                    Grid.SetRow(btn, j);
                    Grid.SetColumn(btn, i);

                    MapViewGrid.Children.Add(btn);

                    collisionMap[i, j] = false;

                    //TileMap.AddTile(new Tile() { });
                }
            }

            for (int i = 0; i < LayersListBox.Items.Count; i++)
            {
                imageMap[i] = tempImageMap;
            }
       
        }

        private void MapCellClicked(object sender, RoutedEventArgs e)
        {

            Button cell = sender as Button;
            int row = (int)cell.GetValue(Grid.RowProperty);
            int column = (int)cell.GetValue(Grid.ColumnProperty);
            
            // REFACTOR
            // Change to use two sub classes that have different implmentations for the brushes
            switch (activeTool)
            {
                case Tool.Brush:
                    if (eraseModifier == true)
                        imageMap[currentLayer][column, row] = Palette.palette[0];
                    else
                        imageMap[currentLayer][column, row] = Palette.currentBrush;
                    
                    // Set Cell to brushes image element
                    ImageBrush img = new ImageBrush();
                    img.ImageSource = imageMap[currentLayer][column, row];
                    img.Stretch = Stretch.UniformToFill;
                    RenderOptions.SetBitmapScalingMode(img, BitmapScalingMode.NearestNeighbor);
                    cell.Background = img;
                    
                    // Add brush id to map of tile ids
                    tileIds[currentLayer][column, row] = Palette.palette.IndexOf(Palette.currentBrush).ToString();

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
                        cell.BorderThickness = new Thickness(0.5);
                    }
                    UpdateViewCollision();

                    project.setMapColMap(collisionMap);
                    break;
            }

            project.setMapData(tileIds[currentLayer], currentLayer);
        }
    }
}
