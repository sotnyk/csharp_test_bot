using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csharp_test_bot
{
    public static class CharsFlipper
    {
        private const string Original    = "abcdefghijklmnopqrstuvwxyz.,;!?\'абвгдеёжзийклмнопрстуфхцчшщьъэя";
        private const string Transformed = "ɐqɔpǝɟƃɥıɾʞlɯuodbɹsʇnʌʍxʎz˙\'؛¡¿,ɐgʚL6ǝǝжɛииʞvwноudɔшʎфхпhmmqqєʁ";

        private static Dictionary<char, char> _transformations;

        static CharsFlipper()
        {
            _transformations = new Dictionary<char, char>(Original.Length);
            for (int i = 0; i < Original.Length; ++i)
            {
                _transformations.Add(Original[i], Transformed[i]);
            }
        }

        public static string Flip(string str)
        {
            if (string.IsNullOrEmpty(str)) return "";

            StringBuilder res = new StringBuilder(str.Length);
            foreach(char c in str.ToLowerInvariant().Reverse())
            {
                if (_transformations.ContainsKey(c))
                    res.Append(_transformations[c]);
                else
                    res.Append(c);
            }
            return res.ToString();
        }
    }
}
