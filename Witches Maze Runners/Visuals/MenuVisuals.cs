using System.Reflection.PortableExecutable;
using System.Xml.Serialization;
using Game.Model;
using Game.Model.WitchesAndPlayersNamespace;
using Spectre.Console;
namespace Game.Visuals
{
    internal class MenuVisuals
    {
        public int? SelectNumberOfPlayers()
        {
            Console.Clear();
            List<(string, int?)> options = new List<(string, int?)>() { ("2 Jugadores", 2), ("3 Jugadores", 3), ("4 Jugadores", 4), ("5 Jugadores", 5), ("6 Jugadores", 6), ("Volver al menú principal", null) };
            string NumberOfPlayers = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[gold1][underline][bold]Selecciona la cantidad de jugadores:[/][/][/]")
                .PageSize(10)
                .AddChoices(options.Select(x => x.Item1))
            );
            return options.FirstOrDefault(x => x.Item1 == NumberOfPlayers).Item2;
        }
        public (int, Player?) AddPlayer(List<(string, Witches)> witches)
        {
            Console.Clear();
            AnsiConsole.Markup("[gold1][underline][bold]Introduce el nombre de tu jugador:[/][/][/]");
            System.Console.WriteLine();
            string? name = Console.ReadLine();
            name = name == null ? "Jugador sin nombre" : name;
            Console.Clear();
            string witchName = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[gold1][underline][bold]Selección de Personajes:[/][/][/]")
                .PageSize(10)
                .AddChoices(witches.Select(x => x.Item1))
                .AddChoices("Volver al menú principal")
            );
            if(witchName == "Volver al menú principal") return (0, null);
            Witches witch = witches.FirstOrDefault(x => x.Item1 == witchName).Item2;
            return (witches.IndexOf((witchName, witch)), new Player(name, witch));
        }
        public int SetDifficultyMenu()
        {
            AnsiConsole.Markup("[gold1][underline][bold]Selecciona la dificultad:[/][/][/]");
            System.Console.WriteLine();
            System.Console.WriteLine();
            List<(string, int)> values = new List<(string, int)>()
            {
                 ("Pincipiante  |   21x21",1),
                 ("Normal       |   29x31",2),
                 ("Difícil      |   29x41",3),
                 ("Maestro      |   29x61",4),
                 ("Volver al menú principal",5)};

            string prompt = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[gold1][underline][bold]Nivel          |   Dimensión del laberinto(LargoxAncho)[/][/][/]")
                    .PageSize(10)
                    .AddChoices(values.Select(x => x.Item1))

            );
            return values.FirstOrDefault(x => x.Item1 == prompt).Item2;
        }
        public void Characters()
        {
            while (true)
            {
                Console.Clear();
                string[] Characters = LoadCharacters();
                List<(string,int)> Choices = new List<(string,int)>(){("[dodgerblue1]Bruja de Agua[/]",1), 
            ("[green3_1]Bruja de Tierra[/]",2), 
            ("[darkorange]Bruja de Fuego[/]",3),
            ("[grey70]Bruja de Aire[/]",4), 
            ("[purple_1]Bruja de Oscuridad[/]",5), 
            ("[white]Bruja de Luz[/]",6),
            ("Volver al menú principal",7)};
            string Choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[gold1][underline][bold]Personajes:[/][/][/]")
                .PageSize(10)
                .AddChoices(Choices.Select(x => x.Item1))
            );
                switch (Choices.FirstOrDefault(x=>x.Item1 == Choice).Item2)
                {
                    case 1:
                        DisplayWitch(2, 16, Characters, "[dodgerblue1]");
                        break;
                    case 2:
                        DisplayWitch(16, 30, Characters, "[green3_1]");
                        break;
                    case 3:
                        DisplayWitch(30, 44, Characters, "[darkorange]");
                        break;
                    case 4:
                        DisplayWitch(44, 58, Characters, "[grey70]");
                        break;
                    case 5:
                        DisplayWitch(58, 72, Characters, "[purple_1]");
                        break;
                    case 6:
                        DisplayWitch(72, Characters.Length, Characters, "[white]");
                        break;
                    case 7:
                        return;
                    default:
                        break;
                }
                Console.ReadKey();
            }
        }
        private void DisplayWitch(int start, int end, string[] Characters, string Color)
        {
            Console.Clear();
            AnsiConsole.Markup("[gold1][underline][bold]" + Characters[0] + "[/][/][/]");
            Console.WriteLine();
            Console.WriteLine();
            for (int i = start; i < end; i++)
            {
                AnsiConsole.Markup(Color + Characters[i] + "[/]");
                System.Console.WriteLine();
            }
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
        public int PrintMenu()
        {
            var x = @"
          ██╗    ██╗██╗████████╗ ██████╗██╗  ██╗███████╗███████╗              
▄ ██╗▄    ██║    ██║██║╚══██╔══╝██╔════╝██║  ██║██╔════╝██╔════╝              
 ████╗    ██║ █╗ ██║██║   ██║   ██║     ███████║█████╗  ███████╗              
▀╚██╔▀    ██║███╗██║██║   ██║   ██║     ██╔══██║██╔══╝  ╚════██║              
  ╚═╝     ╚███╔███╔╝██║   ██║   ╚██████╗██║  ██║███████╗███████║              
           ╚══╝╚══╝ ╚═╝   ╚═╝    ╚═════╝╚═╝  ╚═╝╚══════╝╚══════╝              
                                                                              
                ███╗   ███╗ █████╗ ███████╗███████╗                           
                ████╗ ████║██╔══██╗╚══███╔╝██╔════╝                           
                ██╔████╔██║███████║  ███╔╝ █████╗                             
                ██║╚██╔╝██║██╔══██║ ███╔╝  ██╔══╝                             
                ██║ ╚═╝ ██║██║  ██║███████╗███████╗                           
                ╚═╝     ╚═╝╚═╝  ╚═╝╚══════╝╚══════╝                           
                                                                              
       ██████╗ ██╗   ██╗███╗   ██╗███╗   ██╗███████╗██████╗ ███████╗          
       ██╔══██╗██║   ██║████╗  ██║████╗  ██║██╔════╝██╔══██╗██╔════╝    ▄ ██╗▄
       ██████╔╝██║   ██║██╔██╗ ██║██╔██╗ ██║█████╗  ██████╔╝███████╗     ████╗
       ██╔══██╗██║   ██║██║╚██╗██║██║╚██╗██║██╔══╝  ██╔══██╗╚════██║    ▀╚██╔▀
       ██║  ██║╚██████╔╝██║ ╚████║██║ ╚████║███████╗██║  ██║███████║      ╚═╝ 
       ╚═╝  ╚═╝ ╚═════╝ ╚═╝  ╚═══╝╚═╝  ╚═══╝╚══════╝╚═╝  ╚═╝╚══════╝          
                                                                              
";
            List<(string, int)> menu = new List<(string, int)> { ("[chartreuse1]Nuevo Juego[/]", 1), ("[aqua]Personajes[/]", 2), ("[red]Salir[/]", 0) };
            string MenuDisplay = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title($"[gold1]{x}[/]")
                .PageSize(10)
                .AddChoices(menu.Select(x => x.Item1))
                
            );
            return menu.FirstOrDefault(x => x.Item1 == MenuDisplay).Item2;
        }
    }
}