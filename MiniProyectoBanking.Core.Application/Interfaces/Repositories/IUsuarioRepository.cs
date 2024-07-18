using MiniProyectoBanking.Core.Application.ViewModels.Usuarios;
using MiniProyectoBanking.Core.Domain.Entities;

namespace MiniProyectoBanking.Core.Application.Interfaces.Repositories
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<Usuario> LoginAsync(LoginViewModel LoginVm);
        Task<Usuario> GetByNombreUsuario(string nombreUsuario);
        Task<Usuario> GetUsuarioById(int usuarioId);
        Task<int> CountUsuariosByEstado(bool estado);

    }

}

