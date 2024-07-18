using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProyectoBanking.Core.Domain.Entities
{
    public class Beneficiario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NumeroCuenta { get; set; }
        public int ClienteId { get; set; }
        public Usuario Cliente { get; set; }
    }
}
