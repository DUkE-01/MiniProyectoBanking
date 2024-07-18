using MiniProyectoBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProyectoBanking.Core.Application.Interfaces.Repositories
{
    public interface ITransaccionRepository : IGenericRepository<Transaccion>
    {
        Task<int> CountTransacciones();
        Task<int> CountTransaccionesHoy();
    }
}
