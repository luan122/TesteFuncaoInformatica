using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        [HttpPost]
        public JsonResult BeneficiariosList(long id)
        {
            var beneficiarios = new List<BeneficiarioModel>();
            List<Beneficiario> beneficiariosDML = new BoBeneficiario().Listar(id);
            foreach (var beneficiario in beneficiariosDML)
                beneficiarios.Add(new BeneficiarioModel() {
                    Id = beneficiario.Id,
                    IdCliente = beneficiario.IdCliente,
                    Nome = beneficiario.Nome,
                    Cpf = Convert.ToUInt64(beneficiario.Cpf).ToString(@"000\.000\.000\-00")
                });
            return Json(beneficiarios);
        }
        [HttpPost]
        public JsonResult Incluir(BeneficiarioModel beneficiario)
        {
            var retorno = new BoBeneficiario().Incluir(new Beneficiario() {
                Cpf = beneficiario.Cpf,
                Nome = beneficiario.Nome,
                IdCliente = beneficiario.IdCliente
            });
            return Json(retorno);
        }
        [HttpPost]
        public JsonResult Buscar(long id)
        {
            var retorno = new BoBeneficiario().Consultar(id);
            var beneficiario = new BeneficiarioModel()
            {
                Id = retorno.Id,
                IdCliente = retorno.IdCliente,
                Nome = retorno.Nome,
                Cpf = retorno.Cpf
            };
            return Json(beneficiario);
        }
        [HttpPost]
        public JsonResult Alterar(BeneficiarioModel model)
        {
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                new BoBeneficiario().Alterar(new Beneficiario()
                {
                    Id = model.Id,
                    IdCliente = model.IdCliente,
                    Nome = model.Nome,
                    Cpf = model.Cpf
                });
                return Json("Beneficiário alterado com sucesso!");
            }
        }
    }
}