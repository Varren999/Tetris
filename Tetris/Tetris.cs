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
using Log;

namespace ConsoleApp
{
    internal class Tetris
    {
        //private readonly Random random = new Random(DateTime.Now.Millisecond);
        private readonly Stopwatch timer = new Stopwatch();

        private const int WIDTH = 12, HEIGHT = 16;
        
        private int speed = 1;

        private int[,] block = new int[2, 4];
        private Blocks next_block;
        private bool isBlock_Live = false;
        private bool isExit = false;
        enum Move { Down, Left, Right, Rotation}
        private int[,] Game_Fields = new int[WIDTH, HEIGHT];   // Игровое поле.    

        //Обработка нажатий.
        private void KeyDown()
        {
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey().Key)
                {
                    // Движение фигуры влево.
                    case ConsoleKey.LeftArrow:
                        {
                            MoveBlock(Move.Left);
                        } break;
                    case ConsoleKey.A:
                        {
                            goto case ConsoleKey.LeftArrow;
                        } 

                    // Движение фигуры вправо.
                    case ConsoleKey.RightArrow:
                        {
                            MoveBlock(Move.Right);
                        } break;
                    case ConsoleKey.D:
                        {
                            MoveBlock(Move.Right);
                        } break;

                    // Движение фигуры вниз.
                    case ConsoleKey.DownArrow:
                        {
                            MoveBlock(Move.Down);
                        }
                        break;
                    case ConsoleKey.S:
                        {
                            MoveBlock(Move.Down);
                        } break;

                    // Ротация фигуры.
                    case ConsoleKey.Spacebar:
                        {
                            MoveBlock(Move.Rotation);
                        } break;
                    case ConsoleKey.W:
                        {
                            MoveBlock(Move.Rotation);
                        } break;

                    // Кнопка выхода.
                    case ConsoleKey.Escape:
                        {
                            isExit = true;
                        } break;
                }
            }
        }

        // Создаем новую блок.
        private void Born_Block()
        {
            block = next_block.Block;
            if(Collision())
                isExit = true;
            next_block = new Blocks();
            isBlock_Live = true;
        }

        // Проверка столкновений.
        private bool Collision()
        {
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    if (block[0, i] >= WIDTH - 1 || block[0, i] <= 0 || block[1, i] >= HEIGHT - 1)// || Game_Fields[block[1, i], block[0, i]] == 1)
                        return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.TargetSite + ex.Message);
            }
            return false;
        }

        // Движение фигуры.
        private void MoveBlock(Move move)
        {
            switch (move)
            {
                // Игрок нажимает кнопку движение вниз.
                case Move.Down:
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            Game_Fields[block[0, i], block[1, i]] = 0;
                            block[1, i]++;
                        }
                        if (Collision())
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                block[1, i]--;
                            }
                            isBlock_Live = false;
                        }
                    }
                    break;

                // Игрок нажимает кнопку движение влево.
                case Move.Left:
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            Game_Fields[block[0, i], block[1, i]] = 0;
                            block[0, i]--;
                        }
                        if (Collision())
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                block[0, i]++;
                            }
                        }
                    }
                    break;

                // Игрок нажимает кнопку движение вправо.
                case Move.Right:
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            Game_Fields[block[0, i], block[1, i]] = 0;
                            block[0, i]++;
                        }
                        if (Collision())
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                block[0, i]--;
                            }
                        }
                    }
                    break;

                // Поворачиваем блок.
                case Move.Rotation:
                    {
                        Point max = new Point(0, 0);
                        int[,] temp = new int[2, 4];
                        Array.Copy(temp, block, block.Length);
                        for(int i = 0; i < 4; i++)
                        {
                            if (block[0, i] > max.Y)
                                max.Y = block[0, i];
                            if (block[1, i] > max.X)
                                max.X = block[1, i];
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            int value = block[0, i];
                            block[0, i] = max.Y - (max.X - block[1, i]) - 1;
                            block[1, i] = max.X - (3 - (max.Y - value)) + 1;
                        }
                        if (Collision())
                            Array.Copy(temp, block, temp.Length);
                    } break;
            }

        }

        // Метод очищает игровое поле.
        private void Clear()
        {
            string empty = "                                                                    ";
            Console.SetCursorPosition(0, 0);
            try
            {
                for (int y = 0; y < HEIGHT + 10; y++)
                    Console.WriteLine(empty);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.TargetSite + ex.Message);
            }
            Console.SetCursorPosition(0, 0);
        }

        // Отрисовка экрана.
        private void BuildingScene()
        {
            Clear();
            DrawFigure();
            DrawMap();
            Preparing();
        }

        // Метод рисует фигуру на игровом поле.
        private void DrawFigure()
        {
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    Game_Fields[block[0, i], block[1, i]] = 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.TargetSite + ex.Message);
            }
        }

        // Метод рисует игровое поле.
        private void DrawMap()
        {
            try 
            {
                //for (int y = 0; y < Game_Fields.Length / (Game_Fields.GetUpperBound(0) + 1); y++)
                //{
                //    for (int x = 0; x < (Game_Fields.GetUpperBound(0) + 1); x++)
                //    {
                //        Game_Fields[x, y];
                        
                //    }
                //    Console.WriteLine();
                //}

                for (int x = 0; x < WIDTH; x++)
                {
                    Game_Fields[x, HEIGHT - 1] = 6;
                }

                for (int y = 0; y < HEIGHT; y++)
                {
                    Game_Fields[0, y] = 5;
                    Game_Fields[WIDTH - 1, y] = 5;
                }             
            }
            catch(Exception ex)
            {
                Logger.Error(ex.TargetSite + ex.Message);
            }
        }

        // Метод преобразует игровое поле.
        private void Preparing()
        {
            try
            {
                for (int y = 0; y < Game_Fields.Length / (Game_Fields.GetUpperBound(0) + 1); y++)
                {
                    for (int x = 0; x < (Game_Fields.GetUpperBound(0) + 1); x++)
                    {
                        Console.Write(Substitution(Game_Fields[x, y]));
                        //Console.Write(map[x, y]);
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.TargetSite + ex.Message);
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
                timer.Start();
                long lastTimer = timer.ElapsedMilliseconds;
                long lastTimerScreen = timer.ElapsedMilliseconds;
                next_block = new Blocks();            
                do
                {
                    KeyDown();
                    if (timer.ElapsedMilliseconds - lastTimerScreen >= 100)
                    {
                        lastTimerScreen = timer.ElapsedMilliseconds;
                        if (!isBlock_Live)
                        {
                            Born_Block();
                        }
                        
                        BuildingScene();
                        
                    }
                    
                    if (timer.ElapsedMilliseconds - lastTimer >= (1000/speed))
                    {
                        lastTimer = timer.ElapsedMilliseconds;

                        MoveBlock(Move.Down);

                    }

                } while (!isExit);

                Final();

                timer.Stop();
            }
            catch(Exception ex)
            {
                Logger.Error(ex.TargetSite + ex.Message);
            }
        }
    }
}

