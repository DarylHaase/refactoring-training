using System;

namespace ConsoleIO
{
    public static class ConsoleReader
    {
        public static string ReadLine()
        {
            return Console.ReadLine();
        }

        public static string Prompt(string message)
        {
            ConsoleWriter.Write(message);
            return ReadLine();
        }

        public static int PromptInteger(string message)
        {
            return Convert.ToInt32(Prompt(message));
        }
    }
}
