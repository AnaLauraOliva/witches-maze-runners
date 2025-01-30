using Game.Model;
using Game.Model.MazeNamespace;
using Game.Model.WitchesAndPlayersNamespace;
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
                int index = gameVisuals.PrintMenu();
                switch (index)
                {
                    case 1:
                        newGame();
                        break;

                    case 2:
                        gameVisuals.Characters();
                        break;
                    case 0:
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
                    Print(remainingMoves);
                    bool? MoveCode = MoveOrSkill(remainingMoves);
                    if(MoveCode == null) return;
                    else if (MoveCode == true) remainingMoves--;
                    else { remainingMoves = 0; }
                    if (gameModel.GameWin())
                    {
                        Winners.Add(gameModel.DeletePlayer());
                        remainingMoves = gameModel!.GetSpeed();
                        continue;
                    }
                    if(gameModel.GetSpeed() ==3 && remainingMoves >= gameModel.GetSpeed()) remainingMoves = 2;
                    else if (remainingMoves == 0 || remainingMoves >= gameModel.GetSpeed())
                    {
                        gameModel.ReduceEffects();
                        gameModel.NextTurn();
                        if (gameModel.GetCurrentTurn() == 0) { firstTurn = false; }
                        remainingMoves = gameModel!.GetSpeed();
                    }

                    if (gameModel.Narration.Count >= 3) gameModel.Narration.Remove(gameModel.Narration[0]);
                }
                gameVisuals.PrintWinners(Winners);
            }
        }
        private void Print(int remainingMoves)
        {

            gameVisuals.PrintMaze(gameModel!, remainingMoves);
            gameVisuals.PrintSMS(gameModel!.Narration);
        }
        private bool? MoveOrSkill(int remainingMoves)
        {
            if (gameModel!.HasEffects())
            {

                (int, int)[] Directions = { (-1, 0), (1, 0), (0, -1), (0, 1) };
                while (true)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.A:
                        case ConsoleKey.LeftArrow:
                            if (!gameModel.MakeAValidMove(Directions[2]))
                                continue;
                            break;
                        case ConsoleKey.D:
                        case ConsoleKey.RightArrow:
                            if (!gameModel.MakeAValidMove(Directions[3]))
                                continue;
                            break;
                        case ConsoleKey.W:
                        case ConsoleKey.UpArrow:
                            if (!gameModel.MakeAValidMove(Directions[0]))
                                continue;
                            break;
                        case ConsoleKey.S:
                        case ConsoleKey.DownArrow:
                            if (!gameModel.MakeAValidMove(Directions[1]))
                                continue;
                            break;
                        case ConsoleKey.L:
                            gameModel.Attack();

                            Print(remainingMoves);
                            continue;
                        case ConsoleKey.K:
                            gameModel.Defense();
                            remainingMoves = remainingMoves>3?3: remainingMoves;
                            Print(remainingMoves);
                            continue;
                            case ConsoleKey.Escape:
                            if(gameVisuals.BackToMainMenu())
                            return null;
                            Print(remainingMoves);
                            continue;
                        default:
                            continue;
                    }
                    return true;
                }
            }
            return false;
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
            List<(string,Witches)> witches = new List<(string,Witches)>(){("[dodgerblue1]Bruja de Agua[/]",new Water()), 
            ("[green3_1]Bruja de Tierra[/]",new Earth()), 
            ("[darkorange]Bruja de Fuego[/]",new Fire()),
            ("[grey70]Bruja de Aire[/]",new Air()), 
            ("[purple_1]Bruja de Oscuridad[/]",new Darkness()), 
            ("[white]Bruja de Luz[/]",new Light())};
            int index;
            this.NumberOfPlayers = (int)NumberOfPlayers;
            for (int i = 0; i < NumberOfPlayers; i++)
            {
                (index, Player? player) = gameVisuals.AddPlayer(witches);
                if (player == null) return false;
                witches.Remove(witches[index]);
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