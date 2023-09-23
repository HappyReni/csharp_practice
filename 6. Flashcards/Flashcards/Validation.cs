
using System.Globalization;
using System.Text.RegularExpressions;

namespace Flashcards
{
    internal static class Validation
    {
        public static bool IsUniqueStackName(string name,List<string> stackNames)
        {
            foreach(var _name in stackNames)
            {
                if (_name == name)
                {
                    throw new Exception("Not an unique name.");
                }
            }
            return false;
        }
        public static bool IsValidStackName(string name, Dictionary<string, Stack> Stacks)
        {
            if (!Stacks.ContainsKey(name)) throw new Exception("Not a valid name.");
            else return true;
        }
    }
}
