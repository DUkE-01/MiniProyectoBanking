using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProyectoBanking.Core.Application.Interfaces.Services;
using MiniProyectoBanking.Core.Application.ViewModels.Usuarios;
using MiniProyectoBanking.Core.Application.Helpers;

namespace MiniProyectoBanking.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public LoginController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginVm)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }

            UsuarioViewModel usuarioVm = await _usuarioService.Login(loginVm);

            if (usuarioVm != null)
            {
                if (usuarioVm.Estado)
                {
                    HttpContext.Session.Set<UsuarioViewModel>("usuario", usuarioVm);
                    HttpContext.Session.SetString("UserRole", usuarioVm.Tipo);

                    if (usuarioVm.Tipo == "Admin")
                    {
                        return RedirectToRoute(new { controller = "HomeAdmin", action = "Index" });
                    }
                    else if (usuarioVm.Tipo == "Cliente")
                    {
                        return RedirectToRoute(new { controller = "HomeUsuario", action = "Index" });
                    }
                    else
                    {
                        ModelState.AddModelError("userValidation", "Tipo de usuario no reconocido");
                    }
                }
                else
                {
                    ModelState.AddModelError("userValidation", "El usuario está inactivo, comuníquese con el Administrador");
                }
            }
            else
            {
                ModelState.AddModelError("userValidation", "Datos de acceso incorrectos");
            }

            return View(loginVm);
        }

        public IActionResult Registro()
        {
            return View("Registro", new SaveUsuarioViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Registro(SaveUsuarioViewModel saveUsuarioViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(saveUsuarioViewModel);
            }

            await _usuarioService.Add(saveUsuarioViewModel);

            return RedirectToAction("Index");
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("usuario");
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
    }
}
