using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarCipherWPF
{
    struct AdvLetter
    {
        public char OldLetter { get; set; }
        public char NewLetter { get; set; }
        public int Index { get; set; }

        public AdvLetter(char old, char newest, int ind)
        {
            OldLetter = old;
            NewLetter = newest;
            Index = ind;
        }

        public override string ToString()
        {
            return $"{OldLetter} = {NewLetter}";
        }
    }
}
