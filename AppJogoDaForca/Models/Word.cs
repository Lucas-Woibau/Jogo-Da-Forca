using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJogoDaForca.Models
{
    public class Word
    {
        public string Tips { get; set; } = string.Empty;
        public string Text { get; set; }= string.Empty;

        public Word(string tips, string text)
        {
            Tips = tips;
            Text = text;
        }
    }
}
