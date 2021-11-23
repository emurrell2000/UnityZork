using System;

namespace Zork
{
    internal class ConsoleInputService : IInputService
    {
        public event EventHandler<string> InputRecieved;

        public void ProcessInput()
        {
            string inputString = Console.ReadLine().Trim().ToUpper();
            if (string.IsNullOrWhiteSpace(inputString) == false)
            {
                InputRecieved?.Invoke(this, inputString);
            }
        }
    }
}
