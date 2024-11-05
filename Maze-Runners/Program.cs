using System.Text;
using System;
namespace Maze_Runners
{
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
       

        static void NewGame()
        {
            Console.Clear();
            Lore();
            Maze maze = new Maze(21,21);
            maze.GetMaze();
            maze.PlayerMove();
            Console.ReadKey();
        }
        
        
    }
}
