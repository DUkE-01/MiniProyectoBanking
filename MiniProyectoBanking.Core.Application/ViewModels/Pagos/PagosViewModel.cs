using System;
using System.Collections.Generic;

namespace MiniProyectoBanking.Core.Application.ViewModels.Pagos
{
    public class PagoViewModel
    {
        public DateTime Fecha { get; set; }
        public string TipoPago { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
    }

    public class PagosViewModel
    {
        public List<PagoViewModel> Pagos { get; set; }
        public string TipoPago { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
    }
}
