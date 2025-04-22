using Log;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class ConnectDB
    {
        private readonly string path;

        public ConnectDB(string path)
        {
            this.path = path;
        }

        public void ReadDB()
        {
            var db = new SqliteConnection($"Data Source = {path}");
            db.Open();

            var sql = $"SELECT id, player, scope FROM table_scope ORDER BY scope DESC";

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
            int it = 0;
            foreach (var item in scopes)
            {
                it++;
                Console.WriteLine($"{it}: {item.Player} {item.Scope}");
            }
        }

        public void WriteDB(string Player, int Scope)
        {
            try
            {
                var db = new SqliteConnection($"Data Source = {path}");
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
