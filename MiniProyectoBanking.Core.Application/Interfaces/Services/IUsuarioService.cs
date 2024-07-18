using MiniProyectoBanking.Core.Application.ViewModels.Usuarios;
using MiniProyectoBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProyectoBanking.Core.Application.Interfaces.Services
{
    public interface IUsuarioService : IGenericService<SaveUsuarioViewModel, UsuarioViewModel, Usuario>
    {
        Task<UsuarioViewModel> Login(LoginViewModel loginVm);
        Task<UsuarioViewModel> GetByNombreUsuario(string nombreUsuario);
        Task<EditUsuarioViewModel> GetByIdEditViewModel(int id);
        Task UpdateUsuario(EditUsuarioViewModel vm, int id);
        Task<int> GetTotalUsuariosActivos();
        Task<int> GetTotalUsuariosInactivos();
        Task UpdateEstadoUsuario(int usuarioId, bool estado);
    }
}
