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
    public class BeneficiarioRepository : GenericRepository<Beneficiario>, IBeneficiarioRepository
    {
        private readonly ApplicationContext _dbContext;

        public BeneficiarioRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
