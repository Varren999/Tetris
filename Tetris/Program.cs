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
            //Tetris game = new Tetris();
            //game.Play();
            Figure fig = new Figure();
            for (int i = 0; i < fig.figure.Length; i++)
            {
                Console.WriteLine(fig.figure[i]);
            }

            fig = fig.Rotation(fig);
            Console.WriteLine();
            for (int i = 0; i < fig.figure.Length; i++)
            {
                Console.WriteLine(fig.figure[i]);
            }
        }
    }
}
