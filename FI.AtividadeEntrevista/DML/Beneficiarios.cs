﻿namespace FI.AtividadeEntrevista.DML
{
    public class Beneficiario
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Id Cliente
        /// </summary>
        public long IdCliente { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        public string Cpf { get; set; }
        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }
    }
}
