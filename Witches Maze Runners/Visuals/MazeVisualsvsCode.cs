using Game.Model;
using Game.Model.MazeNamespace;
namespace Game.Visuals
{
    internal class MazeVisualsvsCode: MazeVisuals
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
                    System.Console.Write(Gameboard[i,j]);
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
                    gameboard[i,j]=maze[i,j].Traps?"⭐":maze[i,j].Available?"⬛":"⬜";
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
    }
}