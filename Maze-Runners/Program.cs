using System.Text;
using System;
namespace Maze_Runners;

class Program
{
    static void Main(string[] args)
    {


        while (true)
        {
            Console.Clear();
            PrintMenu();
            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.N:
                case ConsoleKey.Enter:
                    NewGame();

                    break;

                case ConsoleKey.P:

                    break;

                case ConsoleKey.Escape:
                case ConsoleKey.S:
                    return;

                default:
                    break;
            }
        }

    }
    static void PrintMenu()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        System.Console.WriteLine("--Nanatsu no Taizai Maze Runners-- \n");
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        System.Console.WriteLine("[N]uevo Juego");
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        System.Console.WriteLine("[P]ersonajes");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        System.Console.WriteLine("[S]alir");
        Console.ForegroundColor = ConsoleColor.Gray;
        System.Console.WriteLine("Presiona una tecla para continuar...");
    }
    static void Lore()
    {
        Console.Clear();
        StringBuilder sb1 = new StringBuilder();
        sb1.AppendLine("Como parte de uno de sus locos entrenamientos los \"Siete Pecados Capitales\" fueron a un lugar llamado \"El Camino Al Purgatorio\", un laberinto del cual dicen");
        sb1.AppendLine("que nadie ha podido salir. Para hacer el reto más divertido, Meliodas sugirió que podían atacarse entre sí, mas no mortalmente, y que solamente usaran dos");
        sb1.AppendLine("habilidades: una de defensa y otra de ataque. Todos aceptaron y se fueron por su lado no sin antes ser advertidos de que en el laberinto habían trampas.");
        System.Console.Write(sb1);
        Console.ReadKey();
        Console.Clear();
    }
    const int Width = 21;
    const int Height = 21;
    static int[,] maze = new int[Width, Height];
    static void NewGame()
    {
        Console.Clear();
        Lore();

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                maze[i, j] = 1;
            }
        }
        int a = rdm.Next(1, Width - 2);
        int b = rdm.Next(1, Height - 2);
        maze[a, b] = 0;
        GenerateMaze(a, b);
        StartFinish(0,1);
        StartFinish(20,19);
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                System.Console.Write(maze[x, y] == 1 ? "⬜" : "⬛");
            }
            System.Console.WriteLine();
        }
        System.Console.WriteLine();
        System.Console.ReadLine();
    }
    static Random rdm = new Random();
    static void GenerateMaze(int x, int y)
    {

        int[,] directions = new int[,]
        {
            {0,2},//Up
            {0,-2},// Down
            {2,0},//Right
            {-2,0}//Left
        };
        //Randomizar direcciones
        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int j = rdm.Next(directions.GetLength(0));
            var temp = directions[i, 0];
            directions[i, 0] = directions[j, 0];
            directions[j, 0] = temp;

            temp = directions[i, 1];
            directions[i, 1] = directions[j, 1];
            directions[j, 1] = temp;
        }
        //Cavar caminos
        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int nx = x + directions[i, 0];
            int ny = y + directions[i, 1];
            if (nx > 0 && nx < Width && ny > 0 && ny < Height && maze[nx, ny] == 1)
            {
                maze[nx, ny] = 0;
                maze[x + directions[i, 0] / 2, y + directions[i, 1] / 2] = 0;
                GenerateMaze(nx, ny);
            }

        }
    }
    static void StartFinish(int a, int b)
    {
        int x = rdm.Next(1, Height - 1);
        
            
            if (maze[b, x] == 0)
            {
                maze[a, x] = 0;
                
            }
        
    }
}
