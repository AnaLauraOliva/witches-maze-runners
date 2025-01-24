using Game.Model.MazeNamespace;
namespace Game.Model
{
    class Gameboard
    {
        Maze maze;
        public Gameboard(Maze maze) => this.maze = maze;
        public bool VerifyAvailability((int, int) NewPosition)
        {
            return NewPosition.Item2 >= 0 && maze.maze[NewPosition.Item1, NewPosition.Item2].Available;
        }
        public bool FallIntoTrap((int, int) NewPosition)
        {
            if (maze.maze[NewPosition.Item1, NewPosition.Item2].Traps) return true;
            return false;
        }
        public void Effect(Player currentPlayer, List<string>Narration)
        {
            (int,int) currentPosition = currentPlayer.GetPlayerPosition();
            maze.maze[currentPosition.Item1,currentPosition.Item2].FallIntoTrap(currentPlayer, Narration, this);
        }
        public int PutInitialPosition() => maze.startCell;
    }
}