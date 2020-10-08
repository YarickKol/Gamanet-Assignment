using System;
using CommandDecoder;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the command: ");
            string command = ReadCommand();

            Console.ReadLine();
        }

        private static string ReadCommand()
        {
            string command = string.Empty;
            while (Console.KeyAvailable == false)
            {
                command += Console.ReadKey().KeyChar;
                if (Decoder.CommandIsFull(command))
                    break;
            }
            Console.Write('\n');
            return command;
        }
    }
}
