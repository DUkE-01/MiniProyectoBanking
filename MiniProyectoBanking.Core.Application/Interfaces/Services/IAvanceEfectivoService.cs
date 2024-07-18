using MiniProyectoBanking.Core.Application.ViewModels.AvancesEfectivo;
using System.Collections.Generic;

namespace MiniProyectoBanking.Core.Application.Interfaces.Services
{
    public interface IAvanceEfectivoService
    {
        List<AvanceEfectivoViewModel> GetAvancesEfectivoByUserId(int userId);
        void Add(AvanceEfectivoViewModel avanceEfectivo);
    }
}
