using System;
using System.Collections.Generic;
using System.Drawing;

namespace ConsoleApp
{
    internal class Figure
    {
        private Random random = new Random();

        public Point pos = new Point();
        public int[,] figure;

        private static int[,] cube = {{ 1, 1},              // [][]
                                      { 1, 1}};             // [][]

        private static int[,] lineH = { { 1, 1, 1, 1 } };   // [][][][]

        private static int[,] lineV = { { 1 },              // []
                                        { 1 },              // []
                                        { 1 },              // []
                                        { 1 } };            // []

        private static int[,] angleH = { { 1, 1, 0 },       // [][]
                                         { 0, 1, 1 } };     //   [][]

        private static int[,] angleV = { { 0, 1 },          //   []
                                         { 1, 1 },          // [][]
                                         { 1, 0 } };        // []

        private static int[,] RangleH = { { 0, 1, 1 },      //   [][]
                                          { 1, 1, 0 } };    // [][]

        private static int[,] RangleV = { { 1, 0 },         // []
                                          { 1, 1 },         // [][]
                                          { 0, 1 } };       //   []

        private static int[,] G1 = { { 1, 1, 1 },           // [][][]
                                     { 1, 0, 0 } };         // []

        private static int[,] G2 = { { 1, 1 },              // [][]
                                     { 0, 1 },              //   []
                                     { 0, 1 } };            //   []

        private static int[,] G3 = { { 0, 0, 1 },           //     []
                                     { 1, 1, 1 } };         // [][][]

        private static int[,] G4 = { { 1, 0 },              // []
                                     { 1, 0 },              // []
                                     { 1, 1 } };            // [][]

        private static int[,] RG1 = { { 1, 1, 1 },          // [][][]
                                      { 0, 0, 1 } };        //     []

        private static int[,] RG2 = { { 0, 1 },             //   []
                                      { 0, 1 },             //   []
                                      { 1, 1 } };           // [][]

        private static int[,] RG3 = { { 1, 0, 0 },          // []
                                      { 1, 1, 1 } };        // [][][]

        private static int[,] RG4 = { { 1, 1 },             // [][]
                                      { 1, 0 },             // []
                                      { 1, 0 } };           // []     

        private static int[,] T1 = { { 0, 1, 0 },           //   []
                                     { 1, 1, 1 } };         // [][][]

        private static int[,] T2 = { { 1, 0 },              // []
                                     { 1, 1 },              // [][]
                                     { 1, 0 } };            // []      

        private static int[,] T3 = { { 1, 1, 1 },           // [][][]
                                     { 0, 1, 0 } };         //   []

        private static int[,] T4 = { { 0, 1 },              //   []
                                     { 1, 1 },              // [][]
                                     { 0, 1 } };            //   [] 

        private List<int[,]> CollectionFigures = new List<int[,]>() { cube, lineH, lineV, angleH, angleV, RangleH, RangleV, G1, G2, G3, G4, RG1, RG2, RG3, RG4, T1, T2, T3, T4};
        
        public Figure()
        {
            pos.X = 5;
            pos.Y = 0;
            figure = CollectionFigures[random.Next(0, 18)];
        }

        // Метод заменяет полученую фигуру на следующую за ней.
        public Figure Rotation(Figure current)
        {
            if (current.figure == cube)
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
