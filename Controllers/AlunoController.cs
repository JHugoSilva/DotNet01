using System.Collections.Generic;
using System.Web.Mvc;
using TecWeb.Models;

namespace TecWeb.Controllers
{
    public class AlunoController : Controller
    {
        // GET: Aluno
        public ActionResult Index()
        {
            List<Aluno> alunos = Aluno.listarAlunos();
            return View(alunos);
        }
    }
}