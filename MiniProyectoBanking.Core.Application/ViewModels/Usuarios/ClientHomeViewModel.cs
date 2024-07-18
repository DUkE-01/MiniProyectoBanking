using MiniProyectoBanking.Core.Application.ViewModels.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProyectoBanking.Core.Application.ViewModels.Usuarios
{
    public class ClientHomeViewModel
    {
        public UsuarioViewModel UsuarioViewModel { get; set; }
        public List<ProductoViewModel> Productos { get; set; }
    }
}
