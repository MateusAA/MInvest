using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;



namespace Liberta.UI.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "O campo RG é obrigatório.")]
        public string RG { get; set; }
        [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório.")]
        public string DataNascimento { get; set; }
        [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
        public string Telefone { get; set; }
        public Dominio.Model.Enumeradores.EPerfil Perfil { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal SaldoAtual { get; set; }

        public int FlgAtivo { get; set; }

        public List<Usuario> listaGrid { get; set; } = new List<Usuario>();
    }    
}
