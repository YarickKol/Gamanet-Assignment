using System;
using CommandDecoder;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Decoder.PacketResponse += ShowResponse;
            ExecuteCommands();

            Console.ReadLine();
        }
        private static void ExecuteCommands()
        {
            Console.Write("Enter the command: ");
            string command = ReadCommand();
            if (command.StartsWith("PT"))
            {
                Text(command);
            }
            else if (command.StartsWith("PS"))
            {
                Sound(command);
            }
            ExecuteCommands();
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
        
        private static void Sound(string command)
        {
            int[] values = Decoder.GetArrayFromCommand(command);
            Console.Beep(values[0],values[1]);
        }

        private static void Text(string command)
        {
            Console.WriteLine(Decoder.PureText(command));
        }

        private static void ShowResponse(string message)
        {
            Console.WriteLine(message);
        }
    }
}
