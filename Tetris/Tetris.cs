//////////////////////////////////////////////////////////////////////////////////
// Autor: Vatslav Varren
// [*] Создание фигуры.
// [] Обработка движения фигуры.
// [] Проверка на столкновение с поверхностью.
// [] Оставляем фигуру и создаем новую.
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
        private Random random = new Random(DateTime.Now.Millisecond);
        private Stopwatch timer = new Stopwatch();

        private const int WIDTH = 12, HEIGHT = 16;
        
        private int speed = 1;

        private Figure CurFigure;
        private Figure NextFigure;
        private bool isFigureLive = false;
        private bool isExit = false;
        enum Move { Fall, Down, Left, Right}
        private int[,] map = new int[WIDTH,HEIGHT];   // Игровое поле.    

        // Обработка нажатий.
        //private void KeyDown()
        //{
        //    if (Console.KeyAvailable)
        //    {
        //        switch (Console.ReadKey().Key)
        //        {
        //            // Движение фигуры влево.
        //            case ConsoleKey.LeftArrow:
        //                {
        //                    MoveFigure(Move.Left);
        //                }
        //                break;
        //            // Движение фигуры вправо.
        //            case ConsoleKey.RightArrow:
        //                {
        //                    MoveFigure(Move.Right);
        //                }
        //                break;
        //            // Движение фигуры вниз.
        //            case ConsoleKey.DownArrow:
        //                {
        //                    MoveFigure(Move.Down);
        //                }
        //                break;
        //            // Кнопка выхода.
        //            case ConsoleKey.Escape:
        //                {
        //                    isExit = true;
        //                }
        //                break;
        //            // Ротация фигуры.
        //            case ConsoleKey.Spacebar:
        //                {
        //                    CurFigure = CurFigure.Rotation(CurFigure);
        //                    if (Collision())
        //                        CurFigure.pos.X = 11 - (CurFigure.figure.Length / (CurFigure.figure.GetUpperBound(0) + 1));
        //                }
        //                break;
        //        }
        //    }
        //}

        // Стартовые данные.
        private void StartSettings()
        {
            
        }

        // Создаем новую фигуру.
        private void BornFigure()
        {
            Figure temp = new Figure();
            CurFigure = NextFigure;
            NextFigure = temp;
            DrawFigure();
            isFigureLive = true;
        }

        // Проверка на проигрыш.
        private bool IsLoss()
        {
            
            return false;
        }

        // Проверка столкновений.
        private bool Collision()
        {
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    if (CurFigure.figure[0, i] > WIDTH || CurFigure.figure[0, i] <= 0 || CurFigure.figure[1, i] > HEIGHT - 1 || CurFigure.figure[1, i] <= 0)
                        return true;
                }
            }
            catch (Exception ex)
            {
                Message_Log("Collision " + ex.Message);
            }
            return false;
        }

        // Движение фигуры вниз.
        private void MoveFigure(Move move)
        {
            switch (move)
            {
                // Игрок нажимает кнопку движение вниз.
                case Move.Down:
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            map[CurFigure.figure[0, i], CurFigure.figure[1, i]] = 0;
                            CurFigure.figure[1, i]++;
                        }
                        if (Collision())
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                CurFigure.figure[1, i]--;
                            }
                            isFigureLive = false;
                        }
                    }
                    break;

                    //        // Игрок нажимает кнопку движение влево.
                    //        case Move.Left:
                    //            {
                    //                --CurFigure.pos.X;
                    //                if (Collision())
                    //                {
                    //                    ++CurFigure.pos.X;
                    //                }
                    //                else
                    //                {
                    //                    DrawFigure();
                    //                }
                    //            }
                    //            break;

                    //        // Игрок нажимает кнопку движение вправо.
                    //        case Move.Right:
                    //            {
                    //                ++CurFigure.pos.X;
                    //                if (Collision())
                    //                {
                    //                    --CurFigure.pos.X;
                    //                }
                    //                else
                    //                {
                    //                    DrawFigure();
                    //                }
                    //            }
                    //            break;
            }

        }

        // Метод очищает игровое поле.
        private void Clear()
        {
            string empty = "                                                                    ";
            Console.SetCursorPosition(0, 0);
            try
            {
                for (int c = 0; c < HEIGHT + 10; c++)
                    Console.WriteLine(empty);
            }
            catch (Exception ex)
            {
                Message_Log("Clear " + ex.Message);
            }
            Console.SetCursorPosition(0, 0);
        }

        // Отрисовка экрана.
        private void BuildingScene()
        {
            Clear();
            DrawMap();
            DrawFigure();
            Preparing();
        }

        // Метод рисует фигуру на игровом поле.
        private void DrawFigure()
        {
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    map[CurFigure.figure[0, i], CurFigure.figure[1, i]] = 1;
                }
            }
            catch (Exception ex)
            {
                Message_Log("DrawFigure" + ex.Message);
            }
        }

        //
        private void DrawMap()
        {
            try 
            {
                for (int x = 0; x < WIDTH; x++)
                {
                    map[x, HEIGHT - 1] = 6;
                }

                for (int y = 0; y < HEIGHT; y++)
                {
                    map[0, y] = 5;
                    map[WIDTH - 1, y] = 5;
                }             
            }
            catch(Exception ex)
            {
                Message_Log("DrawMap " + ex.Message);
            }
        }

        //
        private void Preparing()
        {
            try
            {
                for (int y = 0; y < map.Length / (map.GetUpperBound(0) + 1); y++)
                {
                    for (int x = 0; x < (map.GetUpperBound(0) + 1); x++)
                    {
                        Console.Write(Substitution(map[x, y]));
                        //Console.Write(map[x, y]);
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Message_Log("Preparing " + ex.Message);
            }
        }

        //
        private string Substitution(int value)
        {
            switch(value)
            {
                case 0:
                    return "  ";
                case 1:
                    return "[]";
                case 5:
                    return "#";
                case 6:
                    return "##";
            }
            return " ";
        }

        //
        private void Final()
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
            Message_Log("Запуск игры");
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
                    //KeyDown();
                    if (timer.ElapsedMilliseconds - lastTimerScreen >= 100)
                    {
                        lastTimerScreen = timer.ElapsedMilliseconds;
                        if (!isFigureLive)
                        {
                            BornFigure();
                        }
                        
                        BuildingScene();
                        
                    }
                    
                    if (timer.ElapsedMilliseconds - lastTimer >= (1000/speed))
                    {
                        lastTimer = timer.ElapsedMilliseconds;

                        MoveFigure(Move.Down);

                    }

                } while (!IsLoss() && !isExit);

                Final();

                timer.Stop();
            }
            catch(Exception ex)
            {
                Message_Log(ex.Message);
            }
            Message_Log("Выход из приложения");
        }

        //
        private void Message_Log(string text)
        {
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter("log.txt", true))
            {
                writer.WriteLine(text + ": " + DateTime.Now.ToShortDateString() + " " +
                DateTime.Now.ToLongTimeString());
                writer.Flush();
            }
        }

    }
}

