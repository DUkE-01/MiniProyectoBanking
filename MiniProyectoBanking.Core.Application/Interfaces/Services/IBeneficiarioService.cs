using MiniProyectoBanking.Core.Application.ViewModels.Beneficiarios;
using MiniProyectoBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProyectoBanking.Core.Application.Interfaces.Services
{
    public interface IBeneficiarioService : IGenericService<SaveBeneficiarioViewModel, BeneficiarioViewModel, Beneficiario>
    {

    }
}
