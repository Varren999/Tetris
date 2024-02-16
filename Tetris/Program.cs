using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main()
        {
            
            bool[,] bools = new bool[15, 10];


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

            //Screen(map);
            Tetris tr = new Tetris();
            tr.Play();
        }
    }
}
