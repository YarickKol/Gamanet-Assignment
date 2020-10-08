using System.Text.RegularExpressions;

namespace CommandDecoder
{
    public class Decoder
    {
        private const string _commmandPattern = "^P[T|S]:[\x20-\x7F]*:E$";

        public static bool CommandIsFull(string command)
        {
            return Regex.IsMatch(command, _commmandPattern);
        }
    }
}
