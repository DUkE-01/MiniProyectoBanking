using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProyectoBanking.Core.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Correo { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string Tipo { get; set; }
        public bool Estado { get; set; }
        public ICollection<Beneficiario> Beneficiarios { get; set; }
        public ICollection<Producto> Productos { get; set; }
    }
}
