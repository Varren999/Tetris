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
        private readonly Random random = new Random(DateTime.Now.Millisecond);
        private Stopwatch timer = new Stopwatch();

        private const int WIDTH = 12, HEIGHT = 16;
        
        private int speed = 1;

        private Blocks Current_Block;
        private Blocks Next_Block;
        private bool isBlock_Live = false;
        private bool isExit = false;
        enum Move { Fall, Down, Left, Right}
        private int[,] Game_Fields = new int[WIDTH,HEIGHT];   // Игровое поле.    

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
                        }
                        break;
                    // Движение фигуры вправо.
                    case ConsoleKey.RightArrow:
                        {
                            MoveBlock(Move.Right);
                        }
                        break;
                    // Движение фигуры вниз.
                    case ConsoleKey.DownArrow:
                        {
                            MoveBlock(Move.Down);
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
                            
                        }
                        break;
                }
            }
        }

        // Создаем новую блок.
        private void Born_Block()
        {
            Next_Block = new Blocks();
            Current_Block = Next_Block;
            isBlock_Live = true;
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
                    if (Current_Block.Block[0, i] >= WIDTH - 1 || Current_Block.Block[0, i] <= 0 || Current_Block.Block[1, i] >= HEIGHT - 1 || Current_Block.Block[1, i] <= 0)
                        return true;
                }
            }
            catch (Exception ex)
            {
                Message_Log("Collision " + ex.Message);
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
                            //Game_Fields[Current_Block.Block[0, i], Current_Block.Block[1, i]] = 0;
                            Current_Block.Block[1, i]++;
                        }
                        if (Collision())
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                Current_Block.Block[1, i]--;
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
                            //Game_Fields[Current_Block.Block[0, i], Current_Block.Block[1, i]] = 0;
                            Current_Block.Block[0, i]--;
                        }
                        if (Collision())
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                Current_Block.Block[0, i]++;
                            }
                        }
                    }
                    break;

                // Игрок нажимает кнопку движение вправо.
                case Move.Right:
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            //Game_Fields[Current_Block.Block[0, i], Current_Block.Block[1, i]] = 0;
                            Current_Block.Block[0, i]++;
                        }
                        if (Collision())
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                Current_Block.Block[0, i]--;
                            }
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
                    Game_Fields[Current_Block.Block[0, i], Current_Block.Block[1, i]] = 1;
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
                Message_Log("DrawMap " + ex.Message);
            }
        }

        //
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
                timer.Start();
                long lastTimer = timer.ElapsedMilliseconds;
                long lastTimerScreen = timer.ElapsedMilliseconds;
                Next_Block = new Blocks();            
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

        // Метод для логирования.
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

