using Microsoft.EntityFrameworkCore;
using MiniProyectoBanking.Core.Application.Interfaces.Repositories;
using MiniProyectoBanking.Core.Domain.Entities;
using MiniProyectoBanking.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProyectoBanking.Infrastructure.Persistence.Repositories
{
    public class ProductoRepository : GenericRepository<Producto>, IProductoRepository
    {
        private readonly ApplicationContext _dbContext;

        public ProductoRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExisteNumeroCuenta(string numeroCuenta)
        {
            return await _dbContext.Productos.AnyAsync(c => c.NumeroCuenta == numeroCuenta);
        }
        public async Task<int> CountProductos()
        {
            return await _dbContext.Productos.CountAsync();
        }
    }
}
