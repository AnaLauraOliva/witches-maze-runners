using System.Text;
using System;
using System.IO;
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
                        Characters();
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

            System.Console.WriteLine("--Witches Maze Runners--\n");
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
            sb1.AppendLine("Para terminar su entrenamiento mágico y lograr ser miembros oficiales del aquelarre, las brujas y brujos que acaban de recibir la marca deben embarcarse en");
            sb1.AppendLine("la aventura del sendero de las brujas (camino al infierno) y salir con vida del mismo. Este sendero no es más que un laberinto lleno de trampas que pondrán");
            sb1.AppendLine("a prueba las habilidades de los recién marcados. Antes de comenzar todos son advertidos de que si pierden todos sus puntos de vida tendrán que esperar dos");
            sb1.AppendLine("turnos y tendrán que volver al inicio y que solo puede haber un ganador y que el resto tendrá que intentarlo otro año. Tu misión, es ayudar a tu brujo o");
            sb1.AppendLine("bruja a ganar. ¡Buena suerte!");
            System.Console.Write(sb1);
            Console.ReadKey();
            Console.Clear();
        }
        static void Characters()
        {
            Console.Clear();
            string[] Characters = LoadCharacters();
            for (int i = 0; i < Characters.Length; i++)
            {
                if (i == 2) Console.ForegroundColor = ConsoleColor.Blue;
                else if (i == 16) Console.ForegroundColor = ConsoleColor.Green;
                else if (i == 30) Console.ForegroundColor = ConsoleColor.Red;
                else if (i == 44) Console.ForegroundColor = ConsoleColor.Cyan;
                else if (i == 58) Console.ForegroundColor = ConsoleColor.DarkMagenta;
                else if (i == 72) Console.ForegroundColor = ConsoleColor.White;
                System.Console.WriteLine(Characters[i]);
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ReadLine();
        }
        static string[] LoadCharacters()
        {
            StreamReader reader = new StreamReader("CharactersDesciption.txt");
            string[] Characters = new string[86];
            for (int i = 0; i < Characters.Length; i++)
            {
                Characters[i] = reader.ReadLine()!;
            }
            return Characters;
        }
        static void NewGame()
        {
            Console.Clear();
            int playersCount = SelectNumberOfPlayer();
            Player[] players = SelectCharacters(playersCount);
            Lore();
            Maze maze = new Maze(21, 21);
            maze.GetMaze();
            int counter = 0;
            int currentPlayer = 0;
            while (true)
            {
                if (counter < players.Length)
                {
                    Console.Clear();
                    players[counter].InitializePlayer(maze);
                    maze.PrintMaze(players);
                    players[counter].PlayerInfo(players[counter].speed);
                    players[counter].Turn(players, maze);
                    counter++;
                    continue;
                }
                Console.Clear();
                currentPlayer = currentPlayer == players.Length ? 0 : currentPlayer;
                maze.PrintMaze(players);
                players[currentPlayer].PlayerInfo(players[currentPlayer].speed);
                if ( players[currentPlayer].Turn(players, maze)== true) break;
                currentPlayer++;

            }
            GameWinner(players[currentPlayer]);
            Console.ReadKey();
        }
        static void GameWinner(Player winner)
        {
            Console.Clear();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Felicidades {winner.name}, has logrado pasar la prueba final, ahora eres un miembro oficial del aquelarre. Para el resto, más suerte para la próxima");
            System.Console.WriteLine(sb);
        }
        static void PrintCharactersMenu()
        {
            System.Console.WriteLine("Selección de personajes:\n");
            System.Console.WriteLine("1-Bruja de Agua");
            System.Console.WriteLine("2-Bruja de Tierra");
            System.Console.WriteLine("3-Bruja de Fuego");
            System.Console.WriteLine("4-Bruja de Aire");
            System.Console.WriteLine("5-Bruja de Oscuridad");
            System.Console.WriteLine("6-Bruja de Luz\n");
            System.Console.WriteLine("Presiona uno de estos números para seleccionar a tu personaje y luego presiona enter para confirmar tu selección");
        }
        static int SelectNumberOfPlayer()
        {
            int playersCount = 0;
            while (playersCount == 0)
            {
                Console.Clear();
                System.Console.WriteLine("Selecciona un número del 2 al 6 para elegir la cantidad de jugadores");
                int option = Convert.ToInt32(Console.ReadLine());
                if (option < 7)
                {
                    playersCount = option;
                    break;
                }
                System.Console.WriteLine("Opción no disponible, presione cualquier tecla para intentarlo otra vez");
                Console.ReadKey();
            }

            return playersCount;
        }
        static Player[] SelectCharacters(int charactersCount)
        {
            Player[] players = new Player[charactersCount];
            for (int i = 0; i < players.Length; i++)
            {
                Console.Clear();
                PrintCharactersMenu();

                int newCharacterIndex = Convert.ToInt32(Console.ReadLine());
                if (newCharacterIndex <= 0 || newCharacterIndex > 6) { i--; continue; }
                players[i] = new Player(newCharacterIndex);
                if (CharacterAvailable(players, i)==false)
                {
                    i--;
                    System.Console.WriteLine("Ese personaje ya he sido seleccionado, presiona enter para intentarlo de nuevo");
                    Console.ReadLine();
                }
            }
            return players;
        }
        static bool CharacterAvailable(Player[] players, int currentPosition)
        {
            for (int i = 0; i < currentPosition; i++)
            {
                if (players[i].name == players[currentPosition].name)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
