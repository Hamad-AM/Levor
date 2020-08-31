using Accessibility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace LevorMapEditor
{
    class Project
    {
        private string mapFilePath = @"C:\Users\Hamad\Documents\Projects\Games\PixelRPG\MonoRPG_TiledProject\";
        private TileMap map;

        public Project()
        {

        }

        public void LoadMap(string filePath)
        {
            
        }

        public void LoadTile(string filePath)
        {

        }

        public void AddToTileSet(int inId, string inFilePath)
        {
            map.tileSet.Add(new Tile() { id = inId, fileName = inFilePath });
        }

        public void setFilePath(string fileName)
        {
            mapFilePath = @"C:\Users\Hamad\Documents\Projects\Games\PixelRPG\MonoRPG_TiledProject\" + fileName + ".xml";
        }

        public void WriteMap()
        {
            FileStream stream = File.Create(mapFilePath);
            Debug.WriteLine("Writing to : " + mapFilePath);
            MapWriter(stream);
        }

        private void MapWriter(Stream stream)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            //settings.Async = true;

            Debug.WriteLine("Writing map!");

            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                writer.WriteStartElement("map");
                writer.WriteAttributeString(null, "name", null, map.mapName);
                writer.WriteAttributeString(null, "width", null, map.width.ToString());
                writer.WriteAttributeString(null, "height", null, map.height.ToString());

                writer.WriteStartElement("tileset");
                
                foreach (Tile tile in map.tileSet)
                {
                    writer.WriteStartElement("tile");
                    writer.WriteAttributeString(null, "id", null, tile.id.ToString());
                    writer.WriteStartElement(null, "source", null);
                    writer.WriteString(tile.fileName.ToString());
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                
                writer.WriteEndElement();
                
                foreach (Layer layer in map.layers)
                {
                    writer.WriteStartElement("layer");
                    writer.WriteAttributeString("id", layer.id.ToString());
                    writer.WriteAttributeString("name", layer.name);
                    for (int i = 0; i < map.width; i++)
                    {
                        writer.WriteStartElement("Column");
                        writer.WriteAttributeString("index", i.ToString());
                        for (int j = 0; j < map.height; j++)
                        {
                            writer.WriteStartElement("Row");
                            writer.WriteAttributeString("index", j.ToString());
                            writer.WriteString(layer.data[i, j]);
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }

                writer.WriteStartElement("collisionMap");
                string colMap = string.Join(",", string.Join(",", map.collisionMap.ToString()));
                writer.WriteString(colMap);
                writer.WriteEndElement();

                writer.WriteEndElement();

                writer.Close();
            }
        }

        public void NewMap(string mapName, int inWidth, int inHeight)
        {
            map = new TileMap();
            map.width = inWidth;
            map.height = inHeight;
            map.mapName = mapName;
            map.layers = new List<Layer>();
            map.layers.Add(new Layer()
            {
                id = 0,
                name = "Background",
                data = new string[inWidth, inHeight]
            });
            map.layers.Add(new Layer()
            {
                id = 1,
                name = "Foreground",
                data = new string[inWidth, inHeight]
            });
            map.collisionMap = new bool[inWidth, inHeight];
            map.tileSet = new List<Tile>();

            mapFilePath += mapName + ".xml";
        }

        public void setMapData(string[,] inData, int layerId)
        {
            map.layers[layerId] = new Layer() {
                id = layerId,
                name = map.layers[layerId].name,
                data = inData
            };
        }

        public void setMapColMap(bool[,] inColMap)
        {
            map.collisionMap = inColMap;
        }

        public void RemoveLayer()
        {

        }

        public void AddNewLayer()
        {

        }
    }
}
