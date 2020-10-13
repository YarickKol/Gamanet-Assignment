using System;
using CommandDecoder;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Decoder.PacketResponse += ShowResponse; //event subscription
            ExecuteCommands();

            Console.ReadLine();
        }

        /// <summary>
        /// Represents method which assemble all commands execution
        /// </summary>
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

        /// <summary>
        /// Reads the values typed by the user and check completeness of the set
        /// </summary>
        /// <returns> command typed by user </returns>
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
        
        /// <summary>
        /// Realization of Sound method specified in assignment
        /// </summary>
        /// <param name="command"></param>
        private static void Sound(string command)
        {
            int[] values = Decoder.GetArrayFromCommand(command);
            if (values != null)
                Console.Beep(values[0], values[1]);
            //Console.Beep(Decoder.FetchSound(command).Item1, Decoder.FetchSound(command).Item2);
        }

        /// <summary>
        /// Realization of Text method specified in assignment
        /// </summary>
        /// <param name="command"></param>
        private static void Text(string command)
        {
            Console.WriteLine(Decoder.PureText(command));
        }

        /// <summary>
        /// Method that the delegate signs to show the response of packets
        /// </summary>
        /// <param name="message"></param>
        private static void ShowResponse(string message)
        {
            Console.WriteLine(message);
        }
    }
}
