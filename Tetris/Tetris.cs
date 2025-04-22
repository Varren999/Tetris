//////////////////////////////////////////////////////////////////////////////////
// Autor: Vatslav Varren
//////////////////////////////////////////////////////////////////////////////////
using System;
using System.Diagnostics;
using System.Drawing;
using Log;

namespace ConsoleApp
{
    internal class Tetris
    {
        private readonly Stopwatch timer = new Stopwatch();
        private Random random;

        private const int WIDTH = 12, HEIGHT = 18;
        
        private int speed = 1;
        private int scope = 0;
        private string player = "";
        private int thisFigure = 0;
        private int nextFigure = 0;

        private Point[] block;
        private Blocks next_block;
        private bool isBlock_Live = false;

        private bool isPause = false;
        private bool isExit = false;
        enum Move { Down, FastDown, Left, Right, Rotation};

        private int[,] Game_Fields = new int[WIDTH, HEIGHT];

        /// <summary>
        /// Обработка нажатий.
        /// </summary>
        private void KeyDown()
        {
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey().Key)
                {
                    // Движение фигуры влево.
                    case ConsoleKey.LeftArrow:
                            MoveBlock(Move.Left); break;
                    case ConsoleKey.A:
                            MoveBlock(Move.Left); break;

                    // Движение фигуры вправо.
                    case ConsoleKey.RightArrow:
                            MoveBlock(Move.Right); break;
                    case ConsoleKey.D:
                            MoveBlock(Move.Right); break;

                    // Движение фигуры вниз.
                    case ConsoleKey.DownArrow:
                            MoveBlock(Move.FastDown); break;
                    case ConsoleKey.S:
                            MoveBlock(Move.FastDown); break;

                    // Ротация фигуры.
                    case ConsoleKey.Spacebar:
                            MoveBlock(Move.Rotation); break;
                    case ConsoleKey.W:
                            MoveBlock(Move.Rotation); break;

                    // Кнопка выхода.
                    case ConsoleKey.Escape:
                            isExit = true; break;

                    // Кнопка паузы.
                    case ConsoleKey.P:
                        isPause = !isPause; break;
                }
            }
        }

        /// <summary>
        /// Создаем новый блок.
        /// </summary>
        private void Born_Block()
        {
            block = next_block.Block;
            if (Collision())
                isExit = true;
            thisFigure = nextFigure;
            nextFigure = random.Next(0, 7);
            next_block = new Blocks(nextFigure);
            isBlock_Live = true;
        }

        /// <summary>
        /// Проверка столкновений.
        /// </summary>
        /// <returns></returns>
        private bool Collision()
        {
            try
            {
                for (int i = 0; i < block.Length; i++)
                {
                    // Проверяем столкновение со стенами и дном.
                    if (block[i].X <= 0 || block[i].X >= WIDTH - 1 || block[i].Y >= HEIGHT - 1 || block[i].Y < 0)
                        return true;

                    // Проверка столкновения с другими фигурами.
                    if (block[i].Y >= 0 && Game_Fields[block[i].X, block[i].Y] == 2)
                        return true;
                }              
            }
            catch (Exception ex)
            {
                Logger.Error(ex.TargetSite + " " + ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Метод движения блока.
        /// </summary>
        /// <param name="move"></param>
        private void MoveBlock(Move move)
        {
            try
            {
                if (!isPause) // Если игра на паузе блоки не двигаются)).
                {
                    // Сохраняем текущее положение на случай отката.
                    Point[] oldPosition = new Point[block.Length];
                    Array.Copy(block, oldPosition, block.Length);

                    switch (move)
                    {
                        case Move.Down:
                            for (int i = 0; i < block.Length; i++)
                            {
                                Game_Fields[block[i].X, block[i].Y] = 0;
                                block[i].Y++;
                            }
                            break;

                        case Move.FastDown:
                            while (!Collision())
                            {
                                // Сохраняем текущее положение на случай отката.
                                oldPosition = new Point[block.Length];
                                Array.Copy(block, oldPosition, block.Length);

                                for (int i = 0; i < block.Length; i++)
                                {
                                    Game_Fields[block[i].X, block[i].Y] = 0;
                                    block[i].Y++;
                                }
                            } break;
                            

                        case Move.Left:
                            for (int i = 0; i < block.Length; i++)
                            {
                                Game_Fields[block[i].X, block[i].Y] = 0;
                                block[i].X--;
                            }
                            break;

                        case Move.Right:
                            for (int i = 0; i < block.Length; i++)
                            {
                                Game_Fields[block[i].X, block[i].Y] = 0;
                                block[i].X++;
                            }
                            break;

                        case Move.Rotation:
                            if (thisFigure == 0) return; // Если фигура квадрат его вращать не нужно.
                            Point center;
                            if ( thisFigure == 6) // Если фигура T то центр вращения 3 точка массива у остальных фигур вторая.
                                center = block[2];
                            else
                                center = block[1]; 
                            Point[] newPositions = new Point[4];

                            for (int i = 0; i < 4; i++)
                            {
                                // Вычисляем новые координаты после поворота
                                int newX = center.X - (block[i].Y - center.Y);
                                int newY = center.Y + (block[i].X - center.X);
                                newPositions[i] = new Point(newX, newY);
                            }

                            // Применяем новые позиции
                            for (int i = 0; i < 4; i++)
                            {
                                Game_Fields[block[i].X, block[i].Y] = 0;
                                block[i] = newPositions[i];
                            }
                            break;
                    }

                    // Если после движения произошло столкновение - возвращаем старое положение
                    if (Collision())
                    {
                        Array.Copy(oldPosition, block, block.Length);
                        if (move == Move.Down || move == Move.FastDown)
                        {
                            FixBlock();
                            isBlock_Live = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.TargetSite + " " + ex.Message);
            }
        }

        // Фиксация блока на игровом поле.
        private void FixBlock()
        {
            for (int i = 0; i < block.Length; i++)
            {
                if (block[i].Y >= 0 || block[i].X <= WIDTH - 1) // Проверяем, что блок в пределах видимой области
                {
                    Game_Fields[block[i].X, block[i].Y] = 2; // 2 - зафиксированный блок
                }
            }
            CheckCompletedLines();
        }

        // Проверка заполненных линий.
        private void CheckCompletedLines()
        {
            for (int y = HEIGHT - 2; y >= 0; y--) // Идем снизу вверх
            {
                bool lineComplete = true;

                // Проверяем всю строку кроме границ
                for (int x = 1; x < WIDTH - 1; x++)
                {
                    if (Game_Fields[x, y] != 2)
                    {
                        lineComplete = false;
                        break;
                    }
                }

                if (lineComplete)
                {
                    RemoveLine(y);
                    y++;
                }
            }
        }

        // Удаление заполненной линии и смещение вышележащих строк вниз
        private void RemoveLine(int lineToRemove)
        {
            // Смещаем все строки выше удаляемой вниз
            for (int y = lineToRemove; y > 0; y--)
            {
                for (int x = 1; x < WIDTH - 1; x++)
                {
                    Game_Fields[x, y] = Game_Fields[x, y - 1];
                }
            }

            // Очищаем верхнюю строку
            for (int x = 1; x < WIDTH - 1; x++)
            {
                Game_Fields[x, 0] = 0;
            }

            scope += 100; 
        }

        /// <summary>
        /// Метод очищает экран.
        /// </summary>
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
                Logger.Error(ex.TargetSite + " " + ex.Message);
            }
            Console.SetCursorPosition(0, 0);
        }

        // Отрисовка экрана.
        private void BuildingScene()
        {
            Clear();
            DrawFigure(1);
            DrawMap();
            Preparing();
        }

        // Метод рисует фигуру на игровом поле.
        private void DrawFigure(int digits)
        {
            try
            {
                for (int i = 0; i < block.Length; i++)
                {
                    Game_Fields[block[i].X, block[i].Y] = digits;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.TargetSite + " " + ex.Message);
            }
        }

        // Метод рисует игровое поле.
        private void DrawMap()
        {
            try 
            {
                // Рисуем дно.
                for (int x = 0; x < WIDTH; x++)
                {
                    Game_Fields[x, HEIGHT - 1] = 6;
                }

                // Рисуем стены.
                for (int y = 0; y < HEIGHT; y++)
                {
                    Game_Fields[0, y] = 5;
                    Game_Fields[WIDTH - 1, y] = 5;
                }
            }
            catch(Exception ex)
            {
                Logger.Error(ex.TargetSite + " " + ex.Message);
            }
        }

        // Код нужно поправить.
        private string InfoPanel(int cols)
        {
            string temp = "";
            switch(cols)
            {
                case 0:
                    temp = "###########"; break;
                case 1:
                    temp = "  Scope   #"; break;
                case 2:
                    temp = $"  {scope}";
                    while (temp.Length <= 9)
                        temp = temp + " ";
                    temp += "#"; break;
                case 3:
                    temp = "###########"; break;
                case 4:
                    temp = "  Speed   #"; break;
                case 5:
                    temp = $"  {speed}";
                    while (temp.Length <= 9)
                        temp = temp + " ";
                    temp += "#"; break;
                case 6:
                    temp = "###########"; break;
                case 7:
                    temp = "   Next   #"; break;
                case 8:
                    temp = "          #"; break;
                case 9:
                    {
                        switch(nextFigure)
                        {
                            case 0:
                                temp = "   [][]   #"; break;
                            case 1:
                                temp = " [][][][] #"; break;
                            case 2:
                                temp = "  [][]    #"; break;
                            case 3:
                                temp = "    [][]  #"; break;
                            case 4:
                                temp = "  [][][]  #"; break;
                            case 5:
                                temp = "  [][][]  #"; break;
                            case 6:
                                temp = "    []    #"; break;
                        }
                    } break;
                case 10:
                    {
                        switch (nextFigure)
                        {
                            case 0:
                                temp = "   [][]   #"; break;
                            case 1:
                                temp = "          #"; break;
                            case 2:
                                temp = "    [][]  #"; break;
                            case 3:
                                temp = "  [][]    #"; break;
                            case 4:
                                temp = "  []      #"; break;
                            case 5:
                                temp = "      []  #"; break;
                            case 6:
                                temp = "  [][][]  #"; break;
                        }
                    } break;
                case 11:
                    temp = "          #"; break;
                case 12:
                    temp = "          #"; break;
                case 13:
                    temp = "          #"; break;
                case 14:
                    temp = "          #"; break;
                case 15:
                    temp = "          #"; break;
                case 16:
                    if(isPause)
                        temp = "  Pause   #";
                    else
                        temp = "          #";
                    break;
                case 17:
                            temp = "###########"; break;
                        }
            return temp;
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
                        //Console.Write(Game_Fields[x, y]);
                    }
                    Console.WriteLine(InfoPanel(y));
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.TargetSite + " " + ex.Message);
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
                case 2:
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
            Console.SetCursorPosition(8, HEIGHT / 2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Игра завершена!");
            Console.ResetColor();
            Console.SetCursorPosition(WIDTH, HEIGHT);
        }

        private void SpeedTest(int Scope)
        {
            switch(Scope)
            {
                case int n when n >= 0 && n < 1000:
                    speed = 1; break;

                case int n when n >= 1000 && n < 2000:
                    speed = 2; break;

                case int n when n >= 3000 && n < 4000:
                    speed = 4; break;
            }
        }

        //
        private void InputPlayer()
        {
            try
            {
                Console.Write("Введите имя: ");
                player = Console.ReadLine();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.TargetSite + ex.Message);
            }
        }

        //
        public void Play()
        {
            try
            {
                ConnectDB connect = new ConnectDB("Scope.db");
                InputPlayer();
                timer.Start();
                long lastTimer = timer.ElapsedMilliseconds;
                long lastTimerScreen = timer.ElapsedMilliseconds;
                random = new Random(DateTime.Now.Millisecond);
                nextFigure = random.Next(0, 7);
                next_block = new Blocks(nextFigure);            
                do
                {
                    KeyDown();
                    if (timer.ElapsedMilliseconds - lastTimerScreen >= 50)
                    {
                        lastTimerScreen = timer.ElapsedMilliseconds;
                        if (!isBlock_Live)
                        {
                            Born_Block();
                        }
                        
                        BuildingScene();
                        
                    }
                    SpeedTest(scope);
                    if (timer.ElapsedMilliseconds - lastTimer >= (1000/speed) && !isPause)
                    {
                        lastTimer = timer.ElapsedMilliseconds;

                        MoveBlock(Move.Down);

                    }

                } while (!isExit);

                Final();

                timer.Stop();
                connect.WriteDB(player, scope);
            }
            catch(Exception ex)
            {
                Logger.Error(ex.TargetSite + ex.Message);
            }
        }
    }
}

