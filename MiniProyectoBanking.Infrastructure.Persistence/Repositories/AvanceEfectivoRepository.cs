using MiniProyectoBanking.Core.Application.Interfaces.Repositories;
using MiniProyectoBanking.Core.Domain.Entities;
using MiniProyectoBanking.Infrastructure.Persistence.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace MiniProyectoBanking.Infrastructure.Persistence.Repositories
{
    public class AvanceEfectivoRepository : IAvanceEfectivoRepository
    {
        private readonly ApplicationContext _context;

        public AvanceEfectivoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<Transaccion> GetAvancesEfectivoByUserId(int userId)
        {
            return _context.Transacciones
                .Where(a => a.CuentaOrigen.ClienteId == userId && a.Tipo == "AvanceEfectivo")
                .ToList();
        }

        public void Add(Transaccion avanceEfectivo)
        {
            _context.Transacciones.Add(avanceEfectivo);
            _context.SaveChanges();
        }
    }
}
