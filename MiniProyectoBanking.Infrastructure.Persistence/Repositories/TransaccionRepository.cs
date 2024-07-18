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
    public class TransaccionRepository : GenericRepository<Transaccion>, ITransaccionRepository
    {
        private readonly ApplicationContext _dbContext;

        public TransaccionRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CountTransacciones()
        {
            return await _dbContext.Transacciones.CountAsync();
        }

        public async Task<int> CountTransaccionesHoy()
        {
            var today = DateTime.Today;
            return await _dbContext.Transacciones.CountAsync(t => t.Fecha.Date == today);
        }
    }
}
