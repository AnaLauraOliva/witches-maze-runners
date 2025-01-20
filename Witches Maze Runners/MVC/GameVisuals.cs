using System.Reflection.PortableExecutable;
using System.Xml.Serialization;
using Game.Model;
using Game.Model.MazeNamespace;
using Game.Model.WitchesAndPlayersNamespace;

namespace Game.Visuals
{
    class GameVisuals
    {
        MenuVisuals menus = new MenuVisuals();
        MazeVisuals maze = new MazeVisuals();
        private void PrintSelectVisualMenu()
        {
            System.Console.WriteLine("¿Está jugando en el cmd de Windows o en la terminal de vsCode?\n(o en algo similar a ellos)");
            System.Console.WriteLine("1- vsCode\n2- cmd");
        }
        public int PrintMenu() => menus.PrintMenu();
        public int Difficulty() => menus.SetDifficultyMenu();
        public int? PlayersCount() => menus.SelectNumberOfPlayers();
        public (int, Player?) AddPlayer(List<(string, Witches)> witches) => menus.AddPlayer(witches);
        public void Characters() => menus.Characters();
        public void GameHistory() => maze.PrintPrologue();
        public void PrintSMS(List<string> Narration) => maze!.PrintNarration(Narration);
        public void PrintMaze(GameModel gameModel, int remainingMoves) => this.maze!.PrintMaze(gameModel, remainingMoves);
        public void PrintWinners(List<string> Winners)
        {

            Console.Clear();
            System.Console.WriteLine("Puesto |Jugador");
            for (int i = 0; i < Winners.Count; i++)
            {
                System.Console.WriteLine($"{i + 1}     |{Winners[i]}");
            }
            System.Console.WriteLine("Felicidades para los ganadores :)");
            System.Console.WriteLine();
            System.Console.WriteLine("Persiona cualquier tecla para volver al menu principal");
            Console.ReadKey();
        }
    }
}