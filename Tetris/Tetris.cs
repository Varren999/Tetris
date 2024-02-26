//////////////////////////////////////////////////////////////////////////////////
// Autor: Vatslav Varren
// [*] Создание фигуры.
// [] Обработка движения фигуры.
// [] Проверка на столкновение с поверхностью.
//////////////////////////////////////////////////////////////////////////////////
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

        const int WIDTH = 22, HEIGHT = 15;
        
        float speed = 0;

        Figure CurFigure;
        Figure NextFigure;
        bool isFigureLive = false;

        bool isExit = false;

        string map = "#0000000000##########\n#0000000000#        #\n#0000000000#        #\n#0000000000#        #\n#0000000000#        #\n#0000000000##########\n#0000000000# Score: #\n#0000000000#        #\n#0000000000#        #\n#0000000000#        #\n#0000000000#        #\n#0000000000#        #\n#0000000000#        #\n#0000000000#        #\n#0000000000#        #\n###############################";

        // Обработка нажатий.
        void KeyDown()
        {
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.LeftArrow:
                        {
                            --CurFigure.pos.X;
                            if (CurFigure.pos.X < 1)
                            {
                                CurFigure.pos.X = 1;
                            }
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        {
                            ++CurFigure.pos.X;
                            if (CurFigure.pos.X + CurFigure.figure[0].Length > 11)
                            {
                                CurFigure.pos.X = 11 - CurFigure.figure[0].Length;
                            }
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        {
                            ++CurFigure.pos.Y;
                            if (CurFigure.pos.Y + CurFigure.figure.Length > HEIGHT - 1)
                            {
                                CurFigure.pos.Y = HEIGHT - CurFigure.figure.Length;
                            }
                        }
                        break;

                    case ConsoleKey.Escape:
                        {
                            isExit = true;
                        }
                        break;

                    case ConsoleKey.Spacebar:
                        {
                            CurFigure = CurFigure.Rotation(CurFigure);
                        }
                        break;
                }
            }
        }

        // Стартовые данные.
        void StartSettings()
        {
            
        }

        void BornFigure()
        {
            Figure temp = new Figure();
            CurFigure = NextFigure;
            NextFigure = temp;
            isFigureLive = true;
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
            bool collision = false;
            try
            {
                    for (int i = 0; i < CurFigure.figure[CurFigure.figure.Length - 1].Length; i++)
                    {
                        if (map[((CurFigure.pos.Y + CurFigure.figure.Length) * WIDTH) + CurFigure.pos.X + i] == '#' || map[((CurFigure.pos.Y + CurFigure.figure.Length) * WIDTH) + CurFigure.pos.X + i] == '1')
                            collision = true;
                    }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (collision)
            {
                isFigureLive = false;
            }
            else
                ++CurFigure.pos.Y;
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
                for (int c = 0; c < CurFigure.figure.Length; c++)
                {
                    for (int i = 0; i < CurFigure.figure[c].Length; i++)
                    { 
                        sb[((CurFigure.pos.Y + c) * WIDTH) + CurFigure.pos.X + i] = CurFigure.figure[c][i];
                    }
                }
                map = sb.ToString();
                map = map.Replace("0", "  ");
                map = map.Replace("1", "[]");
                Console.WriteLine(map);

                for (int c = 0; c < CurFigure.figure.Length; c++)
                {
                    for (int i = 0; i < CurFigure.figure[c].Length; i++)
                    {
                        sb[((CurFigure.pos.Y + c) * WIDTH) + CurFigure.pos.X + i] = '0';
                    }
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
            if (!isExit)
            {
                Console.SetCursorPosition(8, HEIGHT / 2);
                Console.Write("Ты проиграл!");
                Console.SetCursorPosition(WIDTH, HEIGHT);
            }
            else
            {
                Console.SetCursorPosition(8, HEIGHT / 2);
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
                long lastTimerScreen = timer.ElapsedMilliseconds;
                Figure temp = new Figure();
                NextFigure = temp;

                do
                {
                    if(!isFigureLive)
                    {
                        BornFigure();
                    }

                    KeyDown();
                    
                    if (timer.ElapsedMilliseconds - lastTimer >= 1000)
                    {
                        lastTimer = timer.ElapsedMilliseconds;

                        MoveFigure();

                    }

                    if (timer.ElapsedMilliseconds - lastTimerScreen >= 100)
                    {
                        lastTimerScreen = timer.ElapsedMilliseconds;

                        Screen();
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

