using System.Text;
using System;
using System.Security.Cryptography.X509Certificates;
namespace Maze_Runners
{
    class Maze
    {
        public int Width;
        public int Height;
        public int[,] maze;
        public int positionx = 0;
        public int positiony;
        public int end;
        Random rdm = new Random();
        public Maze(int width, int height)
        {
            Width = width;
            Height = height;
            maze = new int[Width, Height];
        }
        public void GetMaze()
        {
            InitializateMaze();
            int a = GetRandom(Width), b = GetRandom(Height);
            maze[a, b] = 0;
            GenerateMaze(a, b);
            positiony = EntryExit(1, 0);
            end = EntryExit(Width - 2, Width - 1);
            maze[positionx, positiony] = 2;
            PrintMaze();
        }
        private int GetRandom(int max)
        {
            int n = rdm.Next(1, max);
            return n % 2 == 0 ? n - 1 : n;
        }
        private void InitializateMaze()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    maze[x, y] = 1;
                }
            }
        }
        private void GenerateMaze(int x, int y)
        {
            int[,] directions =
            {
                {0,-2},//Up
                {0,2},//Down
                {2,0},//Right
                {-2,0}//Left
            };

            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int j = rdm.Next(directions.GetLength(0));

                int temp = directions[i, 0];
                directions[i, 0] = directions[j, 0];
                directions[j, 0] = temp;

                temp = directions[i, 1];
                directions[i, 1] = directions[j, 1];
                directions[j, 1] = temp;
            }

            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int xn = x + directions[i, 0];
                int yn = y + directions[i, 1];
                if (xn > 0 && xn < Width && yn > 0 && yn < Height && maze[xn, yn] == 1)
                {
                    maze[xn, yn] = 0;
                    maze[x + directions[i, 0] / 2, y + directions[i, 1] / 2] = 0;
                    GenerateMaze(xn, yn);
                }

            }

        }
        private int EntryExit(int tocheck, int position)
        {
            int y = rdm.Next(1, Height);
            while (true)
            {
                if (y == 0) y = Height - 2;
                if (maze[tocheck, y] == 0)
                {
                    maze[position, y] = 0;
                    break;
                }
                y--;
            }
            return y;
        }
        private void PrintMaze()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    System.Console.Write(maze[x, y] == 1 ? "⬜" : maze[x, y] == 0 ? "⬛" : "🟣");
                }
                System.Console.WriteLine();
            }
        }
        private void Directions(bool condition, int x, int y)
        {
            if (condition && maze[positionx + x, positiony + y] == 0)
            {
                maze[positionx + x, positiony + y] = 2;
                maze[positionx, positiony] = 0;
                positionx += x;
                positiony += y;
            }
        }
        public void PlayerMove()
        {


            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                Directions( key == ConsoleKey.RightArrow,1, 0);
                Directions( key == ConsoleKey.LeftArrow && positionx > 0,-1, 0);
                Directions( key == ConsoleKey.UpArrow, 0, -1);
                Directions(key == ConsoleKey.DownArrow, 0, 1);
                Console.Clear();
                PrintMaze();
                if (maze[Width - 1, end] == 2)
                {
                    break;
                }
            }
            Console.WriteLine("Ganaste");
            Console.ReadLine();
        }
    }
}
