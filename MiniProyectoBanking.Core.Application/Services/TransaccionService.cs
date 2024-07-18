using AutoMapper;
using Microsoft.AspNetCore.Http;
using MiniProyectoBanking.Core.Application.Helpers;
using MiniProyectoBanking.Core.Application.Interfaces.Repositories;
using MiniProyectoBanking.Core.Application.Interfaces.Services;
using MiniProyectoBanking.Core.Application.ViewModels.Transacciones;
using MiniProyectoBanking.Core.Application.ViewModels.Usuarios;
using MiniProyectoBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProyectoBanking.Core.Application.Services
{
    public class TransaccionService : GenericService<SaveTransaccionViewModel, TransaccionViewModel, Transaccion>, ITransaccionService
    {
        private readonly ITransaccionRepository _transaccionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UsuarioViewModel _usuarioViewModel;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;

        public TransaccionService(ITransaccionRepository transaccionRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper, IUsuarioRepository usuarioRepository) : base(transaccionRepository, mapper)
        {
            _transaccionRepository = transaccionRepository;
            _httpContextAccessor = httpContextAccessor;
            _usuarioViewModel = _httpContextAccessor.HttpContext.Session.Get<UsuarioViewModel>("usuario");
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
        }
        public async Task<int> GetTotalTransacciones()
        {
            return await _transaccionRepository.CountTransacciones();
        }

        public async Task<int> GetTotalTransaccionesHoy()
        {
            return await _transaccionRepository.CountTransaccionesHoy();
        }

    }
}
