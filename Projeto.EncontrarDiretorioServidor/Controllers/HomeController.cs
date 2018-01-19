using Projeto.EncontrarDiretorioServidor.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.EncontrarDiretorioServidor.Controllers
{
    public class HomeController : Controller
    {
        private static string _diretorioRaiz;

        public HomeController()
        {
            _diretorioRaiz = ConfigurationManager.AppSettings["DiretorioRaiz"]?.ToString();
        }

        // GET: Home
        public ActionResult Index()
        {
            var model = new DiretorioModel();
            return View(model);
        }

        [HttpGet, ActionName("FolderDialog")]
        public ActionResult FindDirectoryDialog()
        {
            return RedirectToAction("FindDirectoryDialog", new { raiz = ConfigurationManager.AppSettings["DiretorioRaiz"]?.ToString(), diretorio = " " });
        }

        [HttpPost]
        public ActionResult OpenFile(DiretorioModel model)
        {
            System.Diagnostics.Process.Start(model.CaminhoDiretorio);
            return RedirectToAction("Index");
        }

        public PartialViewResult FindDirectoryDialog(string raiz, string diretorio)
        {
            DiretorioModel model = ObterConteudoDiretorio(CombinaPath(raiz, diretorio));
            return PartialView(model);
        }

        private static DiretorioModel ObterConteudoDiretorio(string caminhoDiretorio)
        {
            var diretorio = new DiretorioModel
            {
                CaminhoRaiz = _diretorioRaiz,
                CaminhoDiretorio = caminhoDiretorio,
                CaminhoDiretorioPai = Directory.GetParent(caminhoDiretorio).ToString()
            };

            var diretorioInfo = new DirectoryInfo(diretorio.CaminhoDiretorio);

            diretorioInfo.GetDirectories().ToList().ForEach(d =>
            {
                diretorio.Diretorios.Add(new Diretorio
                {
                    CaminhoCompleto = d.FullName,
                    Nome = d.Name,
                    Extensao = d.Extension,
                    Icone = "fa fa-folder-o fa-2x",
                    Tamanho = null
                });
            });

            diretorioInfo.GetFiles().ToList().ForEach(f =>
            {
                diretorio.Arquivos.Add(new Arquivo
                {
                    CaminhoCompleto = f.FullName,
                    Nome = f.Name,
                    Extensao = f.Extension,
                    Icone = RetornaIcone(f.Extension),
                    Tamanho = f.Length
                });
            });

            return diretorio;
        }

        private static string RetornaIcone(string extension)
        {
            switch (extension)
            {
                case ".zip":
                case ".rar":
                case ".7zip":
                    return "fa fa-file-archive-o fa-2x";
                case ".doc":
                case ".docx":
                    return "fa fa-file-word-o fa-2x";
                case ".html":
                case ".dll":
                    return "fa fa-file-code-o fa-2x";
                case ".png":
                case ".jpeg":
                case ".bmp":
                    return "fa fa-file-image-o fa-2x";
                case ".pdf":
                    return "fa fa-file-pdf-o fa-2x";
                case ".mpeg":
                case ".mp4":
                case ".avi":
                    return "fa fa-file-video-o fa-2x";
                case ".mp3":
                    return "fa fa-file-audio-o fa-2x";
                case ".xls":
                case ".xlsx":
                    return "fa fa-file-excel-o fa-2x";
                case ".txt":
                    return "fa fa-file-text-o fa-2x";
                default:
                    return "fa fa-file-o fa-2x";
            }
        }

        private static string CombinaPath(string path1, string path2)
        {
            if (string.IsNullOrWhiteSpace(path1) && string.IsNullOrWhiteSpace(path2))
                return "";

            if (string.IsNullOrWhiteSpace(path2))
                return path1.Trim();

            return Path.Combine(path1, path2);
        }
    }
}