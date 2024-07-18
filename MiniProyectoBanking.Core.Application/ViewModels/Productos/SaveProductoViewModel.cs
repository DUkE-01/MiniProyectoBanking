using MiniProyectoBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProyectoBanking.Core.Application.ViewModels.Productos
{
    public class SaveProductoViewModel
    {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public bool EsPrincipal { get; set; }
        public decimal? Monto { get; set; }
        public decimal? Limite { get; set; }
        public decimal? Deuda { get; set; }
        public int ClienteId { get; set; }

    }
}
