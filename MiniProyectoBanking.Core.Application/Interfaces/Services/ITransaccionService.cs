using MiniProyectoBanking.Core.Application.ViewModels.Transacciones;
using MiniProyectoBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProyectoBanking.Core.Application.Interfaces.Services
{
    public interface ITransaccionService : IGenericService<SaveTransaccionViewModel, TransaccionViewModel, Transaccion>
    {
        Task<int> GetTotalTransacciones();
        Task<int> GetTotalTransaccionesHoy();
    }
}
