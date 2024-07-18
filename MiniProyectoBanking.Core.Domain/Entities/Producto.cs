using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProyectoBanking.Core.Domain.Entities
{
    public class Producto
    {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public bool EsPrincipal { get; set; }
        public decimal? Monto { get; set; }
        public decimal? Limite { get; set; }
        public decimal? Deuda { get; set; }
        public int ClienteId { get; set; }
        public Usuario Cliente { get; set; }
        public ICollection<Transaccion> TransaccionesOrigen { get; set; }
        public ICollection<Transaccion> TransaccionesDestino { get; set; }
    }
}
