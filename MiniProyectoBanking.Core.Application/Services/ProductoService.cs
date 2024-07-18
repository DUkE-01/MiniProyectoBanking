using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MiniProyectoBanking.Core.Application.Helpers;
using MiniProyectoBanking.Core.Application.Interfaces.Repositories;
using MiniProyectoBanking.Core.Application.Interfaces.Services;
using MiniProyectoBanking.Core.Application.ViewModels.Productos;
using MiniProyectoBanking.Core.Application.ViewModels.Usuarios;
using MiniProyectoBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProyectoBanking.Core.Application.Services
{
    public class ProductoService : GenericService<SaveProductoViewModel, ProductoViewModel, Producto>, IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UsuarioViewModel _usuarioViewModel;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;

        public ProductoService(IProductoRepository productoRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper, IUsuarioRepository usuarioRepository) : base(productoRepository, mapper)
        {
            _productoRepository = productoRepository;
            _httpContextAccessor = httpContextAccessor;
            _usuarioViewModel = _httpContextAccessor.HttpContext.Session.Get<UsuarioViewModel>("usuario");
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> ExisteNumeroCuenta(string numeroCuenta)
        {
            return await _productoRepository.ExisteNumeroCuenta(numeroCuenta);
        }

        public async Task<List<ProductoViewModel>> GetAllByClienteId(int clienteId)
        {
            var productos = await _productoRepository.GetAllAsync();
            var productosCliente = productos.Where(p => p.ClienteId == clienteId).ToList();
            return _mapper.Map<List<ProductoViewModel>>(productosCliente);
        }

        public IEnumerable<object> GetProductsByUserId(string? currentUserId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetTotalProductos()
        {
            return await _productoRepository.CountProductos();
        }

    }
}
