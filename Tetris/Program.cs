using Log;
using ConsoleMenu;
using System.Collections.Generic;
using System;
using Microsoft.Data.Sqlite;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main()
        {
            Logger.System("Начало работы приложения");
            Tetris game = new Tetris();
            Menu menu = new Menu("Тетрис", new string[] { "Играть", "Управление", "Статистика"}, new List<Menu.delFunction> { game.Play, Test, ReadDB});
            menu.Start();
            //WriteDB(game.player, game.scope);
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

        static void ReadDB()
        {
            var db = new SqliteConnection(@"Data Source = \Scope.db");
            db.Open();

            var sql = "SELECT * FROM table_scope";

            var command = new SqliteCommand(sql, db);

            var reader = command.ExecuteReader();

            if (!reader.HasRows) throw new Exception("Таблица пуста");

            var scopes = new List<Scopes>();

            while (reader.Read())
            {
                var scope = new Scopes()
                {
                    Id = reader.GetInt32(0),
                    Player = reader.GetString(1),
                    Scope = reader.GetInt32(2)
                };
                scopes.Add(scope);
            }

            db.Close();

            foreach (var item in scopes)
            {
                Console.WriteLine($"{item.Id}: {item.Player} {item.Scope}");
            }
        }

        static void WriteDB(string Player, int Scope)
        {
            try
            {
                var db = new SqliteConnection(@"Data Source = C:\Users\varre\source\repos\Tetris\Tetris\Scope.db");
                db.Open();

                var sql = $"INSERT INTO table_scope (player, scope) VALUES ('{Player}', {Scope})";

                var command = new SqliteCommand(sql, db);

                var reader = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.TargetSite + ex.Message);
            }
        }
    }
}
