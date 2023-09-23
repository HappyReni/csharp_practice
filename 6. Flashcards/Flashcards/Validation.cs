
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
                    return false;
                }
            }
            return true;
        }
    }
}
