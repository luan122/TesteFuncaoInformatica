function FecharModalBeneficiarios() {
    let linhas = $('#tblBeneficiarios > tbody > tr');
    //foreach das linhas
    for (var i = 0; i < linhas.length; i++) {
        let _beneficiario = Object.create(Beneficiario.prototype);
        let _idBeneficiario = $(linhas[i]).data('id');
        _beneficiario = new Beneficiario(_idBeneficiario,$('#Id').val(),linhas[i].childNodes[0].innerHTML, linhas[i].childNodes[1].innerHTML);

        if (_idBeneficiario == "0" || _idBeneficiario == null || _idBeneficiario == undefined) {
            Beneficiarios.push(_beneficiario);
        }
        else {
            BeneficiariosAlterar.push(_beneficiario);
        }
    }
    
}