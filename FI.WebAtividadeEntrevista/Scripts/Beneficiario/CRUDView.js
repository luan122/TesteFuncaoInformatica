function ExcluirBeneficiario(objeto) {
    if ($('#Id').val()) {
        let Id = $(objeto).closest('tr').data('id');
        if (Id != 0 && Id != null && Id != undefined) {
            let _beneficiarioEx = Object.create(Beneficiario.prototype);
            let linha = $(objeto).closest('tr');
            _beneficiarioEx = new Beneficiario(Id, $('#Id').val(), linha[0].childNodes[0].innerHTML, linha[0].childNodes[1].innerHTML);
            BeneficiariosExcluir.push(_beneficiarioEx);

            $(objeto).closest('tr').addClass('esconder');
            setTimeout(ExcluirLinha(objeto), 500);
        }
    } else {
        $(objeto).closest('tr').addClass('esconder');
        setTimeout(ExcluirLinha(objeto), 100);
    }
}

function AlterarDadosBeneficiario(CPF, Nome) {
    $('#CPFBeneficiario').val(CPF);
    $('#NomeBeneficiario').val(Nome);
    if (VazioOuNulo($('#CPFBeneficiario').val()) || VazioOuNulo($('#NomeBeneficiario').val())) {
        alert('Erro ao buscar os dados do beneficiário');
        return;
    }
    flgAlterando = true;
    $('#btnIncluir').text('Salvar');

    //Buscar os dados na tabela
    let linhas = $("#tblBeneficiarios > tbody > tr");
    let linhaCorreta = null;
    for (var i = 0; i < linhas.length; i++) {
        if (linhas[i].childNodes[0].innerHTML == CPF) {
            linhaCorreta = linhas[i];
        }
    }
    $('#IdBeneficiario').val($(linhaCorreta).data('id'));
    linhaCorreta.remove();
}

function AddBeneficiario() {
    let CPF = $('#CPFBeneficiario');
    let Nome = $('#NomeBeneficiario');
    if (!ValidadorDeCPF(CPF)) return;
    if (VazioOuNulo(Nome.val())) {
        CampoOnError(Nome);
    }
    else if (!flgAlterando) {
        CampoOnSuccess(CPF);
        CampoOnSuccess(Nome);
        let IdCliente = $('#Id').val();
        if (IdCliente != "" && IdCliente != null && IdCliente != undefined) {
            
            InserirLinhaTabela(CPF.val(), Nome.val(), 0);
        }
        else {
            InserirLinhaTabela(CPF.val(), Nome.val(), 0);
        }
        $('#CPFBeneficiario').removeClass('success');
        $('#NomeBeneficiario').removeClass('success');
        LimparCampos();
    }
    else {
        flgAlterando = false;
        if (!ValidadorDeCPF(CPF)) return;
        if (VazioOuNulo(Nome.val())) {
            CampoOnError(Nome);
            return;
        }
        CampoOnSuccess(CPF);
        CampoOnSuccess(NOME);
        let Id = $('#IdBeneficiario').val();
        InserirLinhaTabela(CPF.val(), Nome.val(), Id);

        $('#CPFBeneficiario').removeClass('success');
        $('#NomeBeneficiario').removeClass('success');
        LimparCampos();
        $('#btnIncluir').text('Incluir');
    }
}