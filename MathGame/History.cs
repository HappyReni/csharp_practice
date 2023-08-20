using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGame
{
    internal class History
    {
        public History()
        {
        }

        public DateTime Time { get; set; }
        public String Question { get; set; }
        public int CorrectAnswer { get; set; }

        public int Answer { get; set; }
        public string Result { get; set; }

    }
}
