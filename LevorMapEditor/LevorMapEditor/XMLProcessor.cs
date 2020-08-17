using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace LevorMapEditor
{
    class XMLProcessor
    {
        private static XElement file;

        public XMLProcessor()
        {

        }

        public static void loadFile(string filePath)
        {
            file = XElement.Load(filePath);
        }
    }
}
