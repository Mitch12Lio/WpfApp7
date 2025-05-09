using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp7.Utilities
{
    enum CipherTypes
    {
        Atbash, Ceasar, Morse, Pic, PigPen, Playfair, Polybius
    }

    enum EggColours
    {
        Purple, Blue, Green, Red, Yellow,Orange
    }

    enum VisibilityTypes
    {
       Collapsed,Visible,Hidden
    }

    struct LocationTypes
    {
        public const string MainLevel = "Mail Level";
        public const string Upstairs = "Upstairs";
        public const string Basement = "Basement";
        public const string Garage = "Garage";
        public const string Attic = "Attic";
        public const string Frontyard = "Front Yard";
        public const string Backyard = "Back Yard";
        public const string Other = "Other";

    }
}
