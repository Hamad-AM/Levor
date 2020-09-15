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
using System.Xml.Serialization;
using DataType;

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
            mapFilePath = @"" + filePath;
            FileStream stream = File.OpenRead(filePath);
            //MapReader(stream);
            MapDeserialize(stream);
            stream.Close();
        }


        public TileMap GetTileMap()
        {
            return map;
        }


        public void LoadTile(string filePath)
        {

        }

        public void AddToTileSet(int inId, string inFilePath)
        {
            map.tileSet.Add(new Tile() { id = inId, fileName = inFilePath });
        }

        public void SetFilePath(string fileName)
        {
            mapFilePath = @"C:\Users\Hamad\Documents\Projects\Games\PixelRPG\MonoRPG_TiledProject\" + fileName + ".xml";
        }

        public void WriteMap()
        {
            FileStream stream = File.Create(mapFilePath);
            Debug.WriteLine("Writing to : " + mapFilePath);
            // MapWriter(stream);
            MapSerialize(stream);
            stream.Close();
        }

        private void MapReader(Stream stream)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            using (XmlReader reader = XmlReader.Create(stream, settings))
            {
                while (reader.Read())
                {
                    switch (reader.ReadElementString())
                    {
                        case "map":
                            map = new TileMap();
                            map.width = Int32.Parse(reader.GetAttribute(0));
                            map.height = Int32.Parse(reader.GetAttribute(1));
                            break;

                        case "tileset":
                            map.tileSet = new List<Tile>();
                            break;

                        case "tile":
                            break;
                        case "layer":
                            break;
                        case "collisionMap":
                            break;
                    }

                    if (reader.ReadElementString() == "map")
                    {
                        map = new TileMap();
                        map.width = Int32.Parse(reader.GetAttribute(0));
                    }
                    else if (reader.ReadElementString() == "tileset")
                    {
                        map.tileSet = new List<Tile>();
                    }
                    else if (reader.ReadElementString() == "tile")
                    {
                        int tileId = Int32.Parse(reader.GetAttribute(0));
                        if (tileId < map.tileSet.Count)
                        {
                            Tile tile = map.tileSet[tileId];
                            tile.id = tileId;
                        }
                    }
                }
            }
        }

        private void MapDeserialize(Stream stream)
        {
            //XmlSerializer reader = new XmlSerializer(typeof(TileMap));
            //map = (TileMap)reader.Deserialize(stream);
            //stream.Close();


            // using monogame content xmlserializer
            XmlWriterSettings settings = new XmlWriterSettings();
            using XmlReader reader = XmlReader.Create(stream);
            map = Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate.IntermediateSerializer.Deserialize<TileMap>(reader, null);
        }

        private void MapSerialize(Stream stream)
        {
            //XmlSerializer writer = new XmlSerializer(typeof(TileMap));
            //writer.Serialize(stream, map);
            XmlWriterSettings settings = new XmlWriterSettings();

            using XmlWriter writer = XmlWriter.Create(stream, settings);

            Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate.IntermediateSerializer.Serialize<TileMap>(writer, map, null);
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
                    string data = "";
                    for (int i = 0; i < map.height; i++)
                    {
                        string[] row = new string[map.width];
                        for (int j = 0; j < map.width; j++)
                        {
                            row[j] = layer.data[i][j].ToString();
                        }
                        if (i == 0)
                        {
                            data += string.Join(",", row);
                        }
                        else
                        {
                            data += "," + string.Join(",", row);
                        }
                        
                    }
                    writer.WriteString(data);
                    writer.WriteEndElement();
                }

                writer.WriteStartElement("collisionMap");
                string colMap = "";
                for (int i = 0; i < map.height; i++)
                {
                    string[] row = new string[map.width];
                    for (int j = 0; j < map.width; j++)
                    {
                        row[j] = map.collisionMap[i][j].ToString();
                    }
                    if (i == 0)
                    {
                        colMap += string.Join(",", row);
                    }
                    else
                    {
                        colMap += "," + string.Join(",", row);
                    }
                }
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
                data = HamadLib.ArrayToList<int>(HamadLib.Populate<int>(new int[inWidth,inHeight], 0))
            });
            map.layers.Add(new Layer()
            {
                id = 1,
                name = "Foreground",
                data = HamadLib.ArrayToList<int>(HamadLib.Populate<int>(new int[inWidth, inHeight], 0))
            });
            map.collisionMap = HamadLib.ArrayToList<bool>(HamadLib.Populate<bool>(new bool[inWidth, inHeight], false));
            map.tileSet = new List<Tile>();

            SetFilePath(mapName);
        }

        public void setMapData(int[,] inData, int layerId)
        {
            List<List<int>> inDataList = HamadLib.ArrayToList<int>(inData);

            map.layers[layerId] = new Layer() {
                id = layerId,
                name = map.layers[layerId].name,
                data = inDataList
            };
        }


        public void setMapColMap(bool[,] inColMap)
        {
            map.collisionMap = HamadLib.ArrayToList<bool>(inColMap);
        }

        public void RemoveLayer()
        {

        }

        public void AddNewLayer()
        {

        }
    }
}
