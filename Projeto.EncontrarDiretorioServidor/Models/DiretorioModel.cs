using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.EncontrarDiretorioServidor.Models
{
    public class DiretorioModel
    {
        public string CaminhoRaiz { get; set; }
        public string CaminhoDiretorio { get; set; }
        public string CaminhoDiretorioPai { get; set; }
        public List<Diretorio> Diretorios { get; set; }
        public List<Arquivo> Arquivos { get; set; }

        public DiretorioModel()
        {
            Diretorios = new List<Diretorio>();
            Arquivos = new List<Arquivo>();
        }
    }
}