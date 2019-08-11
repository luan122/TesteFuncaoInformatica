var tabela = $('#tblBeneficiarios > tbody');

function InserirLinhaTabela(CPF, Nome, BeneficiarioId) {
    let linha =
        `<tr data-id="${BeneficiarioId}">` +
        '<td>' + CPF + '</td>' +
        '<td>' + Nome + '</td>' +
        '<td>' +
        '<div class="row">' +
        '<div class="col-sm-5">' +
        `<button type="button" class='btn btn-primary' onclick="AlterarDadosBeneficiario('${CPF}','${Nome}')">Alterar</button>` +
        '</div>' +
        '<div class="col-sm-5">' +
        '<button type="button" class="btn btn-primary" onclick="ExcluirBeneficiario(this)">Excluir</button>' +
        '</div>' +
        '</div>' +
        '</td>' +
        '</tr>';
    $(tabela).append(linha);
   
}
function LimparTabela() {
    $(tabela).html("");
}