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
    public class TransferenciaController : Controller
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

            TransferenciaViewModel view = new TransferenciaViewModel();

            UsuarioNegocio negocio = new UsuarioNegocio();
            var listaModel = negocio.List(new Dominio.Model.Usuario());

            view.CarregarUsuario(view.ddlUsuarioOrigem, listaModel);
            view.CarregarUsuario(view.ddlUsuarioDestino, listaModel);

            return View(view);
        }

        public JsonResult CarregarValorOrigem(int Id)
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                var model = negocio.GetItem(Id);

                return Json(model.Saldo.ValorAtual.ToString("c2"));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult CarregarValorDestino(int Id)
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                var model = negocio.GetItem(Id);

                return Json(model.Saldo.ValorAtual.ToString("c2"));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult Transferir(TransferenciaViewModel view)
        {
            try
            {
                TransferenciaNegocio negocio = new TransferenciaNegocio();
                Liberta.Dominio.Model.Transferencia model = new Dominio.Model.Transferencia();

                if (view.UsuarioOrigem.Id == 0 || view.UsuarioDestino.Id == 0)
                {
                    return Json(resultadoOperacao.Adicionar("É necessário haver uma origem e um destino", Dominio.Model.Enumeradores.EResultadoOperacao.Atencao));
                }

                model.IdUsuarioOrigem = view.UsuarioOrigem.Id;
                model.IdUsuarioDestino = view.UsuarioDestino.Id;
                model.ValorTransferido = view.ValorTransferido;

                resultadoOperacao = negocio.Tranferir(model);

                UsuarioNegocio negocioUsuario = new UsuarioNegocio();
                var usuario = negocioUsuario.GetItem(Convert.ToInt32(Cookie.Get("IdUsuario")));
                Cookie.Set("SaldoAtualUsuario", usuario.Saldo.ValorAtual.ToString("c"));

                return Json(resultadoOperacao);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}