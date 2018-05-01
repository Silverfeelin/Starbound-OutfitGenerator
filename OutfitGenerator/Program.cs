using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OutfitGenerator
{
    class Program
    {
        private static ConsoleWriter writer;

        [STAThread]
        static void Main(string[] args)
        {
            writer = new ConsoleWriter(ConsoleColor.Cyan);
            writer.WriteLine("= Outfit Generator");
            Console.WriteLine();

            writer.WriteLine("Select the desired generator:");

            Type generatorType = null;
            switch (args.Length)
            {
                case 0:
                    WaitAndExit("Please drag and drop a valid image on the application (not this window).");
                    break;
                case 1:
                    generatorType = SelectGenerator(typeof(PantsGenerator), typeof(SleeveGenerator), typeof(BackGenerator));
                    break;
                case 2:
                    generatorType = SelectGenerator(typeof(ChestPantsMerger), typeof(SleevesMerger));
                    break;
                default:
                    WaitAndExit($"Invalid number of parameters: {args.Length}.");
                    break;
            }

            //create the instance of class using System.Activator class 
            object obj = Activator.CreateInstance(generatorType);
            object[] parameters = new object[] { args };

            MethodInfo process = generatorType.GetMethod("Generate");
            process.Invoke(obj, parameters);
        }

        private static Type SelectGenerator(params Type[] typeNames)
        {
            for (int i = 0; i < typeNames.Length; i++)
            {
                writer.WriteLine($"[{i + 1}]: {typeNames[i].Name}");
            }

            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);

                if (char.IsNumber(cki.KeyChar))
                {
                    int choice = (cki.KeyChar - '0');

                    if (--choice < typeNames.Length)
                        return typeNames[choice];
                }
            }
        }

        public static void WaitAndExit(string message = null, params object[] args)
        {
            if (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine(message, args);
            }
            writer.WriteLine("Press any key to exit...");

            Console.ReadKey(true);

            Environment.Exit(0);
        }
    }
}
