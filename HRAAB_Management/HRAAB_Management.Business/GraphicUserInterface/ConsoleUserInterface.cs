using HRAAB_Management.Business.Abstractions.Interfaces;

namespace HRAAB_Management.Business.GraphicUserInterface
{
    public class ConsoleUserInterface : IGraphicUserInterface
    {
        public void ClearScreen()
        {
            Console.Clear();
        }

        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        public string? ReadString()
        {
            return Console.ReadLine();
        }
    }
}
