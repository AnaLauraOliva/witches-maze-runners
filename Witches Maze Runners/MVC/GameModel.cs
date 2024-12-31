using System.Dynamic;
using Game.Model.MazeNamespace;

namespace Game.Model
{
    class GameModel
    {
        private List<Player> players;
        public List<string> Narration;
        private Maze maze;
        private Gameboard gameboard;
        private int CurrentTurn;
        public GameModel(int Rows, int Cols, int Traps)
        {
            players = new List<Player>();
            Narration = new List<string>();
            CurrentTurn = 0;
            maze = new Maze(Rows, Cols, Traps);
            gameboard = new Gameboard(maze);
        }
        public Cells[,] GetMaze() => maze.maze;
        public void IniPlayer() => players[CurrentTurn % players.Count].UpdatePlayerPosition(gameboard.PutInitialPosition(), 0);
        private void IniPlayer(int index) => players[index].UpdatePlayerPosition(gameboard.PutInitialPosition(), 0);
        public int MoveOrHability()
        {
            if (players[CurrentTurn % players.Count].Effects())
            {

                (int, int)[] Directions = { (-1, 0), (1, 0), (0, -1), (0, 1) };
                while (true)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.A:
                        case ConsoleKey.LeftArrow:
                            if (!MakeAValidMove(Directions[2]))
                                continue;
                            break;
                        case ConsoleKey.D:
                        case ConsoleKey.RightArrow:
                            if (!MakeAValidMove(Directions[3]))
                                continue;
                            break;
                        case ConsoleKey.W:
                        case ConsoleKey.UpArrow:
                            if (!MakeAValidMove(Directions[0]))
                                continue;
                            break;
                        case ConsoleKey.S:
                        case ConsoleKey.DownArrow:
                            if (!MakeAValidMove(Directions[1]))
                                continue;
                            break;
                        case ConsoleKey.L:
                            Narration.Add(players[CurrentTurn % players.Count].Witch.AttackSkill!);
                            players[CurrentTurn % players.Count].Attack(players);
                            for (int i = 0; i < players.Count; i++)
                            {
                                if (players[i].PlayerDeath(Narration))
                                {
                                    IniPlayer(i);
                                }
                            }
                            return 2;
                        case ConsoleKey.K:
                            players[CurrentTurn % players.Count].Defense();
                            Narration.Add(players[CurrentTurn % players.Count].Witch.DefenseSkill!);
                            return 2;
                        default:
                            continue;
                    }

                    return 1;
                }
            }

            players[CurrentTurn % players.Count].ReduceEffects();
            return 0;
        }
        public bool GameWin() => players[CurrentTurn % players.Count].GetPlayerPosition().Item1==maze.endCell && players[CurrentTurn % players.Count].GetPlayerPosition().Item2 == maze.maze.GetLength(1) - 1;
        public string DeletePlayer()
        {
            string name = players[CurrentTurn % players.Count].Name;
            players.Remove(players[CurrentTurn % players.Count]);
            return name;
        }
        private bool MakeAValidMove((int, int) Directions)
        {
            (int, int) PlayerPosition = players[CurrentTurn % players.Count].GetPlayerPosition();
            PlayerPosition.Item1 += Directions.Item1;
            PlayerPosition.Item2 += Directions.Item2;
            if (gameboard.VerifyAvailability(PlayerPosition))
            {

                players[CurrentTurn % players.Count].ReduceEffects();
                players[CurrentTurn % players.Count].UpdatePlayerPosition(PlayerPosition.Item1, PlayerPosition.Item2);
                if (gameboard.FallIntoTrap(PlayerPosition))
                {
                    if (!players[CurrentTurn % players.Count].ConvertToEffect(gameboard.Effect(PlayerPosition), Narration))
                    {
                        Narration.Add($"{players[CurrentTurn % players.Count].Name} ha caído en una trampa de teletransportación");
                        IniPlayer();
                    }
                }
                if (players[CurrentTurn % players.Count].PlayerDeath(Narration)) IniPlayer();

                return true;
            }
            return false;
        }
        public void AddCharacter(Player player) => players.Add(player);
        public int GetCurrentTurn() => CurrentTurn;
        public int GetSpeed() => players[CurrentTurn % players.Count].Witch.Speed;
        public string GetCurrentPlayer() => players[CurrentTurn % players.Count].ToString();
        public void NextTurn() => CurrentTurn++;
        public List<Player> GetPlayers() => players;
    }
}