using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class Program
    {
        static void Main()
        {
            bool[,] bools = new bool[15, 10];

            string[,] pole = {{"#                    ##########" },
                              {"#                    #        #" },
                              {"#                    #        #" },
                              {"#                    #        #" },
                              {"#                    #        #" },
                              {"#                    ##########" },
                              {"#                    # Score: #" },
                              {"#                    #        #" },
                              {"#                    #        #" },
                              {"#                    #        #" },
                              {"#                    #        #" },
                              {"#                    #        #" },
                              {"#                    #        #" },
                              {"#                    #        #" },
                              {"#                    #        #" },
                              {"###############################" } };

            string[,] figureA = { { "[][]" },
                                  { "[][]" }};

            string[] figureB = { "[][][][]" };

            string[,] figureC = { { "[][]  " },
                                  { "  [][]" } };

            string[,] figureD = { {"[][][]"},
                                  {"[]    "} };

            string[,] figureE = { {"[]"},
                                  {"[]"},
                                  {"[]"},
                                  {"[]"}};

            string[,] figureF = { { "  []"},
                                  { "[][]"},
                                  { "[]  "} };

            string[,] figureG = { { "[][]"},
                                  { "  []"},
                                  { "  []"} };

            string[,] figureH = { { "  []  "},
                                  { "[][][]"} };
            try
            {
                for (int c = 0; c < pole.Rank; c++)
                {
                    for (int i = 0; i < pole.Length; i++)
                    {
                        Console.WriteLine(pole[i, c]);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
