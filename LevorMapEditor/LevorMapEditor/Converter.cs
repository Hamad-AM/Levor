using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevorMapEditor
{
    static class HamadLib
    {
        public static List<List<T>> ArrayToList<T>(T[,] inData)
        {
            List<List<T>> outList = new List<List<T>>();

            //for (int i = 0; i < inData.GetLength(0); i++)
            //{
            //    outList.Add(new List<T>());
            //    for (int j = 0; j < inData.GetLength(1); j++)
            //    {
            //        outList[i].Add(null);
            //    }
            //}

            for (int i = 0; i < inData.GetLength(0); i++)
            {
                outList.Add(new List<T>());
                for (int j = 0; j < inData.GetLength(1); j++)
                {
                    outList[i].Add(inData[i, j]);
                }
            }
            return outList;
        }


        public static T[] Populate<T>(T[] data, T value)
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = value;
            }

            return data;
        }

        public static T[,] Populate<T>(T[,] data, T value)
        {
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    data[i, j] = value;
                }
            }

            return data;
        }
    }

}
