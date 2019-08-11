function BuscarBeneficiarios() {
    Beneficiarios = [];
    BeneficiariosAlterar = [];
    if ($('#Id').val()) {
        LimparTabela();
        $.ajax({
            method: 'GET',
            data: { Id: $('#Id').val() },
            url: '/Beneficiario/Beneficiarios',
            success: function (data) {
                let dados = JSON.parse(data);
                dados.forEach(beneficiario => {
                    InserirLinhaTabela(beneficiario.CPF, beneficiario.Nome, beneficiario.Id);                   
                });
            },
            error: function () {
                alert('Erro ao preenchar a tabela de beneficiarios, tente novamente!');
            }
        });
    }
   
}