using Liberta.Dominio.Model.Enumeradores;
using Liberta.Dominio.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Liberta.UI.Model
{
    public class CadastroTitulos
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo é obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo é obrigatório.")]
        public string PercentualRentabilidade { get; set; }
        [Required(ErrorMessage = "O campo é obrigatório.")]
        public string ValorInvestimentoMinimo { get; set; }
        public string DiasResgate { get; set; }
        [Required(ErrorMessage = "O campo é obrigatório.")]
        public string DataVencimento { get; set; }
        [Required(ErrorMessage = "O campo é obrigatório.")]
        public string PercentualIR { get; set; }
        [Required(ErrorMessage = "O campo é obrigatório.")]
        public string PercentualIOF { get; set; }
        [Required(ErrorMessage = "O campo é obrigatório.")]
        public string TipoAtivo { get; set; }
        public ETipoAtivo TipoAtivoEnum { get; set; }
        public List<SelectListItem> ddlTipoAtivo { get; set; } = new List<SelectListItem> { new SelectListItem { Value = ""} };
        [Required(ErrorMessage = "O campo é obrigatório.")]
        public string TipoTitulo { get; set; }
        public List<SelectListItem> ddlTipoTitulo { get; set; } = new List<SelectListItem> { new SelectListItem { Value = ""} };
        [Required(ErrorMessage = "O campo é obrigatório.")]
        public string Modulo { get; set; }
        public List<SelectListItem> ddlModulos { get; set; } = new List<SelectListItem> { new SelectListItem { Value = ""} };

        public List<CadastroTitulos> listaGrid { get; set; } = new List<CadastroTitulos>();

        public void CarregarTipoAtivo()
        {
            ddlTipoAtivo.Add(new SelectListItem() { Text = Util.GetDescription(ETipoAtivo.RendaFixa), Value = ETipoAtivo.RendaFixa.GetHashCode().ToString() });
            ddlTipoAtivo.Add(new SelectListItem() { Text = Util.GetDescription(ETipoAtivo.RendaVariavel), Value = ETipoAtivo.RendaVariavel.GetHashCode().ToString() });
        }

        public void CarregarTipoTitulo(ETipoAtivo tipo)
        {
            if(tipo == ETipoAtivo.RendaFixa)
            {
                ddlTipoTitulo.Add(new SelectListItem() { Text = Util.GetDescription(ETipoRendaFixa.CDB), Value = ETipoRendaFixa.CDB.GetHashCode().ToString() });
                ddlTipoTitulo.Add(new SelectListItem() { Text = Util.GetDescription(ETipoRendaFixa.TesouroDireto), Value = ETipoRendaFixa.TesouroDireto.GetHashCode().ToString() });
                ddlTipoTitulo.Add(new SelectListItem() { Text = Util.GetDescription(ETipoRendaFixa.LCILCA), Value = ETipoRendaFixa.LCILCA.GetHashCode().ToString() });
                ddlTipoTitulo.Add(new SelectListItem() { Text = Util.GetDescription(ETipoRendaFixa.LetrasCambio), Value = ETipoRendaFixa.LetrasCambio.GetHashCode().ToString() });
            }
            else if(tipo == ETipoAtivo.RendaVariavel)
            {
                ddlTipoTitulo.Add(new SelectListItem() { Text = Util.GetDescription(ETipoRendaVariavel.Acoes), Value = ETipoRendaVariavel.Acoes.GetHashCode().ToString() });
                ddlTipoTitulo.Add(new SelectListItem() { Text = Util.GetDescription(ETipoRendaVariavel.FundoImobiliario), Value = ETipoRendaVariavel.FundoImobiliario.GetHashCode().ToString() });
                ddlTipoTitulo.Add(new SelectListItem() { Text = Util.GetDescription(ETipoRendaVariavel.FundoInvestimento), Value = ETipoRendaVariavel.FundoInvestimento.GetHashCode().ToString() });
                ddlTipoTitulo.Add(new SelectListItem() { Text = Util.GetDescription(ETipoRendaVariavel.Criptomoedas), Value = ETipoRendaVariavel.Criptomoedas.GetHashCode().ToString() });
            }
        }

        public void CarregarModulo()
        {
            ddlModulos.Add(new SelectListItem() { Text = Util.GetDescription(EModulos.Modulo1), Value = EModulos.Modulo1.GetHashCode().ToString() });
            ddlModulos.Add(new SelectListItem() { Text = Util.GetDescription(EModulos.Modulo2), Value = EModulos.Modulo2.GetHashCode().ToString() });
            ddlModulos.Add(new SelectListItem() { Text = Util.GetDescription(EModulos.Modulo3), Value = EModulos.Modulo3.GetHashCode().ToString() });
            ddlModulos.Add(new SelectListItem() { Text = Util.GetDescription(EModulos.Modulo4), Value = EModulos.Modulo4.GetHashCode().ToString() });
        }
    }
}