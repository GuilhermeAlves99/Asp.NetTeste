using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
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
        public JsonResult Incluir(BeneficiarioModel model)
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

                model.Id = bo.Incluir(new Beneficiario()
                {
                    CPF = model.CPF,
                    Nome = model.Nome,
                    IdCliente = model.IdCliente
                });


                return Json("Cadastro efetuado com sucesso");
            }
        }

        [HttpPost]
        public JsonResult Alterar(BeneficiarioModel model)
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
                bo.Alterar(new Beneficiario()
                {
                    IdCliente = model.IdCliente,
                    Nome = model.Nome,
                    CPF = model.CPF
                });

                return Json("Cadastro alterado com sucesso");
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoBeneficiario bo = new BoBeneficiario();
            Beneficiario bn = bo.Consultar(id);
            Models.BeneficiarioModel model = null;

            if (bn != null)
            {
                model = new BeneficiarioModel()
                {
                    Id = bn.IdBeneficiario,
                    Nome = bn.Nome,
                    CPF = bn.CPF,
                    IdCliente=bn.IdCliente,
                };


            }

            return View(model);
        }

        [HttpPost]
        public JsonResult BeneficiarioList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "Nome ASC")
        {
            try
            {
                int totalRecords;
                string campo = "Nome"; 
                bool isCrescente = true;

                if (!string.IsNullOrEmpty(jtSorting))
                {
                    var array = jtSorting.Split(' ');
                    campo = array[0];
                    isCrescente = array.Length > 1 && array[1].Equals("ASC", StringComparison.OrdinalIgnoreCase);
                }

                List<Beneficiario> beneficiarios = new BoBeneficiario().Pesquisa(jtStartIndex, jtPageSize, campo, isCrescente, out totalRecords);

                
                return Json(new { Result = "OK", Records = beneficiarios, TotalRecordCount = totalRecords });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

    }
}