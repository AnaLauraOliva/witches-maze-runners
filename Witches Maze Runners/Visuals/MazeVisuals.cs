using Game.Model;
using Game.Model.MazeNamespace;
namespace Game.Visuals
{
    abstract class MazeVisuals
    {
        internal void PrintPrologue()
        {
            System.Console.Write(" Para terminar su entrenamiento mágico y lograr ser miembros oficiales del aquelarre, las brujas y brujos que acaban de recibir la marca deben ");
            System.Console.Write("embarcarse en la aventura del sendero de las brujas (camino al infierno) y salir con vida del mismo.");
            Thread.Sleep(3500);
            System.Console.Write(" Este sendero no es más que un laberinto lleno de");
            System.Console.Write(" trampas que pondrá a prueba las habilidades de los recién marcados.");
            Thread.Sleep(3500);
            System.Console.Write(" Antes de comenzar todos son advertidos de que si pierden todos sus puntos de vida tendrán que esperar dos ");
            System.Console.Write("turnos y tendrán que volver al inicio y que solo la mitad podrá salir de ahí, el resto tendrá que intentarlo otro año.");
            Thread.Sleep(3500);
            System.Console.Write(" Tu misión, es ayudar a tu brujo o ");
            System.Console.WriteLine("bruja a ganar. ¡Buena suerte!");
            System.Console.Write("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }
        internal abstract void PrintMaze(GameModel gameModel);
        
        
        public void PrintNarration(List<string>Narration)
        {
            System.Console.WriteLine("Mensajes:");
            for (int i = 0; i < Narration.Count; i++)
            {
                System.Console.WriteLine(Narration[i]);
            }
        }
        
    }
}