using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAtividadeEntrevista.Models
{
    public class BeneficiarioModel
    {
        public long Id { get; set; }
        public long IdCliente { get; set; }
        [Required]
        public string Cpf { get; set; }
        [Required]
        public string Nome { get; set; }
    }
}