using Log;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main()
        {
            Logger.System("Начало работы приложения");
            Tetris game = new Tetris();
            game.Play();
            Logger.System("Завершение работы приложения");
        }
    }
}
