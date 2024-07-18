using MiniProyectoBanking.Core.Application.ViewModels.Productos;
using MiniProyectoBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProyectoBanking.Core.Application.Interfaces.Services
{
    public interface IProductoService : IGenericService<SaveProductoViewModel, ProductoViewModel, Producto>
    {
        Task<bool> ExisteNumeroCuenta(string numeroCuenta);
        Task<List<ProductoViewModel>> GetAllByClienteId(int clienteId);
        Task<int> GetTotalProductos();
    }
}
