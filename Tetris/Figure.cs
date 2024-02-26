using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Figure
    {
        Random random = new Random();
        public Point pos = new Point();

        public string[] figure;

        static string[] cube = { "11",      // [][]
                                 "11"};     // [][]

        static string[] lineH = { "1111" }; // [][][][]

        static string[] lineV = { "1",      // []
                                  "1",      // []
                                  "1",      // []
                                  "1"};     // []

        static string[] angleH= { "110",      // [][]
                                  "011" };    //   [][]

        static string[] angleV = { "01",       //   []
                                   "11",       // [][]
                                   "10"};      // []

        static string[] RangleH = { "011",      //   [][]
                                    "110" };    // [][]

        static string[] RangleV = { "10",       // []
                                    "11",       // [][]
                                    "01"};      //   []

        static string[] G1 = { "111",           // [][][]
                               "100"};          // []

        static string[] G2 = { "11",            // [][]
                               "01",            //   []
                               "01"};           //   []

        static string[] G3 = { "001",           //     []
                               "111"};          // [][][]

        static string[] G4 = { "10",            // []
                               "10",            // []
                               "11"};           // [][]

        static string[] RG1 = { "111",          // [][][]
                                "001"};         //     []

        static string[] RG2 = { "11",           // [][]
                                "10",           // []
                                "10"};          // []

        static string[] RG3 = { "100",          // []
                                "111"};         // [][][]

        static string[] RG4 = { "01",           //   []
                                "01",           //   []
                                "11"};          // [][]

        static string[] T1 = { "010",           //   []
                               "111"};          // [][][]

        static string[] T2 = { "10",            // []
                               "11",            // [][]
                               "10"};           // []      

        static string[] T3 = { "111",           // [][][]
                               "010"};          //   []

        static string[] T4 = { "01",            //   []
                               "11",            // [][]
                               "01"};           //   [] 

        List<string[]>ColFigure = new List<string[]>() { cube, lineH, lineV, angleH, angleV, RangleH, RangleV, G1, G2, G3, G4, RG1, RG2, RG3, RG4, T1, T2, T3, T4};
        
        public Figure()
        {
            pos.X = 5;
            pos.Y = 0;
            figure = ColFigure[random.Next(1, 19)];
        }

        public Figure Rotation(Figure current)
        {
            if(current.figure == cube)
                return current;

            if (current.figure == lineH)
            {
                current.figure = lineV;
                return current;
            }

            if (current.figure == lineV)
            {
                current.figure = lineH;
                return current;
            }

            if (current.figure == angleH)
            {
                current.figure = angleV;
                return current;
            }

            if (current.figure == angleV)
            {
                current.figure = angleH;
                return current;
            }

            if (current.figure == RangleH)
            {
                current.figure = RangleV;
                return current;
            }

            if (current.figure == RangleV)
            {
                current.figure = RangleH;
                return current;
            }

            if (current.figure == G1)
            {
                current.figure = G2;
                return current;
            }

            if (current.figure == G2)
            {
                current.figure = G3;
                return current;
            }

            if (current.figure == G3)
            {
                current.figure = G4;
                return current;
            }

            if (current.figure == G4)
            {
                current.figure = G1;
                return current;
            }

            if (current.figure == RG1)
            {
                current.figure = RG2;
                return current;
            }

            if (current.figure == RG2)
            {
                current.figure = RG3;
                return current;
            }

            if (current.figure == RG3)
            {
                current.figure = RG4;
                return current;
            }

            if (current.figure == RG4)
            {
                current.figure = RG1;
                return current;
            }

            if (current.figure == T1)
            {
                current.figure = T2;
                return current;
            }

            if (current.figure == T2)
            {
                current.figure = T3;
                return current;
            }

            if (current.figure == T3)
            {
                current.figure = T4;
                return current;
            }

            if (current.figure == T4)
            {
                current.figure = T1;
                return current;
            }

            return current;
        }

    }
}
