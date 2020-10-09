using System.Linq;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace CommandDecoder
{
    public class Decoder
    {
        public delegate void UserHandler(string message);
        public static event UserHandler PacketResponse;
        private const string _commmandPattern = "^P[T|S]:[\x20-\x7F]*:E$";

        public static bool CommandIsFull(string command)
        {
            return Regex.IsMatch(command, _commmandPattern);
        }

        public static string GetStringFromCommand(string command)
        {
            int from = command.IndexOf(':') + 1;
            int to = command.LastIndexOf(':');
            if (CommandIsFull(command)== true)
                return command.Substring(from, to - from);
            return null;
        }

        public static int[] GetArrayFromCommand(string command)
        {
            var splitValues = Regex.Split(GetStringFromCommand(command), @"[\s,]+");
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

        public static void Response(bool resp)
        {
            if (resp == true)
                PacketResponse?.Invoke("ACK Response");
            else
                PacketResponse?.Invoke("NACK Response");
        }

        public static string PureText(string str)
        {
            string text = GetStringFromCommand(str);
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
