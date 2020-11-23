using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;



namespace TecWeb.Models
{
    public class Disciplina
    {
        public int IdDisciplina { get; set; }
        public int IdAluno { get; set; }
        public string Nome { get; set; }
        public string Semestre { get; set; }
        public string Curso { get; set; }
        public List<Aluno> Alunos { get; set; }

        public Disciplina(int idDisciplina, int idAluno, string nome, string semestre, string curso) {
            IdDisciplina = idDisciplina;
            IdAluno = idAluno;
            Nome = nome;
            Semestre = semestre;
            Curso = curso;
        }
        public Disciplina(int idDisciplina, string nome, string semestre, string curso)
        {
            IdDisciplina = idDisciplina;
            Nome = nome;
            Semestre = semestre;
            Curso = curso;
            Alunos = Aluno.listarAlunosPelaDisciplina(idDisciplina);
        }
        public static List<Disciplina> listarDisciplinasPeloAluno(int idAluno) 
        {
            var disciplina = new List<Disciplina>();

            SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["minhaConexao"].ConnectionString);

            conexao.Open();

            string sql = "SELECT * FROM Disciplina " +
                "INNER JOIN AlunoDisciplina ON Disciplina.IdDisciplina=AlunoDisciplina.idDisciplina " +
                "WHERE AlunoDisciplina.idAluno=" + idAluno;
            SqlCommand sqlCommand = new SqlCommand(sql, conexao);
            SqlDataReader sqlRead = sqlCommand.ExecuteReader();

            while (sqlRead.Read())
            {
                disciplina.Add(new Disciplina(int.Parse(sqlRead["idDisciplina"].ToString()),
                    idAluno,
                    sqlRead["Nome"].ToString(),
                    sqlRead["Semestre"].ToString(),
                    sqlRead["Curso"].ToString()));
            }
            return disciplina;
        }

        public static List<Disciplina> listarDisciplinas()
        {
            var disciplina = new List<Disciplina>();

            SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["minhaConexao"].ConnectionString);

            conexao.Open();

            string sql = "SELECT * FROM Disciplina";
            SqlCommand sqlCommand = new SqlCommand(sql, conexao);
            SqlDataReader sqlRead = sqlCommand.ExecuteReader();

            while (sqlRead.Read())
            {
                disciplina.Add(new Disciplina(int.Parse(sqlRead["idDisciplina"].ToString()),
                    sqlRead["Nome"].ToString(),
                    sqlRead["Semestre"].ToString(),
                    sqlRead["Curso"].ToString()));
            }
            return disciplina;
        }

        public static string novaDisciplina(string nome, string semestre, string curso)
        {
            SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["minhaConexao"].ConnectionString);
            conexao.Open();
            string insert = string.Format("INSERT INTO Disciplina(Nome, Semestre, Curso)VALUES('{0}','{1}','{2}')",nome, semestre, curso);
            SqlCommand sqlCommand = new SqlCommand(insert, conexao);
            sqlCommand.ExecuteNonQuery();
            return "Sucesso";
        }

        public static string excluirVinculoDisciplinaAluno(int idAluno, int idDisciplina)
        {
            SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["minhaConexao"].ConnectionString);
            conexao.Open();
            string delete = string.Format("DELETE FROM AlunoDisciplina WHERE idAluno={0} AND idDisciplina={1}", idAluno, idDisciplina);
            SqlCommand sqlCommand = new SqlCommand(delete, conexao);
            sqlCommand.ExecuteNonQuery();
            return "Sucesso";
        }
       
    }
}