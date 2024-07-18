using MiniProyectoBanking.Core.Application.ViewModels.Usuarios;
using MiniProyectoBanking.Core.Application.Helpers;
using Microsoft.AspNetCore.Http;

namespace MiniProyectoBanking.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;  

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            UsuarioViewModel usuarioViewModel = _httpContextAccessor.HttpContext.Session.Get<UsuarioViewModel>("usuario");
            if (usuarioViewModel == null) 
            { 
                return false;
            }

            return true;
        }

        public string GetUserType()
        {
            UsuarioViewModel usuarioViewModel = _httpContextAccessor.HttpContext.Session.Get<UsuarioViewModel>("usuario");
            if (usuarioViewModel != null)
            {
                return usuarioViewModel.Tipo;
            }
            return null; 
        }
    }
}
