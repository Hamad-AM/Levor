using Microsoft.Windows.Themes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;
using System.Xml.Serialization;

namespace LevorMapEditor
{
    [Serializable]
    [XmlRoot(ElementName = "map")]
    public class TileMap
    {

        public string mapName;
        [XmlArray]
        public List<Tile> tileSet;
        [XmlArray]
        public List<Layer> layers;
        [XmlArray]
        public List<List<bool>> collisionMap;
        public int width;
        public int height;
    }

    [Serializable]
    [XmlRoot(ElementName = "layer")]
    public class Layer
    {
        public int id;
        public string name;
        public List<List<string>> data;
    }

    [Serializable]
    [XmlRoot(ElementName = "tile")]
    public class Tile
    {
        public string fileName;
        public int id;
    }
}
