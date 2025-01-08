using Game.Model;
using Game.Model.MazeNamespace;
using Game.Visuals;
namespace Game.Contoller
{
    class GameController
    {
        private int NumberOfPlayers;
        private GameModel? gameModel = null;
        private GameVisuals gameVisuals = new GameVisuals();
        public void Start()
        {
            while (true)
            {
                Console.Clear();
                gameVisuals.PrintMenu();
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.N:
                    case ConsoleKey.Enter:
                        newGame();
                        break;

                    case ConsoleKey.P:
                        gameVisuals.Characters();
                        break;
                    case ConsoleKey.Escape:
                    case ConsoleKey.S:
                        Console.Clear();

                        return;
                    default:
                        break;
                }
            }

        }
        private void newGame()
        {
            if (DifficultySelection() && CharactersSelection(gameVisuals.PlayersCount()))
            {
                Console.Clear();
                gameVisuals.GameHistory();
                List<string> Winners = new List<string>();
                int remainingMoves = gameModel!.GetSpeed();
                bool firstTurn = true;
                while (Winners.Count != NumberOfPlayers / 2)
                {
                    if (firstTurn && remainingMoves == gameModel!.GetSpeed())
                        gameModel.IniPlayer();
                    gameVisuals.PrintMaze(gameModel);
                    System.Console.WriteLine($"Movimientos restantes: {remainingMoves}");
                    gameVisuals.PrintSMS(gameModel.Narration);
                    int MoveCode = gameModel.MoveOrHability();
                    if (MoveCode == 1) remainingMoves--;
                    else if (MoveCode == 0)
                    { remainingMoves = 0; }
                    if (gameModel.GameWin())
                    {
                        Winners.Add(gameModel.DeletePlayer());
                        remainingMoves = gameModel!.GetSpeed();
                        continue;
                    }
                    if (remainingMoves == 0|| remainingMoves >= gameModel.GetSpeed())
                    {
                        gameModel.NextTurn();
                        if(gameModel.GetCurrentTurn() == 0) {firstTurn = false;}
                        remainingMoves = gameModel!.GetSpeed();
                    }

                    if (gameModel.Narration.Count >= 3) gameModel.Narration.Remove(gameModel.Narration[0]);
                }
                gameVisuals.PrintWinners(Winners);
            }
        }
        private bool DifficultySelection()
        {
            int difficulty = gameVisuals.Difficulty();
            if (difficulty != 5)
            {
                (int a, int b, int c) = SetDifficulty(difficulty - 1);
                gameModel = new GameModel(a, b, c);
                return true;
            }
            return false;
        }

        private bool CharactersSelection(int? NumberOfPlayers)
        {
            if (NumberOfPlayers == null) return false;
            bool[] AvailableCharacter = { true, true, true, true, true, true };
            int index;
            this.NumberOfPlayers = (int)NumberOfPlayers;
            for (int i = 0; i < NumberOfPlayers; i++)
            {
                (index, Player? player) = gameVisuals.AddPlayer(AvailableCharacter);
                if (player == null) return false;
                AvailableCharacter[index] = false;
                gameModel!.AddCharacter(player);
            }
            return true;
        }
        private (int, int, int) SetDifficulty(int difficulty)
        {
            (int, int, int)[] values = { (21, 21, 4), (29, 31, 6), (29, 41, 8), (29, 61, 10) };
            return values[difficulty];
        }

    }
}