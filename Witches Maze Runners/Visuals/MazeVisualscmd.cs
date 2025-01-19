using Game.Model;
using Game.Model.MazeNamespace;
namespace Game.Visuals
{
    internal class MazeVisualscmd: MazeVisuals
    {
        
        internal override void PrintMaze(GameModel gameModel)
        {
            Console.Clear();
            Cells[,] maze = gameModel.GetMaze();
            List<Player> players = gameModel.GetPlayers();
            string[,] Gameboard = GetGameboard(maze, players,gameModel.GetCurrentTurn()%players.Count);
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    switch(Gameboard[i,j])
                    {
                        case "**":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                        case "FF":
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;
                        case "DD":
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        break;
                        case "LL":
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                        case "AA":
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        break;
                        case "WW":
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        break;
                        case "EE":
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        break;
                        default:
                        break;
                    }
                    System.Console.Write(Gameboard[i,j]);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                System.Console.WriteLine();
            }
           System.Console.WriteLine(players[gameModel.GetCurrentTurn()%players.Count].ToString());
        }
        private string[,] GetGameboard(Cells[,] maze, List<Player> players, int currentPlayer)
        {
            string[,] gameboard = new string[maze.GetLength(0), maze.GetLength(1)];
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    gameboard[i,j]=maze[i,j].Traps?"**":maze[i,j].Available?"  ":"||";
                }
            }
            for (int i = 1; i <= players.Count; i++)
            {
                (int,int) playerPosition =players[(currentPlayer+players.Count-i)%players.Count].GetPlayerPosition();
                if(playerPosition != (0,0))
                gameboard[playerPosition.Item1,playerPosition.Item2]=GetWitchSymbol(players[(currentPlayer+players.Count-i)%players.Count]);
            }
            return gameboard;
        }
        private string GetWitchSymbol(Player player)
        {
            switch (player.Witch.WitchType)
            {
                case "Bruja de Fuego":
                    return "FF";
                case "Bruja de Oscuridad":
                    return "DD";
                case "Bruja de Luz":
                    return "LL";
                case "Bruja de Aire":
                    return "AA";
                case "Bruja de Agua":
                    return "WW";
                case "Bruja de Tierra":
                    return "EE";
            }
            return "";
        }
    }
}