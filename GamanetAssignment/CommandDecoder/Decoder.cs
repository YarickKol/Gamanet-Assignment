using System.Linq;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace CommandDecoder
{
    public class Decoder
    {
        public delegate void UserHandler(string message);
        public static event UserHandler PacketResponse;
        private const string _commmandPattern = "^P[T|S]:[\x20-\x7F]*:E$"; //pattern of commands
        // private static int _frequencyParameter = 0, _durationParameter = 1;

        /// <summary>
        /// Checks the spelling of the command
        /// </summary>
        /// <param name="command"></param>
        /// <returns>true if the regular expression finds a match; otherwise, false</returns>
        public static bool CommandIsFull(string command)
        {
            return Regex.IsMatch(command, _commmandPattern);
        }

        /// <summary>
        /// Gets value from command between two collon characters 
        /// </summary>
        /// <param name="command"></param>
        /// <returns>if command full returns value from command; otherwise, null</returns>
        public static string GetStringFromCommand(string command)
        {
            int from = command.IndexOf(':') + 1;
            int to = command.LastIndexOf(':');
            if (CommandIsFull(command)== true)
                return command.Substring(from, to - from);
            return null;
        }

        /// <summary>
        /// Gets array of integers from command for Console.Beep in Sound Method 
        /// </summary>
        /// <param name="command"></param>
        /// <returns>if characters in array are number type return array; otherwise, null</returns>
        public static int[] GetArrayFromCommand(string command)
        {
            var splitValues = Regex.Split(GetStringFromCommand(command), @"[,]+");
            if (splitValues.Length == 2 && splitValues.All(e => { return Regex.IsMatch(e, @"\d+"); }) == true)
            {
                int[] values = new int[splitValues.Length];
                for (int i = 0; i < splitValues.Length; i++)
                {
                    values[i] = int.Parse(splitValues[i]);
                }
                Response(true);
                return values;
            }
            else
            {
                Response(false);
                return null;
            }
        }

        //public static (int, int) FetchSound(string command)
        //{
        //    string text = GetStringFromCommand(command);
        //    Match match = Regex.Match(text, @"\d+");
        //    return (int.Parse(match.Groups[_frequencyParameter].Value), int.Parse(match.Groups[_durationParameter].Value));
        //}

        /// <summary>
        /// Checks the accepted or invalid packets
        /// </summary>
        /// <param name="resp"></param>
        private static void Response(bool resp)
        {
            if (resp == true)
                PacketResponse?.Invoke("ACK Response");
            else
                PacketResponse?.Invoke("NACK Response");
        }

        /// <summary>
        /// Gets text from command for Text Method 
        /// </summary>
        /// <param name="command"></param>
        /// <returns>if text not null return text; otherwise, null</returns>
        public static string PureText(string command)
        {
            string text = GetStringFromCommand(command);
            if (text != null)
            {
                Response(true);
                return text;
            }
            else
            {
                Response(false);
                return null;
            }
        }
    }
}
