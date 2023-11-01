using Liberta.Dominio.BLL;
using Liberta.Dominio.Utils;
using Liberta.UI.Controllers.BaseController;
using Liberta.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Liberta.UI.Controllers
{
    public class CarteiraController : Controller
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

            InvestimentoViewModel viewModel = new InvestimentoViewModel();
            InvestimentoNegocio negocio = new InvestimentoNegocio();
            var listaModel = negocio.List(new Dominio.Model.Investimento() { Usuario = new Dominio.Model.Usuario() { Id = Convert.ToInt32(Cookie.Get("IdUsuario")) } });

            foreach (var model in listaModel.Where(x => x.DataResgate == DateTime.MinValue))
            {
                var view = new InvestimentoViewModel();
                view.Id = model.Id;
                view.NomeTitulo = model.Titulo.Nome;
                view.PercentualRentabilidadeTitulo = model.Titulo.PercentualRentabilidade.ToString();
                view.ValorInvestimentoMinimoTitulo = model.Titulo.ValorInvestimentoMinimo.ToString("c2");
                view.DiasResgateTitulo = model.Titulo.DiasResgate.ToString();
                view.DataVencimentoTitulo = model.Titulo.DataVencimento.ToShortDateString();
                view.DataInvestimento = model.DataInvestimento;
                view.ValorInvestimento = model.ValorInvestimento;
                viewModel.listaGrid.Add(view);
            }
            return View(viewModel);
        }

        public PartialViewResult AbrirResgatar(int IdInvestimento)
        {
            try
            {
                InvestimentoViewModel view = new InvestimentoViewModel();
                InvestimentoNegocio negocio = new InvestimentoNegocio();

                var model = negocio.GetItem(IdInvestimento);
                view.Id = model.Id;
                view.NomeTitulo = model.Titulo.Nome;
                view.PercentualRentabilidadeTitulo = model.Titulo.PercentualRentabilidade.ToString();
                view.DiasResgateTitulo = model.Titulo.DiasResgate.ToString();
                view.DataInvestimento = model.DataInvestimento;
                view.ValorInvestimento = model.ValorInvestimento;
                view.IRRF = model.Titulo.PercentualIR;
                view.IOF = model.Titulo.PercentualIOF;

                return PartialView("_Resgatar", view);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult Resgatar(InvestimentoViewModel view)
        {
            try
            {
                Liberta.Dominio.Model.Investimento model = new Dominio.Model.Investimento();
                model.Id = view.Id;

                UsuarioNegocio negocioUsuario = new UsuarioNegocio();
                InvestimentoNegocio negocio = new InvestimentoNegocio();

                resultadoOperacao = negocio.Resgatar(model);

                var usuario = negocioUsuario.GetItem(Convert.ToInt32(model.Usuario.Id));
                Cookie.Set("SaldoAtualUsuario", usuario.Saldo.ValorAtual.ToString("c"));

                return Json(resultadoOperacao);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}