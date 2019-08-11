function VazioOuNulo(objeto) {
    return objeto == "" || objeto == undefined || objeto == null;
}

function ExcluirLinha(objeto) {
    $(objeto).closest('tr').remove();
}

function LimparCampos() {
    $('#CPFBeneficiario').val("");
    $('#NomeBeneficiario').val("");
}

function ValidadorDeCPF(CPF) {
    if (!CPFValido(CPF.val().replace(/[^\w\s]/gi, ''))) {
        alert('CPF inválido');
        LimparCampos();
        CampoOnError($('#CPFBeneficiario'));
        return false;
    }

    if (CPFJaExiste(CPF.val())) {
        alert('CPF já cadastrado!');
        LimparCampos();
        CampoOnError(CPF);
        return false;
    }
    return true;
}

function CPFJaExiste(CPF) {
    let linhas = $('#tblBeneficiarios > tbody > tr');
    for (var i = 0; i < linhas.length; i++) {
        if (linhas[i].childNodes[0].innerHTML == CPF) return true;
    }
    return false;
}

function CPFValido(CPF) {
    var Soma;
    var Resto;
    Soma = 0;
    if (CPF == "00000000000") return false;

    for (i = 1; i <= 9; i++) Soma = Soma + parseInt(CPF.substring(i - 1, i)) * (11 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11)) Resto = 0;
    if (Resto != parseInt(CPF.substring(9, 10))) return false;

    Soma = 0;
    for (i = 1; i <= 10; i++) Soma = Soma + parseInt(CPF.substring(i - 1, i)) * (12 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11)) Resto = 0;
    if (Resto != parseInt(CPF.substring(10, 11))) return false;
    return true;
}
