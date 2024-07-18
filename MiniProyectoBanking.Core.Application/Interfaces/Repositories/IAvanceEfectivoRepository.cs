using MiniProyectoBanking.Core.Domain.Entities;
using System.Collections.Generic;

namespace MiniProyectoBanking.Core.Application.Interfaces.Repositories
{
    public interface IAvanceEfectivoRepository
    {
        List<Transaccion> GetAvancesEfectivoByUserId(int userId);
        void Add(Transaccion avanceEfectivo);
    }
}
