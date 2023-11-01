using Liberta.Dominio.BLL;
using Liberta.Dominio.Utils;
using Liberta.UI.Controllers.BaseController;
using Liberta.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Liberta.UI.Controllers
{
    public class LoginController : Controller
    {
        ResultadoOperacao resultadoOperacao = new ResultadoOperacao();

        public ActionResult  Login()
        {
            Usuario view = new Usuario();

            return View(view);
        }

        public JsonResult Logar(Usuario view)
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                Liberta.Dominio.Model.Usuario model = new Dominio.Model.Usuario();
                model.Email = view.Email;
                model.Senha = view.Senha;

                var UsuarioValido = negocio.ValidaUsuario(model);

                if(UsuarioValido)
                {
                    var user = negocio.List(model).FirstOrDefault();

                    Cookie.Set("IdUsuario", user.Id.ToString());
                    Cookie.Set("NomeUsuario", user.Nome);
                    Cookie.Set("PerfilUsuario", user.Perfil.GetHashCode().ToString());
                    Cookie.Set("SaldoAtualUsuario", user.Saldo.ValorAtual.ToString("c"));

                    return Json(resultadoOperacao.Adicionar("OK", Dominio.Model.Enumeradores.EResultadoOperacao.Sucesso));
                }
                else
                {
                    return Json(resultadoOperacao.Adicionar("Usuário ou Senha inválidos.", Dominio.Model.Enumeradores.EResultadoOperacao.Atencao));
                }
            }
            catch (Exception ex)
            {                
                return Json(resultadoOperacao.Adicionar(ex.Message, Dominio.Model.Enumeradores.EResultadoOperacao.Erro));
            }
        }
        public ActionResult Logout()
        {
            // Limpar os cookies
            Cookie.Delete("IdUsuario");
            Cookie.Delete("NomeUsuario");
            Cookie.Delete("PerfilUsuario");
            Cookie.Delete("SaldoAtualUsuario");

            // Redirecionar para a página de login ou outra página de sua escolha
            return RedirectToAction("Login");
        }

        /*
        [HttpPost]
        public ActionResult Authenticate(User user)
        {
            
            if (user.UserName == "admin" && user.Password == "123456")
            {
                
                return RedirectToAction("", "");
            }

            
            ModelState.AddModelError("", "Credenciais inválidas");
            return View("Login");
        }*/
    }
    public class AccountController : Controller
    {
        // Outros métodos existentes...

        public ActionResult Cadastro()
        {
            return View();
        }

        /*[HttpPost]
        public ActionResult Create(User user)
        {
            // Lógica para criar o usuário no banco de dados
            // ...

            return RedirectToAction("Login");
        }*/
    }



}