using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class Program
    {
        static void Screen(string[] text)
        {
            try
            {
                for (int c = 0; c < text.Length; c++)
                {
                    Console.WriteLine(text[c]);
                }
}
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void Main()
        {
            
            bool[,] bools = new bool[15, 10];

            string[] pole = {"#                    ##########",
                             "#                    #        #",
                             "#                    #        #",
                             "#                    #        #",
                             "#                    #        #",
                             "#                    ##########",
                             "#                    # Score: #",
                             "#                    #        #",
                             "#                    #        #",
                             "#                    #        #",
                             "#                    #        #",
                             "#                    #        #",
                             "#                    #        #",
                             "#                    #        #",
                             "#                    #        #",
                             "###############################"};

            string[] figureA = { "[][]",
                                 "[][]"};

            string[] figureB = { "[][][][]" };

            string[] figureC = { "[][]  ",
                                 "  [][]" };

            string[] figureD = { "[][][]",
                                 "[]    "};

            string[] figureE = { "[]",
                                 "[]",
                                 "[]",
                                 "[]"};

            string[] figureF = { "  []",
                                 "[][]",
                                 "[]  "};

            string[] figureG = { "[][]",
                                 "  []",
                                 "  []"};

            string[] figureH = { "  []  ",
                                 "[][][]"};

            Screen(pole);
        }
    }
}
