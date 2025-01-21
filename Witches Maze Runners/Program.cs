using Game.Contoller;
using Spectre.Console;
namespace WitchesMazeRunners
{
    class Program
    {
        static void Main(string[] args)
        {
            
           GameController controller = new GameController();
            controller.Start();
        }

    }
}