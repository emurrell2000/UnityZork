using System;

namespace Zork
{
    internal class ConsoleOutputService : IOutputService
    {
        public void Clear()
        {
            Console.Clear();
        }

        public void Write(object value)
        {
            Console.Write(value);
        }

        public void WriteLine(object value)
        {
            Console.WriteLine(value);
        }
    }
}
