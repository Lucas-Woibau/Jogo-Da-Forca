using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJogoDaForca.Libraries.Text
{
    public static class StringExtension
    {
        public static List<int> GetPositions(this string str, string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("A string não pode ser vazia", "value");
            }
            List<int> indexes = new List<int>();

            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }
    }
}
