using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        [HttpPost]
        public JsonResult Alterar(BeneficiarioModel beneficiario)
        {
            BoBeneficiario bo = new BoBeneficiario();

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
                if (bo.VerificarExistencia(beneficiario.CPF, beneficiario.IdCliente))
                {
                    Response.StatusCode = 400;
                    return Json("CPF já cadastrado para esse beneficiário");
                }

                bo.Alterar(new Beneficiario()
                {
                    Id = beneficiario.Id,
                    IdCliente = beneficiario.IdCliente,
                    Nome = beneficiario.Nome,
                    CPF = beneficiario.CPF
                });

                return Json("Cadastro alterado com sucesso");
            }
        }

        [HttpPost]
        public JsonResult Excluir(long Id)
        {
            if (Id == 0)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                BoBeneficiario boBeneficiario = new BoBeneficiario();
                boBeneficiario.Excluir(Id);

                Response.StatusCode = 200;
                return Json("Cadastro excluído com sucesso");
            }
        }

        [HttpPost]
        public JsonResult Adicionar(BeneficiarioModel beneficiario)
        {
            BoBeneficiario bo = new BoBeneficiario();
            if (bo.VerificarExistencia(beneficiario.CPF, beneficiario.IdCliente))
            {
                return Json("CPF já cadastrado para esse beneficiário");
            }
            List<Beneficiario> beneficiarios = new List<Beneficiario>();
            beneficiarios.Add(new Beneficiario()
            {
                CPF = beneficiario.CPF,
                IdCliente = beneficiario.IdCliente,
                Nome = beneficiario.Nome
            });

            beneficiario.Id = bo.Incluir(beneficiarios).Last();
            Response.StatusCode = 200;
            return Json(JsonConvert.SerializeObject(beneficiario));
        }

        [HttpGet]
        public JsonResult Beneficiarios(long Id)
        {
            if (Id == 0)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                BoBeneficiario boBeneficiario = new BoBeneficiario();
                var beneficiariosJson = JsonConvert.SerializeObject(boBeneficiario.Listar(Id));
                Response.StatusCode = 200;
                return Json(beneficiariosJson, JsonRequestBehavior.AllowGet);
            }


        }
    }
}