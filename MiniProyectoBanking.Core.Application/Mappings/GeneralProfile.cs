using AutoMapper;
using MiniProyectoBanking.Core.Application.ViewModels.Beneficiarios;
using MiniProyectoBanking.Core.Application.ViewModels.Productos;
using MiniProyectoBanking.Core.Application.ViewModels.Transacciones;
using MiniProyectoBanking.Core.Application.ViewModels.Usuarios;
using MiniProyectoBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProyectoBanking.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<EditUsuarioViewModel, SaveUsuarioViewModel>();
            CreateMap<SaveUsuarioViewModel, EditUsuarioViewModel>();

            CreateMap<SaveUsuarioViewModel, Usuario>();

            CreateMap<Usuario, UsuarioViewModel>();

            CreateMap<Usuario, SaveUsuarioViewModel>()
                .ForMember(dest => dest.ConfirmarContraseña, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Beneficiarios, opt => opt.Ignore())
                .ForMember(dest => dest.Productos, opt => opt.Ignore());

            CreateMap<Usuario, EditUsuarioViewModel>();
            CreateMap<EditUsuarioViewModel, Usuario>();

            CreateMap<Producto, ProductoViewModel>();

            CreateMap<ProductoViewModel, SaveProductoViewModel>();

            CreateMap<Producto, SaveProductoViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.TransaccionesOrigen, opt => opt.Ignore())
                .ForMember(dest => dest.TransaccionesDestino, opt => opt.Ignore());

            CreateMap<Transaccion, TransaccionViewModel>();

            CreateMap<Transaccion, SaveTransaccionViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.CuentaOrigen, opt => opt.Ignore())
                .ForMember(dest => dest.CuentaDestino, opt => opt.Ignore());

            CreateMap<Beneficiario, BeneficiarioViewModel>();

            CreateMap<Beneficiario, SaveBeneficiarioViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Cliente, opt => opt.Ignore());
        }
    }
}
