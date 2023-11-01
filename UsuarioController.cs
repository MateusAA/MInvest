using Liberta.Dominio.BLL;
using Liberta.Dominio.Utils;
using Liberta.UI.Controllers.BaseController;
using Liberta.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Liberta.UI.Controllers
{
    public class UsuarioController : Controller
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

            Usuario user = new Usuario();

            UsuarioNegocio negocio = new UsuarioNegocio();
            var listaModel = negocio.List(new Dominio.Model.Usuario());

            foreach (var model in listaModel)
            {
                var view = new Usuario();
                view.Id = model.Id;
                view.Nome = model.Nome;
                view.Email = model.Email;
                view.CPF = model.CPF;
                view.RG = model.RG;
                view.DataNascimento = model.DataNascimento.ToShortDateString();
                view.Telefone = model.Telefone;
                view.Perfil = model.Perfil;
                view.FlgAtivo = model.FlgAtivo;
                view.SaldoInicial = model.Saldo.ValorInicial;
                view.SaldoAtual = model.Saldo.ValorAtual;
                user.listaGrid.Add(view);
            }

            return View(user);
        }

        public JsonResult Registrar(Usuario view)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                 .Select(e => e.ErrorMessage)
                                                 .ToList();

                    return Json(new { success = false, errors = errors });
                }
                Liberta.Dominio.Model.Usuario model = new Dominio.Model.Usuario();
                model.Id = view.Id;
                model.Nome = view.Nome.ToUpper();
                model.Email = view.Email;
                model.Senha = view.Senha;
                model.CPF = view.CPF.Replace(".", "").Replace("-", "");
                model.RG = view.RG.Replace(".", "").Replace("-", "");
                model.DataNascimento = Convert.ToDateTime(view.DataNascimento);
                model.Telefone = view.Telefone.Replace("(", "").Replace(")", "").Replace("-", ""); 
                model.FlgAtivo = 1;
                model.Perfil = view.Perfil;

                UsuarioNegocio negocio = new UsuarioNegocio();

                resultadoOperacao = negocio.Gravar(model);

                return Json(resultadoOperacao);
            }
            catch (Exception ex)
            {
                    throw ex;
            }
        }

        public JsonResult Editar(int Id)
        {
            try
            {
                Usuario view = new Usuario();
                UsuarioNegocio negocio = new UsuarioNegocio();

                var model = negocio.GetItem(Id);

                view.Id = model.Id;
                view.Nome = model.Nome;
                view.Email = model.Email;
                view.Senha = model.Senha;
                view.CPF = model.CPF;
                view.RG = model.RG;
                view.DataNascimento = model.DataNascimento.ToShortDateString();
                view.Telefone = model.Telefone;
                view.Perfil = model.Perfil;

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
                UsuarioNegocio negocio = new UsuarioNegocio();

                resultadoOperacao = negocio.Excluir(new Dominio.Model.Usuario() { Id = Id });

                return Json(resultadoOperacao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult Ativar(int Id)
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();

                resultadoOperacao = negocio.AtualizarSituacao(new Dominio.Model.Usuario() { Id = Id, FlgAtivo = 1 });

                return Json(resultadoOperacao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult Inativar(int Id)
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();

                resultadoOperacao = negocio.AtualizarSituacao(new Dominio.Model.Usuario() { Id = Id , FlgAtivo = 0});

                return Json(resultadoOperacao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}