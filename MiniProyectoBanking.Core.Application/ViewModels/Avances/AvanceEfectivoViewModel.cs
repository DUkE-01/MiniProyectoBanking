using MiniProyectoBanking.Core.Application.ViewModels.Pagos;
using MiniProyectoBanking.Core.Application.ViewModels.Productos;
using System;

namespace MiniProyectoBanking.Core.Application.ViewModels.AvancesEfectivo
{
    public class AvanceEfectivoViewModel
    {
        public List<PagoViewModel> AvancesEfectivo { get; set; }
        public int Id { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public int CuentaOrigenId { get; set; }
        public ProductoViewModel CuentaOrigen { get; set; }
    }
}
