using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp
{
    internal class Tetris
    {
        Random random = new Random();
        Stopwatch timer = new Stopwatch();
        StringBuilder sb = new StringBuilder();

        const int WIDTH = 32, HEIGHT = 15;
        
        float speed = 0;

        enum MOVE { DOWN, LEFT, RIGTH };


        string figure = "[]";
        Point pFigure = new Point();
        bool deep = false;

        int snake_dir;

        bool isExit = false;

        string map = "#                    ##########\n#                    #        #\n#                    #        #\n#                    #        #\n#                    #        #\n#                    ##########\n#                    # Score: #\n#                    #        #\n#                    #        #\n#                    #        #\n#                    #        #\n#                    #        #\n#                    #        #\n#                    #        #\n#                    #        #\n###############################";

        // Обработка нажатий.
        void KeyDown()
        {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.LeftArrow:
                        {

                        }
                        break;

                    case ConsoleKey.RightArrow:
                        {

                        }
                        break;

                    case ConsoleKey.DownArrow:
                        {

                        }
                        break;

                    case ConsoleKey.Escape:
                        {
                            isExit = true;
                        }
                        break;

                    case ConsoleKey.Spacebar:
                        {

                        }
                        break;
                }
        }

        // Стартовые данные.
        void StartSettings()
        {
            
        }

        //
        bool IsFigureLive()
        {
            return false;
        }

        void FigureBorn()
        {

        }

        // Проверка на проигрыш.
        bool IsLoss()
        {
            // Проверка на столкновение со стеной.
            //if (pSnake[0].X == 0 || pSnake[0].Y == 0 || pSnake[0].X == WIDTH - 2 || pSnake[0].Y == HEIGHT - 1)
            //    return true;

            //for (size_t i = 1; i < len; i++)
            //{
            //    if (pSnake[0].X == pSnake[i].X && pSnake[0].Y == pSnake[i].Y)
            //    {
            //        i = len;
            //        return true;
            //    }
            //}
            return false;
        }

        // Движение.
        void MoveFigure()
        {
            if (pFigure.Y == HEIGHT - 1)
            {
                deep = true;
                pFigure.Y = 0;
            }
            else
                ++pFigure.Y;

        }

        // Метод очищает игровое поле.
        void Clear()
        {
            string empty = "                                                                    ";
            Console.SetCursorPosition(0, 0);
            try
            {
                for (int c = 0; c < HEIGHT; c++)
                    Console.WriteLine(empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.SetCursorPosition(0, 0);
        }

        // Отрисовка экрана.
        void Screen()
        {
            Clear();
        
            try
            {
                sb = new StringBuilder(map);
                for (int c = 0; c < figure.Length; c++)
                {
                    sb[pFigure.Y * WIDTH + pFigure.X + c] = figure[c];
                }
                map = sb.ToString();

                Console.WriteLine(map);

                for (int c = 0; c < figure.Length; c++)
                {
                    sb[pFigure.Y * WIDTH + pFigure.X + c] = ' ';
                }
                map = sb.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //
        void Final()
        {
            if (isExit)
            {
                Console.SetCursorPosition(13, HEIGHT / 2);
                Console.Write("Ты проиграл!");
                Console.SetCursorPosition(WIDTH, HEIGHT);
            }
            else
            {
                Console.SetCursorPosition(13, HEIGHT / 2);
                Console.Write("Игра завершена!");
                Console.SetCursorPosition(WIDTH, HEIGHT);
            }
        }

        //
        public void Play()
        {
            try
            {
                StartSettings();
                timer.Start();
                long lastTimer = timer.ElapsedMilliseconds;
                pFigure.X = 9;
                pFigure.Y = 0;
                do
                {
                    if(!IsFigureLive())
                    {
                        FigureBorn();
                    }

                    if (timer.ElapsedMilliseconds - lastTimer >= 1000)
                    {
                        lastTimer = timer.ElapsedMilliseconds;

                        KeyDown();

                        Screen();

                        MoveFigure();

                    }
                } while (!IsLoss() && !isExit);

                Final();

                timer.Stop();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

