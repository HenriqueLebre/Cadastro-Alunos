using App.Domain;
using App.Repository;
using System;
using System.Collections.Generic;


namespace WebApp3.Models {
    public class AlunoModel {




        public List<AlunoDTO> listarAluno(int? id = null) {

            try {
                var alunoBD = new AlunoDAO();
                return alunoBD.listarAlunosDB(id);
            }
            catch (Exception ex) {

                throw new Exception($"Erro ao listar Alunos: Error => {ex.Message}");
            }
        }

        public void Inserir(AlunoDTO aluno) {
            //var listaAlunos = this.listarAluno();

            //var maxId = listaAlunos.Max(aluno => aluno.id);
            //Aluno.id = maxId + 1;
            //listaAlunos.Add(Aluno);

            //RescreverArquivo(listaAlunos);
            //return Aluno;

            try {
                var alunoBD = new AlunoDAO();
                alunoBD.inserirAlunosDB(aluno);
            }
            catch (Exception ex) {

                throw new Exception($"Erro ao inserir Aluno: Error => {ex.Message}");
            }

        }

        public void Atualizar(AlunoDTO aluno) {
            try {
                var alunoBD = new AlunoDAO();
                alunoBD.atualizarAlunosDB(aluno);
            }
            catch (Exception ex) {

                throw new Exception($"Erro ao atualizar o Aluno: Error => {ex.Message}");
            }
        }

        public void Deletar(int id) {
            try {
                var alunoBD = new AlunoDAO();
                alunoBD.deletarAlunosDB(id);
            }
            catch (Exception ex) {

                throw new Exception($"Erro ao deletar o Aluno: Error => {ex.Message}");
            }

        }
    }
}