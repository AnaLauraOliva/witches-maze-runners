using System.Reflection.PortableExecutable;
using System.Xml.Serialization;
using Game.Model;
using System;
using Game.Model.MazeNamespace;
using Game.Model.WitchesAndPlayersNamespace;
using Spectre.Console;

namespace Game.Visuals
{
  class GameVisuals
  {
    MenuVisuals menus = new MenuVisuals();
    MazeVisuals maze = new MazeVisuals();
    public int PrintMenu() => menus.PrintMenu();
    public int Difficulty() => menus.SetDifficultyMenu();
    public int? PlayersCount() => menus.SelectNumberOfPlayers();
    public (int, Player?) AddPlayer(List<(string, Witches)> witches) => menus.AddPlayer(witches);
    public void Characters() => menus.Characters();
    public void GameHistory() => maze.PrintPrologue();
    public void PrintSMS(List<string> Narration) => maze!.PrintNarration(Narration);
    public void PrintMaze(GameModel gameModel, int remainingMoves) => this.maze!.PrintMaze(gameModel, remainingMoves);
    public bool BackToMainMenu()
    {
      Console.Clear();
      int index = (int)AnsiConsole.Prompt(
         new SelectionPrompt<YesNo>()
         .Title("[lightyellow3]Está seguro de que desea salir al menú principal[/]")
         .AddChoices(YesNo.No, YesNo.Sí)
       );
       return index == 0? false : true;
    }
    enum YesNo
    {
      No,
      Sí
    }
    public void PrintWinners(List<string> Winners)
    {

      Console.Clear();
      System.Console.WriteLine("Puesto |Jugador");
      for (int i = 0; i < Winners.Count; i++)
      {
        System.Console.WriteLine($"{i + 1}      |{Winners[i]}");
      }

      var x = @"          
   ▄████████    ▄████████  ▄█        ▄█   ▄████████  ▄█  ████████▄     ▄████████ ████████▄     ▄████████    ▄████████ 
  ███    ███   ███    ███ ███       ███  ███    ███ ███  ███   ▀███   ███    ███ ███   ▀███   ███    ███   ███    ███ 
  ███    █▀    ███    █▀  ███       ███▌ ███    █▀  ███▌ ███    ███   ███    ███ ███    ███   ███    █▀    ███    █▀  
 ▄███▄▄▄      ▄███▄▄▄     ███       ███▌ ███        ███▌ ███    ███   ███    ███ ███    ███  ▄███▄▄▄       ███        
▀▀███▀▀▀     ▀▀███▀▀▀     ███       ███▌ ███        ███▌ ███    ███ ▀███████████ ███    ███ ▀▀███▀▀▀     ▀███████████ 
  ███          ███    █▄  ███       ███  ███    █▄  ███  ███    ███   ███    ███ ███    ███   ███    █▄           ███ 
  ███          ███    ███ ███▌    ▄ ███  ███    ███ ███  ███   ▄███   ███    ███ ███   ▄███   ███    ███    ▄█    ███ 
  ███          ██████████ █████▄▄██ █▀   ████████▀  █▀   ████████▀    ███    █▀  ████████▀    ██████████  ▄████████▀  
                          ▀                                                                                           
   ▄███████▄    ▄████████    ▄████████    ▄████████       ▄█        ▄██████▄     ▄████████                            
  ███    ███   ███    ███   ███    ███   ███    ███      ███       ███    ███   ███    ███                            
  ███    ███   ███    ███   ███    ███   ███    ███      ███       ███    ███   ███    █▀                             
  ███    ███   ███    ███  ▄███▄▄▄▄██▀   ███    ███      ███       ███    ███   ███                                   
▀█████████▀  ▀███████████ ▀▀███▀▀▀▀▀   ▀███████████      ███       ███    ███ ▀███████████                            
  ███          ███    ███ ▀███████████   ███    ███      ███       ███    ███          ███                            
  ███          ███    ███   ███    ███   ███    ███      ███▌    ▄ ███    ███    ▄█    ███                            
 ▄████▀        ███    █▀    ███    ███   ███    █▀       █████▄▄██  ▀██████▀   ▄████████▀                             
                            ███    ███                   ▀                                                            
   ▄██████▄     ▄████████ ███▄▄▄▄      ▄████████ ████████▄   ▄██████▄     ▄████████    ▄████████    ▄████████         
  ███    ███   ███    ███ ███▀▀▀██▄   ███    ███ ███   ▀███ ███    ███   ███    ███   ███    ███   ███    ███         
  ███    █▀    ███    ███ ███   ███   ███    ███ ███    ███ ███    ███   ███    ███   ███    █▀    ███    █▀          
 ▄███          ███    ███ ███   ███   ███    ███ ███    ███ ███    ███  ▄███▄▄▄▄██▀  ▄███▄▄▄       ███                
▀▀███ ████▄  ▀███████████ ███   ███ ▀███████████ ███    ███ ███    ███ ▀▀███▀▀▀▀▀   ▀▀███▀▀▀     ▀███████████         
  ███    ███   ███    ███ ███   ███   ███    ███ ███    ███ ███    ███ ▀███████████   ███    █▄           ███         
  ███    ███   ███    ███ ███   ███   ███    ███ ███   ▄███ ███    ███   ███    ███   ███    ███    ▄█    ███         
  ████████▀    ███    █▀   ▀█   █▀    ███    █▀  ████████▀   ▀██████▀    ███    ███   ██████████  ▄████████▀          
                                                                         ███    ███                                   
                                                                 
";
      AnsiConsole.MarkupLine("[lime]" + x + "[/]");
      System.Console.WriteLine();
      System.Console.WriteLine("Persiona cualquier tecla para volver al menu principal");
      Console.ReadKey();
    }
  }
}