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
        private Random random = new Random();
        private Stopwatch timer = new Stopwatch();
        //private StringBuilder sb = new StringBuilder(map);

        private const int WIDTH = 11, HEIGHT = 15;
        
        private int speed = 1;

        private Figure CurFigure;
        private Figure NextFigure;
        private Point lastPos = new Point(0, 0);
        private bool isFigureLive = false;
        private bool isExit = false;
        enum Move { Fall, Down, Left, Right}
        // Игровое поле.
        private int[,] map = new int[,]  { { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 }, // 0
                                           { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 }, // 1
                                           { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 }, // 2
                                           { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 }, // 3
                                           { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 }, // 4
                                           { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 }, // 5
                                           { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 }, // 6
                                           { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 }, // 7
                                           { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 }, // 8
                                           { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 }, // 9
                                           { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 }, // 10
                                           { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 }, // 11
                                           { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 }, // 12
                                           { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 }, // 13
                                           { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 }, // 14
                                           { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 }, // 15
                                           { 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 5 } }; // 16
        

        // Обработка нажатий.
        private void KeyDown()
        {
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey().Key)
                {
                    // Движение фигуры влево.
                    case ConsoleKey.LeftArrow:
                        {
                            MoveFigure(Move.Left);
                        }
                        break;
                    // Движение фигуры вправо.
                    case ConsoleKey.RightArrow:
                        {
                            MoveFigure(Move.Right);
                        }
                        break;
                    // Движение фигуры вниз.
                    case ConsoleKey.DownArrow:
                        {
                            MoveFigure(Move.Down);
                        }
                        break;
                    // Кнопка выхода.
                    case ConsoleKey.Escape:
                        {
                            isExit = true;
                        }
                        break;
                    // Ротация фигуры.
                    case ConsoleKey.Spacebar:
                        {
                            CurFigure = CurFigure.Rotation(CurFigure);
                            if (Collision())
                                CurFigure.pos.X = 11 - (CurFigure.figure.Length / (CurFigure.figure.GetUpperBound(0) + 1));
                        }
                        break;
                }
            }
        }

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
            lastPos = CurFigure.pos;
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
            bool collision = false;
            try
            {
                for (int i = 0; i < (CurFigure.figure.GetUpperBound(0) + 1); i++)
                {
                    for (int j = 0; j < CurFigure.figure.Length / (CurFigure.figure.GetUpperBound(0) + 1); j++)
                    {
                        if (map[CurFigure.pos.Y + i, CurFigure.pos.X + j] == 5 || map[CurFigure.pos.Y + i, CurFigure.pos.X + j] == 6) // || map[CurFigure.pos.Y + i, CurFigure.pos.X + j] == 1)
                            collision = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return collision;
        }

        // Движение фигуры вниз.
        private void MoveFigure(Move move)
        {
            switch (move)
            {
                // Игрок нажимает кнопку движение вниз.
                case Move.Down:
                    {
                        ++CurFigure.pos.Y;
                        if (Collision())
                        {
                            --CurFigure.pos.Y;
                            isFigureLive = false;
                        }
                        else
                        {
                            ClearFigure();
                            DrawFigure();
                            lastPos = CurFigure.pos;
                        }
                    }
                    break;

                // Игрок нажимает кнопку движение влево.
                case Move.Left:
                    {
                        --CurFigure.pos.X;
                        if (Collision())
                        {
                            ++CurFigure.pos.X;
                        }
                        else
                        {
                            ClearFigure();
                            DrawFigure();
                            lastPos = CurFigure.pos;
                        }
                    }
                    break;

                // Игрок нажимает кнопку движение вправо.
                case Move.Right:
                    {
                        ++CurFigure.pos.X;
                        if (Collision())
                        {
                            --CurFigure.pos.X;
                        }
                        else
                        {
                            ClearFigure();
                            DrawFigure();
                            lastPos = CurFigure.pos;
                        }
                    }
                    break;
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
                Console.WriteLine(ex.Message);
            }
            Console.SetCursorPosition(0, 0);
        }

        // Отрисовка экрана.
        private void BuildingScene()
        {
            Clear();
            Preparing();
        }

        // Метод рисует фигуру на игровом поле.
        private void DrawFigure()
        {
            try
            {
                for (int i = 0; i < (CurFigure.figure.GetUpperBound(0) + 1); i++)
                {
                    for (int j = 0; j < CurFigure.figure.Length / (CurFigure.figure.GetUpperBound(0) + 1); j++)
                    {
                        map[CurFigure.pos.Y + i, CurFigure.pos.X + j] = CurFigure.figure[i, j];
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DrawFigure" + ex.Message);
            }
        }

        // Метод стирает фигуру на игровом поле.
        private void ClearFigure()
        {
            try
            {
                for (int i = 0; i < (CurFigure.figure.GetUpperBound(0) + 1); i++)
                {
                    for (int j = 0; j < CurFigure.figure.Length / (CurFigure.figure.GetUpperBound(0) + 1); j++)
                    {
                        map[lastPos.Y + i, lastPos.X + j] = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DrawFigure" + ex.Message);
            }
        }

        //
        private void Preparing()
        {
            try
            {
                for (int c = 0; c < (map.GetUpperBound(0) + 1); c++)
                {
                    for (int i = 0; i < map.Length / (map.GetUpperBound(0) + 1); i++)
                    {
                        Console.Write(Substitution(map[c, i]));
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                    KeyDown();
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
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}

