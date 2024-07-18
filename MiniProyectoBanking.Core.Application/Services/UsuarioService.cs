using MiniProyectoBanking.Core.Application.Helpers;
using MiniProyectoBanking.Core.Application.Interfaces.Repositories;
using MiniProyectoBanking.Core.Application.Interfaces.Services;
using MiniProyectoBanking.Core.Application.ViewModels.Usuarios;
using MiniProyectoBanking.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MiniProyectoBanking.Core.Application.ViewModels.Productos;

namespace MiniProyectoBanking.Core.Application.Services
{
    public class UsuarioService : GenericService<SaveUsuarioViewModel, UsuarioViewModel, Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UsuarioViewModel _usuarioViewModel;
        private readonly IMapper _mapper;
        private readonly IProductoService _productoService;

        public UsuarioService(IUsuarioRepository usuarioRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper, IProductoService productoService) : base(usuarioRepository, mapper)
        {
            _usuarioRepository = usuarioRepository;
            _httpContextAccessor = httpContextAccessor;
            _usuarioViewModel = _httpContextAccessor.HttpContext.Session.Get<UsuarioViewModel>("usuario");
            _mapper = mapper;
            _productoService = productoService;
        }

        public async Task<UsuarioViewModel> Login(LoginViewModel loginVm)
        {
            Usuario usuario = await _usuarioRepository.LoginAsync(loginVm);

            if (usuario == null)
            {
                return null;
            }

            UsuarioViewModel usuarioVm = _mapper.Map<UsuarioViewModel>(usuario);

            return usuarioVm;
        }


        public async Task<UsuarioViewModel> GetByNombreUsuario(string nombreUsuario)
        {
            var usuario = await _usuarioRepository.GetByNombreUsuario(nombreUsuario);
            if (usuario == null) return null;

            return new UsuarioViewModel
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Correo = usuario.Correo,
                Cedula = usuario.Cedula,
                NombreUsuario = usuario.NombreUsuario,
                Estado= usuario.Estado,
                Tipo= usuario.Tipo,
            };
        }

        public async Task<EditUsuarioViewModel> GetByIdEditViewModel(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario == null)
            {
                return null;
            }

            return _mapper.Map<EditUsuarioViewModel>(usuario);
        }

        public override async Task<SaveUsuarioViewModel> Add(SaveUsuarioViewModel vm)
        {
            var usuarioEntity = _mapper.Map<Usuario>(vm);
            usuarioEntity = await _usuarioRepository.AddAsync(usuarioEntity);
            var usuarioViewModel = _mapper.Map<SaveUsuarioViewModel>(usuarioEntity);

            if (vm.Tipo == "Cliente" && vm.Monto.HasValue)
            {
                string numeroCuenta;
                do
                {
                    numeroCuenta = new Random().Next(100000000, 999999999).ToString();
                } while (await _productoService.ExisteNumeroCuenta(numeroCuenta));

                var cuentaViewModel = new SaveProductoViewModel
                {
                    NumeroCuenta = numeroCuenta,
                    TipoCuenta = "Cuenta de ahorro",
                    EsPrincipal = true,
                    Monto = vm.Monto.Value,
                    Limite = null,
                    Deuda = null,
                    ClienteId = usuarioViewModel.Id
                    
                };

                await _productoService.Add(cuentaViewModel);

            }

            return usuarioViewModel;
        }

        public async Task UpdateUsuario(EditUsuarioViewModel vm, int id)
        {
            var usuarioEntity = await _usuarioRepository.GetByIdAsync(id);
            if (usuarioEntity == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            usuarioEntity.Nombre = vm.Nombre;
            usuarioEntity.Apellido = vm.Apellido;
            usuarioEntity.Cedula = vm.Cedula;
            usuarioEntity.Correo = vm.Correo;
            usuarioEntity.NombreUsuario = vm.NombreUsuario;
            usuarioEntity.Estado = vm.Estado;

            if (!string.IsNullOrEmpty(vm.Contraseña))
            {
                usuarioEntity.Contraseña = PasswordInscryption.ComputeSha256Hash(vm.Contraseña);
            }

            await _usuarioRepository.UpdateAsync(usuarioEntity, id);

            if (vm.Tipo == "Cliente" && vm.MontoAdicional.HasValue)
            {
                var productos = await _productoService.GetAllByClienteId(vm.Id);
                var productoPrincipal = productos.FirstOrDefault(p => p.EsPrincipal);

                if (productoPrincipal != null)
                {
                    productoPrincipal.Monto += vm.MontoAdicional.Value;
                    var saveProductoViewModel = _mapper.Map<SaveProductoViewModel>(productoPrincipal);
                    await _productoService.Update(saveProductoViewModel, saveProductoViewModel.Id);
                }
            }

        }

        public async Task<int> GetTotalUsuariosActivos()
        {
            return await _usuarioRepository.CountUsuariosByEstado(true);
        }

        public async Task<int> GetTotalUsuariosInactivos()
        {
            return await _usuarioRepository.CountUsuariosByEstado(false);
        }
        public async Task UpdateEstadoUsuario(int usuarioId, bool estado)
        {
            var usuarioEntity = await _usuarioRepository.GetByIdAsync(usuarioId);
            if (usuarioEntity == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            usuarioEntity.Estado = estado;

            await _usuarioRepository.UpdateAsync(usuarioEntity, usuarioId);
        }

        public object GetUserById(string? currentUserId)
        {
            throw new NotImplementedException();
        }
    }

}