using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebAtividadeEntrevista.Helpers;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            BoCliente bo = new BoCliente();

            if (!CpfHelper.ValidarCPF(model.CPF))
                ModelState.AddModelError("CPF", "CPF Inválido");
            else
            {
                model.CPF = model.CPF.Trim().Replace(".", "").Replace("-", "");

                if (bo.VerificarExistencia(model.CPF))
                    ModelState.AddModelError("CPF", "CPF Já Cadastrado na base de dados!");
            }

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

                #region | Lista Beneficiarios |
                List<Beneficiario> beneficiarios = new List<Beneficiario>();
                foreach (var item in model.Beneficiarios)
                {
                    beneficiarios.Add(new Beneficiario()
                    {
                        CPF = item.CPF,
                        Id = item.Id,
                        Nome = item.Nome
                    });
                }
                #endregion

                model.Id = bo.Incluir(new Cliente()
                {
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone,
                    CPF = model.CPF
                }, 
                beneficiarios);

                return Json("Cadastro efetuado com sucesso");
            }
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            BoCliente bo = new BoCliente();

            if (!CpfHelper.ValidarCPF(model.CPF))
                ModelState.AddModelError("CPF", "CPF Inválido");
            else
                model.CPF = model.CPF.Trim().Replace(".", "").Replace("-", "");

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
                #region Trazendo os beneficiarios
                List<Beneficiario> BeneficiariosIncluir = new List<Beneficiario>();
                List<Beneficiario> BeneficiariosUpdate = new List<Beneficiario>();
                List<long> BeneficiariosDelete = new List<long>();
                foreach (var item in model.Beneficiarios)
                {
                    BeneficiariosIncluir.Add(new Beneficiario()
                    {
                        CPF = item.CPF,
                        IdCliente = item.IdCliente,
                        Nome = item.Nome
                    });
                }
                foreach (var item in model.BeneficiariosUpdate)
                {
                    BeneficiariosUpdate.Add(new Beneficiario()
                    {
                        CPF = item.CPF,
                        IdCliente = model.Id,
                        Id = item.Id,
                        Nome = item.Nome
                    });
                }
                foreach (var item in model.BeneficiariosDelete)
                {
                    BeneficiariosDelete.Add(item.Id);
                }
                #endregion

                bo.Alterar(new Cliente()
                {
                    Id = model.Id,
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone,
                    CPF = model.CPF
                }, 
                BeneficiariosIncluir, 
                BeneficiariosUpdate, 
                BeneficiariosDelete);

                return Json("Cadastro alterado com sucesso");
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoCliente bo = new BoCliente();
            Cliente cliente = bo.Consultar(id);
            Models.ClienteModel model = null;

            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone,
                    CPF = cliente.CPF
                };
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}