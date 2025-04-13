namespace HRAAB_Management.Business.Abstractions.Interfaces
{
    public interface IGraphicUserInterface
    {
        /// <summary>
        /// Clears the screen.
        /// </summary>
        void ClearScreen();

        /// <summary>
        /// Prints a message to the screen.
        /// </summary>
        /// <param name="message"></param>
        void PrintMessage(string message);

        /// <summary>
        /// Reads a line of input from the user.
        /// </summary>
        /// <returns></returns>
        string? ReadString();
    }
}
