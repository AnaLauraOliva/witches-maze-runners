using System.Reflection.PortableExecutable;
using System.Xml.Serialization;
using Game.Model;
using Game.Model.WitchesAndPlayersNamespace;
namespace Game.Visuals
{
    internal class MenuVisuals
    {
        public int? SelectNumberOfPlayers()
        {
            while (true)
            {
                Console.Clear();
                System.Console.WriteLine("Selecciona la cantidad de jugadores o presiona 7 para salir al menu principal, recuerda que son de dos a seis jugadores: ");
                if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int NumberOfPlayers)
                && NumberOfPlayers > 1 && NumberOfPlayers < 7) return NumberOfPlayers;
                else if (NumberOfPlayers == 7) return null;
                System.Console.WriteLine("Opción no válida");
                Thread.Sleep(1000);
            }
        }
        public (int,Player?) AddPlayer(bool[] AvailableCharacters)
        {
            Console.Clear();
            System.Console.WriteLine("Introduce el nombre de tu jugador");
            string? name = Console.ReadLine();
            name = name == null ? "Jugador sin nombre" : name;
            Player? player;
            while (true)
            {
                Console.Clear();
                System.Console.Write("Selección de ");
                PrintWitchesInfo();
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        player = AddPlayers(AvailableCharacters, 0, name, new Water());
                        if (player != null) return (0,player);
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        player = AddPlayers(AvailableCharacters, 1, name, new Earth());
                        if (player != null) return (1,player);
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        player = AddPlayers(AvailableCharacters, 2, name, new Fire());
                        if (player != null) return (2,player);
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        player = AddPlayers(AvailableCharacters, 3, name, new Air());
                        if (player != null) return (3,player);
                        break;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        player = AddPlayers(AvailableCharacters, 4, name, new Darkness());
                        if (player != null) return (4,player);
                        break;
                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        player = AddPlayers(AvailableCharacters, 5, name, new Light());
                        if (player != null) return (5,player);
                        break;
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        return (6,null);
                    default:
                        System.Console.WriteLine("Opción no válida");
                        Thread.Sleep(1000);
                        break;
                }
            }
        }
        private Player? AddPlayers(bool[] AvailableCharacter, int index, string name, Witches witches)
        {
            if (AvailableCharacter[index])
            {
                return new Player(name, witches);
            }
            else
            {
                System.Console.WriteLine("Esta bruja ya ha sido seleccionada");
                Thread.Sleep(1000);
            }
            return null;
        }
        public int SetDifficultyMenu()
        {
            while (true)
            {
                Console.Clear();
                PrintDifficulty();
                if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int difficulty) && difficulty > 0 && difficulty < 5) return difficulty;
                else if (difficulty == 5) break;
            }
            return 5;
        }
        private void PrintDifficulty()
        {
            System.Console.WriteLine("Selecciona la dificultad:");
            System.Console.WriteLine("   Nivel        |   Dimensión del laberinto(LargoxAncho)");
            System.Console.WriteLine("1- Pincipiante  |   21x21");
            System.Console.WriteLine("2- Normal       |   29x31");
            System.Console.WriteLine("3- Difícil      |   29x41");
            System.Console.WriteLine("4- Maestro      |   29x61");
        }
        public void Characters()
        {
            while (true)
            {
                Console.Clear();
                string[] Characters = LoadCharacters();
                PrintWitchesInfo();
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        System.Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        DisplayWitch(2, 16, Characters);
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.ForegroundColor = ConsoleColor.Green;
                        DisplayWitch(16, 30, Characters);
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.ForegroundColor = ConsoleColor.Red;
                        DisplayWitch(30, 44, Characters);
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        DisplayWitch(44, 58, Characters);
                        break;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        DisplayWitch(58, 72, Characters);
                        break;
                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        Console.ForegroundColor = ConsoleColor.White;
                        DisplayWitch(72, Characters.Length, Characters);
                        break;
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        return;
                    default:
                        break;
                }
                Console.ReadKey();
            }
        }
        private void PrintWitchesInfo()
        {
            System.Console.WriteLine("Personajes:");
            System.Console.WriteLine("1- Bruja de agua");
            System.Console.WriteLine("2- Bruja de tierra");
            System.Console.WriteLine("3- Bruja de Fuego");
            System.Console.WriteLine("4- Bruja de Aire");
            System.Console.WriteLine("5- Bruja de Oscuridad");
            System.Console.WriteLine("6- Bruja de Luz");
            System.Console.WriteLine("7- Volver al menu principal");
        }
        private void DisplayWitch(int start, int end, string[] Characters)
        {
            Console.Clear();
            Console.WriteLine(Characters[0]);
            Console.WriteLine(Characters[1]);
            for (int i = start; i < end; i++)
            {
                System.Console.WriteLine(Characters[i]);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private string[] LoadCharacters()
        {
            StreamReader reader = new StreamReader("CharactersDesciption.txt");
            string[] Characters = new string[86];
            for (int i = 0; i < Characters.Length; i++)
            {
                Characters[i] = reader.ReadLine()!;
            }
            return Characters;
        }
        public void PrintMenu()
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
    }
}