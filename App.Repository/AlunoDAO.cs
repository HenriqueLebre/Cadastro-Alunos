using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace App.Repository {
    public class AlunoDAO {

        private string stringConnection = ConfigurationManager.ConnectionStrings["ConnectionDB"].ConnectionString;
        private IDbConnection connection;


        public AlunoDAO() {

            connection = new SqlConnection(stringConnection);

            connection.Open();
        }

        public List<AlunoDTO> listarAlunosDB(int? id) {

            var listaAlunos = new List<AlunoDTO>();

            try {

                IDbCommand selectCmd = connection.CreateCommand();

                if (id == null)
                    selectCmd.CommandText = "select * from Alunos";
                else
                    selectCmd.CommandText = $"select * from Alunos where id = {id}";


                IDataReader resultado = selectCmd.ExecuteReader();
                while (resultado.Read()) {

                    var alu = new AlunoDTO {
                        id = Convert.ToInt32(resultado["Id"]),
                        nome = Convert.ToString(resultado["nome"]),
                        sobrenome = Convert.ToString(resultado["sobrenome"]),
                        telefone = Convert.ToString(resultado["telefone"]),
                        nascimento = Convert.ToString(resultado["nascimento"]),
                        ra = Convert.ToInt32(resultado["ra"]),
                    };
                    listaAlunos.Add(alu);

                }

                return listaAlunos;

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
            finally {
                connection.Close();
            }


        }

        public void inserirAlunosDB(AlunoDTO aluno) {

            try {
                IDbCommand insertCmd = connection.CreateCommand();
                insertCmd.CommandText = "insert into Alunos (nome, sobrenome, telefone, nascimento, ra) values (@nome, @sobrenome, @telefone, @nascimento, @ra)";

                IDbDataParameter paramNome = new SqlParameter("nome", aluno.nome);
                insertCmd.Parameters.Add(paramNome);

                IDbDataParameter paramSobrenome = new SqlParameter("nome", aluno.sobrenome);
                insertCmd.Parameters.Add(paramSobrenome);

                IDbDataParameter paramTelefone = new SqlParameter("nome", aluno.telefone);
                insertCmd.Parameters.Add(paramTelefone);

                IDbDataParameter paramNascimento = new SqlParameter("nome", aluno.nascimento);
                insertCmd.Parameters.Add(paramNascimento);

                IDbDataParameter paramRa = new SqlParameter("nome", aluno.ra);
                insertCmd.Parameters.Add(paramRa);

                insertCmd.ExecuteNonQuery();
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
            finally {
                connection.Close();
            }

        }

        public void atualizarAlunosDB(AlunoDTO aluno) {

            try {
                IDbCommand updateCmd = connection.CreateCommand();
                updateCmd.CommandText = "update Alunos set nome = @nome, sobrenome = @sobrenome, telefone = @telefone, nascimento = @nascimento, ra = @ra where id = @id";

                IDbDataParameter paramNome = new SqlParameter("nome", aluno.nome);
                IDbDataParameter paramSobrenome = new SqlParameter("sobrenome", aluno.sobrenome);
                IDbDataParameter paramTelefone = new SqlParameter("telefone", aluno.telefone);
                IDbDataParameter paramNascimento = new SqlParameter("nascimento", aluno.nascimento);
                IDbDataParameter paramRa = new SqlParameter("ra", aluno.ra);
                updateCmd.Parameters.Add(paramNome);
                updateCmd.Parameters.Add(paramSobrenome);
                updateCmd.Parameters.Add(paramTelefone);
                updateCmd.Parameters.Add(paramNascimento);
                updateCmd.Parameters.Add(paramRa);

                IDbDataParameter paramID = new SqlParameter("id", aluno.id);
                updateCmd.Parameters.Add(paramID);

                updateCmd.ExecuteNonQuery();
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
            finally {
                connection.Close();
            }
        }


        public void deletarAlunosDB(int id) {

            try {
                IDbCommand deleteCmd = connection.CreateCommand();
                deleteCmd.CommandText = "delete from Alunos where id = @id";

                IDbDataParameter paramID = new SqlParameter("id", id);
                deleteCmd.Parameters.Add(paramID);

                deleteCmd.ExecuteNonQuery();
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
            finally {
                connection.Close();
            }
        }
    }
}