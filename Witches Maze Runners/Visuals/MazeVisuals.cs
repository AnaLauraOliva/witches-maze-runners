using Game.Model;
using Game.Model.MazeNamespace;
using Spectre.Console;
namespace Game.Visuals
{
    internal class MazeVisuals
    {
        internal void PrintPrologue()
        {
            System.Console.Write(" Para terminar su entrenamiento mágico y lograr ser miembros oficiales del aquelarre, las brujas y brujos que acaban de recibir la marca deben ");
            System.Console.Write("embarcarse en la aventura del sendero de las brujas (camino al infierno) y salir con vida del mismo.");
            Thread.Sleep(1500);
            System.Console.Write(" Este sendero no es más que un laberinto lleno de");
            System.Console.Write(" trampas que pondrá a prueba las habilidades de los recién marcados.");
            Thread.Sleep(1500);
            System.Console.Write(" Antes de comenzar todos son advertidos de que si pierden todos sus puntos de vida tendrán que esperar dos ");
            System.Console.Write("turnos y tendrán que volver al inicio y que solo la mitad podrá salir de ahí, el resto tendrá que intentarlo otro año.");
            Thread.Sleep(1500);
            System.Console.Write(" Tu misión, es ayudar a tu brujo o ");
            System.Console.WriteLine("bruja a ganar. ¡Buena suerte!");
            System.Console.Write("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }
        internal void PrintMaze(GameModel gameModel, int remainingMoves)
        {
            Console.Clear();
            Cells[,] maze = gameModel.GetMaze();
            List<Player> players = gameModel.GetPlayers();
            string[,] Gameboard = GetGameboard(maze, players, gameModel.GetCurrentTurn() % players.Count);
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    AnsiConsole.Markup(Gameboard[i, j]);
                }
                System.Console.WriteLine();
            }
            AnsiConsole.MarkupLine("[lightyellow3]" + players[gameModel.GetCurrentTurn() % players.Count].ToString() + "[/]");
            AnsiConsole.MarkupLine($"[lightyellow3]Cantidad de movimientos restantes {remainingMoves}[/]");
            AnsiConsole.MarkupLine("[lightyellow3]Presiona escape para volver al menú principal[/]");

        }
        private string[,] GetGameboard(Cells[,] maze, List<Player> players, int currentPlayer)
        {
            string[,] gameboard = new string[maze.GetLength(0), maze.GetLength(1)];
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    gameboard[i, j] = maze[i, j].Traps ? ":star:" : maze[i, j].Available ? "  " : ":white_square_button:";
                }
            }
            for (int i = 1; i <= players.Count; i++)
            {
                (int, int) playerPosition = players[(currentPlayer + players.Count - i) % players.Count].GetPlayerPosition();
                if (playerPosition != (0, 0))
                    gameboard[playerPosition.Item1, playerPosition.Item2] = GetWitchSymbol(players[(currentPlayer + players.Count - i) % players.Count]);
            }
            return gameboard;
        }
        private string GetWitchSymbol(Player player)
        {
            switch (player.Witch.WitchType)
            {
                case "Bruja de Fuego":
                    return "🧡";
                case "Bruja de Oscuridad":
                    return "🖤";
                case "Bruja de Luz":
                    return "🤍";
                case "Bruja de Aire":
                    return "🩶";
                case "Bruja de Agua":
                    return "💙";
                case "Bruja de Tierra":
                    return "🤎";
            }
            return "";
        }
        public void PrintNarration(List<string> Narration)
        {
            AnsiConsole.MarkupLine("[lightyellow3]Mensajes:[/]");
            for (int i = 0; i < Narration.Count; i++)
            {
                AnsiConsole.MarkupLine("[lightyellow3]" + Narration[i] + "[/]");
            }
        }

    }
}