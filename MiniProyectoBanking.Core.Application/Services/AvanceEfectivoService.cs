using MiniProyectoBanking.Core.Application.ViewModels.AvancesEfectivo;
using System.Collections.Generic;

namespace MiniProyectoBanking.Core.Application.Interfaces.Services
{
    public class AvanceEfectivoService : IAvanceEfectivoService
    {
        private readonly ApplicationContext _context;

        public AvanceEfectivoService(ApplicationContext context)
        {
            _context = context;
        }

        public List<AvanceEfectivoViewModel> GetAvancesEfectivoByUserId(int userId)
        {
            return _context.AvancesEfectivo
                .Where(a => a.CuentaOrigen.ClienteId == userId)
                .Select(a => new AvanceEfectivoViewModel
                {
                    Id = a.Id,
                    Monto = a.Monto,
                    Fecha = a.Fecha,
                    CuentaOrigenId = a.CuentaOrigenId,
                    CuentaOrigen = a.CuentaOrigen
                }).ToList();
        }

        public void Add(AvanceEfectivoViewModel avanceEfectivo)
        {
            var entity = new AvancesEfectivo
            {
                Monto = avanceEfectivo.Monto,
                Fecha = avanceEfectivo.Fecha,
                CuentaOrigenId = avanceEfectivo.CuentaOrigenId
            };

            _context.AvancesEfectivo.Add(entity);
            _context.SaveChanges();
        }
    }
}

