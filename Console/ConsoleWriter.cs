using System;

namespace ConsoleIO
{
    public class ConsoleWriter
    {
        public static void WriteLine(string message, ConsoleColor color)
        {
            Console.Clear();
            Console.ForegroundColor = color;
            WriteLine(message);
            Console.ResetColor();
        }

        public static void Write(string message, ConsoleColor color)
        {
            Console.Clear();
            Console.ForegroundColor = color;
            Write(message);
            Console.ResetColor();
        }

        public static void WriteLine(string message)
        {
            Console.WriteLine();
            Write(message);
        }

        public static void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}