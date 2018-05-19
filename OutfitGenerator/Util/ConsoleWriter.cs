using System;

namespace OutfitGenerator
{
    /// <summary>
    /// Simple helper class to write colored text to the standard output.
    /// </summary>
    public class ConsoleWriter
    {
        public ConsoleColor DefaultColor { get; set; } = ConsoleColor.Cyan;

        public ConsoleWriter() { }

        public ConsoleWriter(ConsoleColor defaultColor)
        {
            DefaultColor = defaultColor;
        }

        public void Write(string str, params object[] args)
        {
            WriteColored(Console.Write, DefaultColor, str, args);
        }

        public void Write(ConsoleColor color, string str, params object[] args)
        {
            WriteColored(Console.Write, color, str, args);
        }

        public void WriteLine(string str, params object[] args)
        {
            WriteColored(Console.WriteLine, DefaultColor, str, args);
        }

        public void WriteLine(ConsoleColor color, string str, params object[] args)
        {
            WriteColored(Console.WriteLine, color, str, args);
        }

        private static void WriteColored(Action<string, object[]> writeMethod, ConsoleColor color, string str, params object[] args)
        {
            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = color;
            writeMethod(str, args);

            Console.ResetColor();
            Console.ForegroundColor = old;
        }
    }
}
