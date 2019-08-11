function Beneficiario(Id,IdCliente,CPF, Nome) {
    this.Id = Id ? Id : 0;
    this.IdCliente = IdCliente ? IdCliente : 0;
    this.CPF = CPF;
    this.Nome = Nome;
}