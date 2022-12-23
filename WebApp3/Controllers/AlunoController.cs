using App.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApp3.Models;

namespace WebApp3.Controllers {
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/Aluno")]

    public class AlunoController : ApiController {
        // GET: api/Aluno
        [HttpGet]
        [Route("Recuperar")]
        [Authorize]
        public IHttpActionResult Recuperar() {
            try {
                AlunoModel aluno = new AlunoModel();
                var alunos = aluno.listarAluno();
                return Ok(aluno.listarAluno());
            }
            catch (Exception ex) {

                return InternalServerError(ex);
            }


        }

        // GET: api/Aluno/5
        [HttpGet]
        [Route("Recuperar/{id}")]
        public IHttpActionResult RecuperarPorId(int id, string nome, string sobrenome) {
            try {
                AlunoModel aluno = new AlunoModel();

                return Ok(aluno.listarAluno(id).FirstOrDefault());
            }
            catch (Exception ex) {

                return InternalServerError(ex);
            }
        }

        // GET: api/Aluno/5
        [HttpGet]
        [Route(@"RecuperarPorNascimento/{nascimento:regex([0-9]{4}\-[0-9]{2}-[0-9]{2})}")]
        public IHttpActionResult Recuperar(string nascimento) {

            try {
                AlunoModel aluno = new AlunoModel();

                IEnumerable<AlunoDTO> alunos = aluno.listarAluno().Where(x => x.nascimento == nascimento);

                if (!alunos.Any())
                    return NotFound();

                return Ok(alunos);
            }
            catch (Exception) {

                return InternalServerError();
            }

        }

        [HttpPost]
        public IHttpActionResult Post(AlunoDTO aluno) {

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try {
                AlunoModel _aluno = new AlunoModel();

                _aluno.Inserir(aluno);

                return Ok(_aluno.listarAluno());
            }
            catch (Exception ex) {

                return InternalServerError(ex);
            }

        }

        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] AlunoDTO aluno) {

            try {
                AlunoModel _aluno = new AlunoModel();
                aluno.id = id;
                _aluno.Atualizar(aluno);

                return Ok(_aluno.listarAluno().FirstOrDefault(alu => alu.id == id));
            }
            catch (Exception ex) {

                return InternalServerError(ex);
            }
            

        }

        [HttpDelete]
        public IHttpActionResult Delete(int id) {

            try {
                AlunoModel _aluno = new AlunoModel();
                _aluno.Deletar(id);

                return Ok("Usuário deletado com sucesso!");
            }
            catch (Exception ex) {

                return InternalServerError(ex);
            }

        }
    }
}
