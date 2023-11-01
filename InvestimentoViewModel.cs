using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Liberta.UI.Model
{
    public class InvestimentoViewModel
    {
        public int Id { get; set; }
        public int IdTitulo { get; set; }
        public int IdUsuario { get; set; }
        public string NomeTitulo { get; set; }
        public string PercentualRentabilidadeTitulo { get; set; }
        public string DataVencimentoTitulo { get; set; }
        public string DiasResgateTitulo { get; set; }
        public string ValorInvestimentoMinimoTitulo { get; set; }
        public decimal ValorInvestimento { get; set; }
        public DateTime DataInvestimento { get; set; }

        public List<InvestimentoViewModel> listaGrid { get; set; } = new List<InvestimentoViewModel>();
        public decimal IRRF { get; set; }
        public decimal IOF { get; set; }
    }
}