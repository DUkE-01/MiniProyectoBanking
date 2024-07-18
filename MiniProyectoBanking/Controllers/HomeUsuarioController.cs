using Microsoft.AspNetCore.Mvc;
using MiniProyectoBanking.Core.Application.Interfaces.Services;
using MiniProyectoBanking.Core.Application.Services;
using MiniProyectoBanking.Core.Application.ViewModels.Productos;
using MiniProyectoBanking.Core.Application.ViewModels.Usuarios;
using MiniProyectoBanking.Middlewares;
using System.Linq;
using System.Security.Claims;

namespace MiniProyectoBanking.Controllers
{
    public class HomeUsuarioController : Controller
    {
        private readonly UsuarioService _userService;
        private readonly ProductoService _productService;
        private readonly ValidateUserSession _validateUserSession;

        public HomeUsuarioController(UsuarioService userService, ProductoService productService, ValidateUserSession validateUserSession)
        {
            _userService = userService;
            _productService = productService;
            _validateUserSession = validateUserSession;
        }

        public IActionResult Index()
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["ErrorMensaje"] = "No tienes permiso para acceder a estas secciones, tienes que iniciar sesión.";
                return RedirectToAction("Index", "Login");
            }

            var userType = _validateUserSession.GetUserType();

            if (userType == "Admin")
            {
                TempData["ErrorMensaje"] = "No puedes acceder a esta sección.";
                return RedirectToAction("Index", "HomeAdmin");
            }


            return View(); 
        }
    }
}
