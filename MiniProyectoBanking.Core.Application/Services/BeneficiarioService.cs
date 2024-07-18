using AutoMapper;
using Microsoft.AspNetCore.Http;
using MiniProyectoBanking.Core.Application.Helpers;
using MiniProyectoBanking.Core.Application.Interfaces.Repositories;
using MiniProyectoBanking.Core.Application.Interfaces.Services;
using MiniProyectoBanking.Core.Application.ViewModels.Beneficiarios;
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
    public class BeneficiarioService : GenericService<SaveBeneficiarioViewModel, BeneficiarioViewModel, Beneficiario>, IBeneficiarioService
    {
        private readonly IBeneficiarioRepository _beneficiarioRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UsuarioViewModel _usuarioViewModel;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;

        public BeneficiarioService(IBeneficiarioRepository beneficiarioRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper, IUsuarioRepository usuarioRepository) : base(beneficiarioRepository, mapper)
        {
            _beneficiarioRepository = beneficiarioRepository;
            _httpContextAccessor = httpContextAccessor;
            _usuarioViewModel = _httpContextAccessor.HttpContext.Session.Get<UsuarioViewModel>("usuario");
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
        }
    }
}
