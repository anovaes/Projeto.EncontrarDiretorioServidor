using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.EncontrarDiretorioServidor.Models
{
    public class Item
    {
        public string CaminhoCompleto { get; set; }
        public string Nome { get; set; }
        public DateTime DataModificacao { get; set; }
        public string Extensao { get; set; }
        public string Icone { get; set; }
        public long? Tamanho { get; set; }
    }
}