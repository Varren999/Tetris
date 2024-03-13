using System;
using System.Collections.Generic;
using System.Drawing;

namespace ConsoleApp
{
    internal class Blocks
    {
        private readonly Random random = new Random(DateTime.Now.Millisecond);

        private readonly int[,] block;

        public int[,] Block
        {
            get { return  block; }
        }

        public Blocks()
        {
            switch(random.Next(0, 6))
            {
                case 0:
                    {
                        block = new int[,]{ { 5, 6, 5, 6},       // [][]
                                            { 0, 0, 1, 1} };     // [][]
                    } break;

                case 1:
                    {
                        block = new int[,]{ { 4, 5, 6, 7 },
                                            { 0, 0, 0, 0 } };    // [][][][]
                    } break;

                case 2:
                    {
                        block = new int[,]{ { 5, 6, 6, 7 },      // [][]
                                            { 0, 0, 1, 1 } };    //   [][]
                    } break;

                case 3:
                    {
                        block = new int[,]{ { 5, 6, 6, 7 },      //   [][]
                                            { 1, 1, 0, 0 } };    // [][]
                    } break;

                case 4:
                    {
                        block = new int[,]{ { 5, 5, 6, 7 },      // [][][]
                                            { 1, 0, 0, 0 } };    // []
                    } break;

                case 5:
                    {
                        block = new int[,]{ { 5, 6, 7, 7 },      // [][][]
                                            { 0, 0, 0, 1 } };    //     []
                    } break;

                case 6:
                    {
                        block = new int[,]{ { 5, 6, 6, 7 },      //   []
                                            { 1, 1, 0, 1 } };    // [][][]
                    } break;
            }
        }
    }
}
