using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Liberta.UI.Model
{
    public class TransferenciaViewModel
    {
        public int Id { get; set; }
        public Usuario UsuarioOrigem { get; set; } = new Usuario();
        public Usuario UsuarioDestino { get; set; } = new Usuario();
        public decimal ValorTransferido { get; set; }
        public List<SelectListItem> ddlUsuarioOrigem { get; set; } = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Selecione..." } };
        public List<SelectListItem> ddlUsuarioDestino { get; set; } = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Selecione..." } };

        public void CarregarUsuario(List<SelectListItem> ddl, List<Liberta.Dominio.Model.Usuario> lista)
        {
            foreach(var item in lista)
            {
                ddl.Add(new SelectListItem() { Text = item.Nome, Value = item.Id.ToString() });
            }
        }
    }
}