using Liberta.Dominio.BLL;
using Liberta.Dominio.Model.Enumeradores;
using Liberta.Dominio.Utils;
using Liberta.UI.Controllers.BaseController;
using Liberta.UI.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Liberta.UI.Controllers
{
    public class CadastroTitulosController : Controller
    {
        ResultadoOperacao resultadoOperacao = new ResultadoOperacao();

        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Cookie.Get("IdUsuario")))
            {
                return RedirectToAction("../Login/Login");
            }

            ViewBag.IdUsuario = Cookie.Get("IdUsuario");
            ViewBag.NomeUsuario = Cookie.Get("NomeUsuario");
            ViewBag.PerfilUsuario = Cookie.Get("PerfilUsuario");
            ViewBag.SaldoUsuario = Cookie.Get("SaldoAtualUsuario");

            CadastroTitulos viewModel = new CadastroTitulos();
            viewModel.CarregarTipoAtivo();
            viewModel.CarregarModulo();

            TitulosNegocio negocio = new TitulosNegocio();
            var listaModel = negocio.List(new Dominio.Model.Titulos());

            foreach (var model in listaModel)
            {
                var view = new CadastroTitulos();
                view.Id = model.Id;
                view.Nome = model.Nome;
                view.PercentualRentabilidade = model.PercentualRentabilidade.ToString();
                view.ValorInvestimentoMinimo = model.ValorInvestimentoMinimo.ToString();
                view.DiasResgate = model.DiasResgate.ToString();
                view.DataVencimento = model.DataVencimento.ToShortDateString();
                view.PercentualIR = model.PercentualIR.ToString();
                view.PercentualIOF = model.PercentualIOF.ToString();
                view.TipoAtivo = model.TipoAtivo.ToString();
                if (model.TipoAtivo == ETipoAtivo.RendaFixa)
                {
                    view.TipoTitulo = model.TipoTituloRendaFixa.ToString();
                }
                else if (model.TipoAtivo == ETipoAtivo.RendaVariavel)
                {
                    view.TipoTitulo = model.TipoTituloRendaVariavel.ToString();
                }
              
                view.Modulo = model.Modulo.ToString();
                viewModel.listaGrid.Add(view);
            }
            return View(viewModel);
        }

        public JsonResult CarregarTipos(int tipoRenda)
        {
            try
            {
                CadastroTitulos view = new CadastroTitulos();
                view.CarregarTipoTitulo((Dominio.Model.Enumeradores.ETipoAtivo)tipoRenda);

                return Json(view.ddlTipoTitulo);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult Gravar(CadastroTitulos view)
        {
            try
            {

                Liberta.Dominio.Model.Titulos model = new Dominio.Model.Titulos();
                model.Id = view.Id;
                model.Nome = view.Nome;
                model.PercentualRentabilidade = Convert.ToDecimal(view.PercentualRentabilidade);
                model.ValorInvestimentoMinimo = Convert.ToDecimal(view.ValorInvestimentoMinimo);
                model.DiasResgate = Convert.ToInt32(view.DiasResgate);
                model.DataVencimento = Convert.ToDateTime(view.DataVencimento);
                model.PercentualIR = Convert.ToDecimal(view.PercentualIR);
                model.PercentualIOF = Convert.ToDecimal(view.PercentualIOF);
                model.TipoAtivo = (ETipoAtivo)Convert.ToInt32(view.TipoAtivo);
                if(model.TipoAtivo == ETipoAtivo.RendaFixa)
                {
                    model.TipoTituloRendaFixa = (ETipoRendaFixa)Convert.ToInt32(view.TipoTitulo);
                }
                else if(model.TipoAtivo == ETipoAtivo.RendaVariavel)
                {
                    model.TipoTituloRendaVariavel = (ETipoRendaVariavel)Convert.ToInt32(view.TipoTitulo);
                }
                model.Modulo = (EModulos)Convert.ToInt32(view.Modulo);

                TitulosNegocio negocio = new TitulosNegocio();

                resultadoOperacao = negocio.Gravar(model);

                return Json(resultadoOperacao);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult Editar(int Id)
        {
            try
            {
                CadastroTitulos view = new CadastroTitulos();
                TitulosNegocio negocio = new TitulosNegocio();

                var model = negocio.GetItem(Id);

                view.Id = model.Id;
                view.Nome = model.Nome;
                view.PercentualRentabilidade = model.PercentualRentabilidade.ToString();
                view.ValorInvestimentoMinimo = model.ValorInvestimentoMinimo.ToString();
                view.DiasResgate = model.DiasResgate.ToString();
                view.DataVencimento = model.DataVencimento.ToShortDateString();
                view.PercentualIR = model.PercentualIR.ToString();
                view.PercentualIOF = model.PercentualIOF.ToString();
                view.TipoAtivo = model.TipoAtivo.GetHashCode().ToString();
                view.CarregarTipoAtivo();
                if (model.TipoAtivo == ETipoAtivo.RendaFixa)
                {
                    view.TipoTitulo = model.TipoTituloRendaFixa.GetHashCode().ToString();
                    view.CarregarTipoTitulo(ETipoAtivo.RendaFixa);
                }
                else if (model.TipoAtivo == ETipoAtivo.RendaVariavel)
                {
                    view.TipoTitulo = model.TipoTituloRendaVariavel.GetHashCode().ToString();
                    view.CarregarTipoTitulo(ETipoAtivo.RendaVariavel);
                }
                view.Modulo = model.Modulo.GetHashCode().ToString();
                view.CarregarModulo();
                

                return Json(view);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult Excluir(int Id)
        {
            try
            {
                TitulosNegocio negocio = new TitulosNegocio();

                resultadoOperacao = negocio.Excluir(new Dominio.Model.Titulos() { Id = Id });

                return Json(resultadoOperacao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}