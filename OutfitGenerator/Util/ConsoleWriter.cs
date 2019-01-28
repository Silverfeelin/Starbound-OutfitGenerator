using System;
using System.Text.RegularExpressions;

namespace OutfitGenerator.Util
{
    /// <summary>
    /// Simple helper class to write colored text to the standard output.
    /// </summary>
    public class ConsoleWriter
    {
        public ConsoleColor ResetColor { get; set; } = Console.ForegroundColor;
        public ConsoleColor DefaultColor { get; set; } = ConsoleColor.Cyan;

        public ConsoleWriter()
        {
            DefaultColor = ResetColor;
            Console.ResetColor();
        }

        public ConsoleWriter(ConsoleColor defaultColor)
        {
            DefaultColor = defaultColor;
            Console.ForegroundColor = DefaultColor;
        }

        /// <summary>
        /// <para>
        /// $lCyan$kGreen$0Default\$Escaped
        /// </para>
        /// Black(a) DarkBlue(b) DarkGreen(c) DarkCyan(d) DarkRed(e) DarkMagenta(f) DarkYellow(g) Gray(h)
        /// DarkGray(i) Blue(j) Green(k) Cyan(l) Red(m) Magenta(n) Yellow(o) White(p)
        /// </summary>
        public void WriteColorful(string str)
        {
            if (!str.StartsWith('$')) str = "$0" + str;
            var split = Regex.Split(str, "(?<!\\\\)\\$");
            ConsoleColor old = Console.ForegroundColor;
            foreach (string s in split)
            {
                var part = s.Replace("\\$", "$");
                if (string.IsNullOrEmpty(part)) continue;
                var c = part[0];
                Write(ResolveColor(c), part.Substring(1));
            }
            Console.ForegroundColor = old;
        }

        /// <summary>
        /// <para>
        /// $lCyan$kGreen$0Default\$Escaped
        /// </para>
        /// Black(a) DarkBlue(b) DarkGreen(c) DarkCyan(d) DarkRed(e) DarkMagenta(f) DarkYellow(g) Gray(h)
        /// DarkGray(i) Blue(j) Green(k) Cyan(l) Red(m) Magenta(n) Yellow(o) White(p)
        /// </summary>
        public void WriteColorfulLine(string str)
        {
            WriteColorful(str);
            Console.WriteLine();
        }

        /// <summary>
        /// Black(a) DarkBlue(b) DarkGreen(c) DarkCyan(d) DarkRed(e) DarkMagenta(f) DarkYellow(g) Gray(h)
        /// DarkGray(i) Blue(j) Green(k) Cyan(l) Red(m) Magenta(n) Yellow(o) White(p)
        /// </summary>
        private ConsoleColor ResolveColor(char c)
        {
            int i = c - 'a';
            return Enum.IsDefined(typeof(ConsoleColor), i) ? (ConsoleColor)i : DefaultColor;
        }

        public void Write(string str, params object[] args) => WriteColored(Console.Write, DefaultColor, str, args);

        public void Write(ConsoleColor color, string str, params object[] args) => WriteColored(Console.Write, color, str, args);

        public void WriteLine(string str, params object[] args) => WriteColored(Console.WriteLine, DefaultColor, str, args);

        public void WriteLine(ConsoleColor color, string str, params object[] args) => WriteColored(Console.WriteLine, color, str, args);

        private static void WriteColored(Action<string, object[]> writeMethod, ConsoleColor color, string str, params object[] args)
        {
            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = color;
            writeMethod(str, args);
            Console.ForegroundColor = old;
        }
    }
}
