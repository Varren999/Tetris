using Log;
using ConsoleMenu;
using System.Collections.Generic;
using System;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main()
        {
            Logger.System("Начало работы приложения");
            Tetris game = new Tetris();
            ConnectDB connect = new ConnectDB("Scope.db");
            Menu menu = new Menu("Тетрис", new string[] { "Играть", "Управление", "Статистика"}, new List<Menu.delFunction> { game.Play, Test, connect.ReadDB});
            menu.Start();
            Logger.System("Завершение работы приложения");

        }

        // Не придумал норм названия для этого метода.
        static void Test()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("Для управления фигурой используйте кнопки A, D, курсор Вправо, курсор Влево");
            System.Console.WriteLine("Для быстрого движения фигуры вниз используйте кнопки S или курсор Вниз");
            System.Console.WriteLine("Для ротации фигуры используйте кнопки W или Пробел");
            System.Console.WriteLine("Чтобы поставить игру на паузу нажмите кнопку P");
            System.Console.WriteLine("Чтобы выйти из игры нажмите кнопку ESCAPE");
            Console.ResetColor();
        } 
    }
}
