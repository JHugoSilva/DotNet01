using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace TecWeb.Models
{
    public class Aluno
    {
        public int IdAluno { get; set; }
        public string Nome { get; set; }
        public int RA { get; set; }
        public DateTime DataNascimento { get; set; }

        public Aluno(int idAluno, string nome, int ra, DateTime dataNasc) {
            IdAluno = idAluno;
            Nome = nome;
            RA = ra;
            DataNascimento = dataNasc;
        }

        public static List<Aluno> listarAlunos()
        {
            SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["minhaConexao"].ConnectionString);

            conexao.Open();

            string sql = "SELECT * FROM Aluno";
            SqlCommand sqlCommand = new SqlCommand(sql, conexao);
            SqlDataReader sqlRead = sqlCommand.ExecuteReader();
            var alunos = new List<Aluno>();
            while (sqlRead.Read())
            {
                alunos.Add(new Aluno(int.Parse(sqlRead["IdAluno"].ToString()),
                    sqlRead["Nome"].ToString(),
                    int.Parse(sqlRead["RA"].ToString()),
                    Convert.ToDateTime(sqlRead["DataNascimento"].ToString())));
            }
            return alunos;
        }

        public static List<Aluno> listarAlunosPelaDisciplina(int idDisciplina)
        {
          
            SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["minhaConexao"].ConnectionString);

            conexao.Open();

            string sql = "SELECT * FROM Aluno INNER JOIN AlunoDisciplina ON Aluno.IdAluno=AlunoDisciplina.idAluno WHERE AlunoDisciplina.idDisciplina=" + idDisciplina;
            Console.WriteLine(sql);
            SqlCommand sqlCommand = new SqlCommand(sql, conexao);
            SqlDataReader sqlRead = sqlCommand.ExecuteReader();
            var alunos = new List<Aluno>();
           
              while (sqlRead.Read())
                {
                    alunos.Add(new Aluno(int.Parse(sqlRead["IdAluno"].ToString()),
                        sqlRead["Nome"].ToString(),
                        int.Parse(sqlRead["RA"].ToString()),
                        Convert.ToDateTime(sqlRead["DataNascimento"].ToString())));
                }
            return alunos;
        }
    }
}