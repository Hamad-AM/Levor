using Microsoft.Windows.Themes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;

namespace LevorMapEditor
{
    struct TileMap
    {

        public string mapName;
        public List<Tile> tileSet;
        public List<Layer> layers;
        public bool[,] collisionMap;

        public int width;
        public int height;
    }


    struct Layer
    {
        public int id;
        public string name;
        public string[,] data;
    }

    struct Tile
    {
        public string fileName;
        public int id;
    }
}
