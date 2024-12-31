using System.Runtime.Intrinsics.Arm;

namespace Game.Model.MazeNamespace
{
    internal class Maze
    {
        public int startCell { get; private set; }
        public int endCell { get; private set; }
        public Cells[,] maze { get; private set; }
        public Maze(int Rows, int Cols, int trapsNumber)
        {
            maze = new Cells[Rows, Cols];
            IniMaze();
            GenerateMaze(GetRandom(Rows - 2), GetRandom(Cols - 2));
            startCell = EntranceExit(0, 1, (0, maze.GetLength(0) / 2));
            endCell = EntranceExit(Cols - 1, Cols - 2, (maze.GetLength(0) / 2, maze.GetLength(0)));
            for (int i = 0; i < 6; i++)
            {
                PutTrapOrTresure(i, trapsNumber);
            }
        }
        private void IniMaze()
        {
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    maze[i, j] = new Cells();
                }
            }
        }
        private int EntranceExit(int Fcolumn, int Scolumn, (int, int) position)
        {
            Random rdm = new Random();
            int EntryExit = rdm.Next(position.Item1, position.Item2);
            if (maze[EntryExit, Scolumn].Available == true)
            {
                maze[EntryExit, Fcolumn].MakeAvailable();
                return EntryExit;
            }
            return EntranceExit(Fcolumn, Scolumn, position);
        }
        private int GetRandom(int max)
        {
            Random random = new Random();
            int rdm = random.Next(1, max);
            return rdm % 2 == 0 ? rdm - 1 : rdm;
        }
        private void GenerateMaze(int IRow, int ICol)
        {
            Stack<(int, int)> Stack = new Stack<(int, int)>();
            maze[IRow, ICol].MakeAvailable();
            Stack.Push((IRow, ICol));
            while (Stack.Count != 0)
            {
                (int, int) Neighbours = GetNeighbors(Stack.Peek().Item1, Stack.Peek().Item2);
                if (Neighbours == (0, 0)) { Stack.Pop(); continue; }
                else
                {
                    maze[Neighbours.Item1, Neighbours.Item2].MakeAvailable();
                    maze[(Stack.Peek().Item1 + Neighbours.Item1) / 2, (Stack.Peek().Item2 + Neighbours.Item2) / 2].MakeAvailable();
                    Stack.Push(Neighbours);
                }
            }
        }
        private (int, int) GetNeighbors(int row, int col)
        {
            List<(int, int)> Neighbours = new List<(int, int)>();
            (int, int)[] Directions = { (0, 2), (0, -2), (-2, 0), (2, 0) };
            for (int i = 0; i < Directions.Length; i++)
            {
                if (Conditions(row + Directions[i].Item1, col + Directions[i].Item2))
                {
                    Neighbours.Add((row + Directions[i].Item1, col + Directions[i].Item2));
                }
            }
            Random rdm = new Random();
            return Neighbours.Count == 0 ? (0, 0) : Neighbours[rdm.Next(Neighbours.Count)];
        }
        private bool Conditions(int IRow, int ICol)
        {
            return IRow > 0 && ICol > 0 && ICol < maze.GetLength(1)
            && IRow < maze.GetLength(0) && maze[IRow, ICol].Available == false;
        }
        private void PutTrapOrTresure(int code, int remaining)
        {
            Random rdm = new Random();
            for (int traps = 0; traps <= remaining; traps++)
            {
                while (true)
                {
                    int rows = rdm.Next(1, maze.GetLength(0) - 1);
                    int cols = rdm.Next(3, maze.GetLength(1) - 3);
                    if (maze[rows, cols].Available && maze[rows, cols].Traps == false)
                    {
                        maze[rows, cols].PutTrapOrTreasure(code);
                        break;
                    }
                }
            }
        }
    }
}