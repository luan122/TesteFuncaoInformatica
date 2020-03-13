$(document).ready(function () {
    $("#formCadastro").validate({
        debug: true,
        rules: {
            Cpf: {
                required: true,
                remote: {
                    url: urlValidateCpf,
                    type: "post",
                    data: {
                        cpf: function () {
                            return $('#Cpf').val();
                        }
                    }
                },
                cpfexiste: $('#Cpf').val()
            }
        },
        messages: {
            Cpf: {
                remote: 'CPF Inválido',
            }
        }
    });
    $("#Cpf").mask("999.999.999-99");
    $.validator.addMethod(
        "cpfexiste",
        function (value, element) {
            var retorno = cpfexistsAjax(value);
            console.log("retornei:");
            console.log(retorno);
            return retorno;
        }, "CPF já cadastrado no sistema"
    );
    function cpfexistsAjax(cpf) {
        var retorno = false;
        $.ajax({
            url: urlCheckCpf,
            type: 'POST',
            contentType: "application/json",
            async: false,
            data: JSON.stringify({
                "cpf": cpf
            }),
            success: function (result) {
                if (!result) {
                    retorno = true;
                }
            }
        });
        return retorno;
    }
    $("#Cpf").on("change", function () {
        $("#Cpf").mask("999.999.999-99");
    });
    if (obj.Id != 0)
        $("#botaoBeneficiarios").append('<a onclick="ModalBeneficiarios()" class="btn btn-default">Beneficiários</a>');

});
function ModalBeneficiarios() {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">Beneficiarios</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
		'					<form id="formBeneficiario" method="post">														' +
        '                    <input name="id" id="id" value="0" type="hidden"/>' +
        '                    <input name="idCliente" value="'+obj.Id+'" id="idCliente" type="hidden"/>' +
        '                    <div class="row">                                                                              ' +
        '						<div class="col-md-4">																		' +
        '							<label for="Cpf">CPF:</label>															' +
        '							<input required = "required" type = "text" class="form-control" id = "Cpf" name="Cpf" placeholder="Ex.: 010.011.111-00" >' +
        '						</div>																						' +
        '						<div class="col-md-4">																		' +
        '							<label for="Nome">Nome:</label>															' +
        '							<input required = "required" type = "text" class="form-control" id="Nome" name="Nome" placeholder="Ex.: João da Silva" >' +
        '						</div>																						' +
        '						<div class="col-md-3">																		' +
		'							</br>																					' +
        '							<a class="btn btn-sm btn-success" onClick="EnviarBeneficiario()">Incluir</a>					' +
        '						</div>																						' +
        '					</div>                                                                                             ' +
        '					</form>																							' +
        '					 <div class="border-top my-3"></div>															' +
        '                    <div class="row"><div  id="listaBeneficiarios" class="col-md-12"></div></div>                                                ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '             </div>' +              
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
    var dados = "";
    $.ajax({
        url: urlGetBenefiiarios,
        type: 'POST',
        contentType: "application/json",
        async: false,
        data: JSON.stringify({
            "id": idCliente
        }),
        success: function (result) {
            if (result) {
                for (var i = 0; i < result.length; i++) {
                    dados += '<tr id="beneficiario' + result[i].Id + '"><td>' + result[i].Cpf + '</td><td>' + result[i].Nome + '</td><td><a href="#" onClick="EditarBeneficiario(' + result[i].Id + ')" class="btn btn-default">Editar</a><a href="#" onClick="DeletarBeneficiario(' + result[i].Id + ')" class="btn btn-default">Excluir</a></td></tr>'
                }
            }
        }
    });
    var table = '\
		<table class="table">\
			<thead>\
				<tr>\
					<td><span style="font-weight:bold">CPF</span></td>\
					<td><span style="font-weight:bold">Nome</span></td>\
					<td></td>\
				</tr>\
			</thead >\
			<tbody>'+ dados +'</tbody>\
		</table>';

    $('#listaBeneficiarios').append(table);
}
function EditarBeneficiario(id) {
    $.ajax({
        url: urlGetBenefiiario,
        type: 'POST',
        contentType: "application/json",
        async: false,
        data: JSON.stringify({
            "id": id
        }),
        success: function (result) {
            if (result) {
                $("#formBeneficiario #id").val(result.Id);
                $("#formBeneficiario #idCliente").val(result.IdCliente);
                $("#formBeneficiario #Nome").val(result.Nome);
                $("#formBeneficiario #Cpf").val(result.Cpf);
            }
        }
    });
}
function EnviarBeneficiario() {
    if ($("#formBeneficiario #id").val() == "0") {
        $.ajax({
            url: urlPostBeneficiario,
            type: 'POST',
            contentType: "application/json",
            async: false,
            data: JSON.stringify({
                "Id": $("#formBeneficiario #id").val(),
                "IdCliente": $("#formBeneficiario #idCliente").val(),
                "Nome": $("#formBeneficiario #Nome").val(),
                "Cpf": $("#formBeneficiario #Cpf").val(),
            }),
            success: function (result) {
                if (result) {
                    alert("Beneficiario incluido com sucesso!");
                }
            }
        });
    } else {
        $.ajax({
            url: urlPostAltBeneficiario,
            type: 'POST',
            contentType: "application/json",
            async: false,
            data: JSON.stringify({
                "Id": $("#formBeneficiario #id").val(),
                "IdCliente": $("#formBeneficiario #idCliente").val(),
                "Nome": $("#formBeneficiario #Nome").val(),
                "Cpf": $("#formBeneficiario #Cpf").val(),
            }),
            success: function (result) {
                if (result) {
                    alert("Beneficiario alterado com sucesso!");
                }
            }
        });
    }
}