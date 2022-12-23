var tbody = document.querySelector('table tbody');
var aluno = {};

function Cadastrar() {

    aluno.nome = document.querySelector('#nome').value;
    aluno.sobrenome = document.querySelector('#sobrenome').value;
    aluno.telefone = document.querySelector('#telefone').value;
    aluno.nascimento = document.querySelector('#nascimento').value;
    aluno.ra = document.querySelector('#ra').value;

    console.log(aluno);

    if (aluno.id === undefined || aluno.id === 0) {
        salvarEstudantes('POST', 0, aluno);
    } else {
        salvarEstudantes('PUT', aluno.id, aluno);
    }

    carregaEstudantes();

    $('#myModal').modal('hide');

}

function novoAluno(){
    var btnSalvar = document.querySelector('#btnSalvar');
    var titulo = document.querySelector('#titulo');

    document.getElementById('nome').value = '';
    document.getElementById('sobrenome').value = '';
    document.getElementById('telefone').value = '';
    document.getElementById('nascimento').value = '';
    document.getElementById('ra').value = '';

    aluno = {};


    titulo.textContent = "Cadastrar Aluno";
    btnSalvar.textContent = "Cadastrar";

    $('#myModal').modal('show');
}

function Cancelar() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var titulo = document.querySelector('#titulo');

    document.getElementById('nome').value = '';
    document.getElementById('sobrenome').value = '';
    document.getElementById('telefone').value = '';
    document.getElementById('nascimento').value = '';
    document.getElementById('ra').value = '';

    aluno = {};


    titulo.textContent = "Cadastrar Aluno";
    btnSalvar.textContent = "Cadastrar";

    $('#myModal').modal('hide');
}

function carregaEstudantes() {
    tbody.innerHTML = '';

    var xhr = new XMLHttpRequest();

    xhr.open(`GET`, `https://localhost:44336/api/Aluno/Recuperar`, true);
    xhr.setRequestHeader('Authorization', sessionStorage.getItem('token'));

    xhr.onerror = function(){
        console.log('ERROR', xhr.readyState);
    }

    xhr.onreadystatechange = function(){
        if(this.readyState == 4){
            if(this.status == 200){
                var estudantes = JSON.parse(xhr.responseText);
                for(var indice in estudantes){
                    adcionaLinha(estudantes[indice]);
                }
            }else if(this.status == 500){
                var erro = JSON.parse(xhr.responseText);
                console.log(erro.Message);
                console.log(erro.ExceptionMessage);
            }

        }
        
}

    xhr.send();

}

//    tbody.innerHTML = '';
//
//    var xhr = new XMLHttpRequest();
//
//
//
//    xhr.open(`GET`, `https://localhost:44371/api/Aluno`, true);
//
//    xhr.onload = function () {
//        var estudantes = JSON.parse(this.responseText);
//        for (var indice in estudantes) {
//            adcionaLinha(estudantes[indice]);
//        }
//    }
//    xhr.send();



function salvarEstudantes(metodo, id, corpo) {

    var xhr = new XMLHttpRequest();

    if (id === undefined || id === 0)
        id = '';

    xhr.open(metodo, `https://localhost:44336/api/Aluno/${id}`, false);

    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(JSON.stringify(corpo));
}

function excluirEstudante(id) {
    var xhr = new XMLHttpRequest();

    xhr.open(`DELETE`, `https://localhost:44336/api/Aluno/${id}`, false);

    xhr.send();
}

function excluir(estudante) {
    bootbox.confirm({
        message: `Certeza que quer excluir estudante: ${estudante.nome} ${estudante.sobrenome}`,
        buttons: {
            confirm: {
                label: 'SIM',
                className: 'btn-success'
            },
            cancel: {
                label: 'NÃO',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if(result){
                excluirEstudante(estudante.id);
                carregaEstudantes();
            };
        }
    });
}


carregaEstudantes();

function editarEstudante(estudante) {
    var btnSalvar = document.querySelector('#btnSalvar');
    var titulo = document.querySelector('#titulo');

    document.querySelector('#nome').value = estudante.nome;
    document.querySelector('#sobrenome').value = estudante.sobrenome;
    document.querySelector('#telefone').value = estudante.telefone;
    document.getElementById('nascimento').value = estudante.nascimento;
    document.querySelector('#ra').value = estudante.ra;


    titulo.textContent = `Editar Aluno ${estudante.nome}`;
    btnSalvar.textContent = "Salvar";

    aluno = estudante;

    console.log(aluno);
}

function adcionaLinha(estudante) {

    var trow = `<tr>
                            <td>${estudante.nome}</td>
                            <td>${estudante.sobrenome}</td>
                            <td>${estudante.telefone}</td>
                            <td>${estudante.nascimento}</td>
                            <td>${estudante.ra}</td>
                            <td>
                                <button class="btn btn-info" data-toggle="modal" data-target="#myModal" onclick='editarEstudante(${JSON.stringify(estudante)})'>Editar</button>
                                <button class="btn btn-danger" onclick='excluir(${JSON.stringify(estudante)})'>Excluir</button>
                            </td>
                        <\tr>`
    tbody.innerHTML += trow;
}