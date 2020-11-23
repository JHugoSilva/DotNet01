using System.Collections.Generic;
using System.Web.Mvc;
using TecWeb.Models;

namespace TecWeb.Controllers
{
    public class DisciplinaController : Controller
    {
        // GET: Disciplina
        [HttpGet]
        public ActionResult Index(int idAluno, string nomeAluno)
        {
            ViewBag.idAluno = idAluno;
            ViewBag.NomeAluno = nomeAluno;
            List<Disciplina> disciplinas = Disciplina.listarDisciplinasPeloAluno(idAluno);
            return View(disciplinas);
        }

        [HttpPost]
        public ActionResult index(int idAluno, int idDisciplina)
        {
            string resultado = Disciplina.excluirVinculoDisciplinaAluno(idAluno, idDisciplina);
            List<Disciplina> disciplinas = Disciplina.listarDisciplinasPeloAluno(idAluno);
            return View(disciplinas);
        }
       public ActionResult disciplinas() 
       {
            List<Disciplina> disciplina = Disciplina.listarDisciplinas();
            return View(disciplina); 
       } 
        public ActionResult novaDisciplina()
        {
            return View();
        }
        [HttpPost]
        public ActionResult novaDisciplina(string Nome, string Semestre, string Curso) 
        {
            string sucesso = Disciplina.novaDisciplina(Nome, Semestre, Curso);
            ViewBag.sucesso = sucesso;
            return View(); 
        }
    }
}