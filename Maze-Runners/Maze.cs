using System.Text;
using System;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.DataAnnotations.Schema;
namespace Maze_Runners
{
    class Maze
    {
        enum Traps
        {
            teleport = -1,
            frost = -2,
            lowSpeed = -4,
            damage = -5,
        }
        public int Width;
        public int Height;
        public int[,] maze;
        public int start;
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
            start = EntryExit(1, 0, rdm.Next(0, Height / 2));
            end = EntryExit(Width - 2, Width - 1, rdm.Next(Height / 2, Height - 1));
            int[] trapsCodes = { (int)Traps.teleport, (int)Traps.frost, (int)Traps.lowSpeed, (int)Traps.damage};
            for (int i = 0; i < trapsCodes.Length; i++)
            {
                GenerateTraps(6, trapsCodes[i]);
            }
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
        private void GenerateTraps(int trapsLeft, int trapCode)
        {
            int x = rdm.Next(2, 17);
            int y = rdm.Next(2, 17);
            for (int i = 0; i < trapsLeft; i++)
            {

                while (maze[x, y] != 0)
                {
                    x = rdm.Next(2, 17);
                    y = rdm.Next(2, 17);
                }
                maze[x, y] = trapCode;
            }
        }
        private int EntryExit(int tocheck, int position, int y)
        {
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
        public void PrintMaze(Player[] players)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    System.Console.Write(maze[x, y] == 1 ? "⬜" : maze[x, y] >= 2 ? GetPiece(players, maze[x, y]) : "⬛");
                }
                System.Console.WriteLine();
            }
        }
        private string GetPiece(Player[] players, int code)
        {

            int count = 0;
            while (players[count].code != code)
            {
                count++;
            }
            return players[count].piece;
        }
        public bool Directions(bool condition, int x, int y, Player currentPlayer, Player[] players)
        {
            if (condition && maze[currentPlayer.positionx + x, currentPlayer.positiony + y] != 1)
            {
                maze[currentPlayer.positionx, currentPlayer.positiony] = currentPlayer.SamePosition(players, this);
                if (maze[currentPlayer.positionx + x, currentPlayer.positiony + y] == (int)Traps.teleport)
                {
                    currentPlayer.InitializePlayer(this);
                    return true;
                }
                maze[currentPlayer.positionx + x, currentPlayer.positiony + y] = currentPlayer.code;
                currentPlayer.positionx += x;
                currentPlayer.positiony += y;
                FallIntoTrap(currentPlayer, maze[currentPlayer.positionx + x, currentPlayer.positiony + y]);
                return true;
            }
            return false;
        }
        private void FallIntoTrap(Player currentPlayer, int trapValue)
        {
            if (trapValue == (int)Traps.frost)
            {
                if (currentPlayer.defense.defrost == true) currentPlayer.defense.defrost = false;
                else currentPlayer.frostTimeLeft = 3;
            }
            else if (trapValue == (int)Traps.lowSpeed)
            {
                currentPlayer.speed = 1;
                currentPlayer.lowSpeedTimeLeft = 3;
            }
            else if (trapValue == (int)Traps.damage)
            {
                currentPlayer.hp = currentPlayer.hp / 2;
            }
        }

    }
}
