using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MiniProyectoBanking.Core.Application.Helpers;
using MiniProyectoBanking.Core.Application.Interfaces.Services;
using MiniProyectoBanking.Core.Application.Services;
using MiniProyectoBanking.Core.Application.ViewModels.Productos;
using MiniProyectoBanking.Core.Application.ViewModels.Usuarios;
using MiniProyectoBanking.Core.Application.Helpers;
using MiniProyectoBanking.Middlewares;

namespace MiniProyectoBanking.Controllers
{
    public class AdministrarUsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IProductoService _productoService;
        private readonly IMapper _mapper;
        private readonly ValidateUserSession _validateUserSession;

        public AdministrarUsuarioController(IUsuarioService usuarioService, IProductoService productoService, IMapper mapper, ValidateUserSession validateUserSession)
        {
            _usuarioService = usuarioService;
            _productoService = productoService;
            _mapper = mapper;
            _validateUserSession = validateUserSession;
        }
        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["ErrorMensaje"] = "No tienes permiso para acceder a estas secciones, tienes que iniciar sesión.";
                return RedirectToAction("Index", "Login");
            }

            var userType = _validateUserSession.GetUserType();

            if (userType == "Cliente")
            {
                TempData["ErrorMensaje"] = "No puedes acceder a esta sección.";
                return RedirectToAction("Index", "HomeUsuario");
            }

            var usuarios = await _usuarioService.GetAllViewModel();
            return View(usuarios);
        }

        public IActionResult SaveUsuario()
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["ErrorMensaje"] = "No tienes permiso para acceder a estas secciones, tienes que iniciar sesión.";
                return RedirectToAction("Index", "Login");
            }

            var userType = _validateUserSession.GetUserType();

            if (userType == "Cliente")
            {
                TempData["ErrorMensaje"] = "No puedes acceder a esta sección.";
                return RedirectToAction("Index", "HomeUsuario");
            }

            return View("SaveUsuario", new SaveUsuarioViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SaveUsuario(SaveUsuarioViewModel saveUsuarioViewModel)
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["ErrorMensaje"] = "No tienes permiso para acceder a estas secciones, tienes que iniciar sesión.";
                return RedirectToAction("Index", "Login");
            }

            var userType = _validateUserSession.GetUserType();

            if (userType == "Cliente")
            {
                TempData["ErrorMensaje"] = "No puedes acceder a esta sección.";
                return RedirectToAction("Index", "HomeUsuario");
            }

            if (!ModelState.IsValid)
            {
                return View(saveUsuarioViewModel);
            }

            await _usuarioService.Add(saveUsuarioViewModel);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditUsuario(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["ErrorMensaje"] = "No tienes permiso para acceder a estas secciones, tienes que iniciar sesión.";
                return RedirectToAction("Index", "Login");
            }

            var userType = _validateUserSession.GetUserType();

            if (userType == "Cliente")
            {
                TempData["ErrorMensaje"] = "No puedes acceder a esta sección.";
                return RedirectToAction("Index", "HomeUsuario");
            }

            var usuario = await _usuarioService.GetByIdSaveViewModel(id);
            if (usuario == null)
            {
                return NotFound();
            }

            var editViewModel = _mapper.Map<EditUsuarioViewModel>(usuario);
            return View(editViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsuario(EditUsuarioViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["ErrorMensaje"] = "No tienes permiso para acceder a estas secciones, tienes que iniciar sesión.";
                return RedirectToAction("Index", "Login");
            }

            var userType = _validateUserSession.GetUserType();

            if (userType == "Cliente")
            {
                TempData["ErrorMensaje"] = "No puedes acceder a esta sección.";
                return RedirectToAction("Index", "HomeUsuario");
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var usuarioActual = await _usuarioService.GetByIdEditViewModel(vm.Id);

            if (usuarioActual.NombreUsuario != vm.NombreUsuario)
            {
                var existingUser = await _usuarioService.GetByNombreUsuario(vm.NombreUsuario);
                if (existingUser != null && existingUser.Id != vm.Id)
                {
                    ModelState.AddModelError("NombreUsuario", "El nombre de usuario ya está en uso.");
                    return View(vm);
                }
            }

            await _usuarioService.UpdateUsuario(vm, vm.Id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Activacion(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["ErrorMensaje"] = "No tienes permiso para acceder a estas secciones, tienes que iniciar sesión.";
                return RedirectToAction("Index", "Login");
            }

            var userType = _validateUserSession.GetUserType();

            if (userType == "Cliente")
            {
                TempData["ErrorMensaje"] = "No puedes acceder a esta sección.";
                return RedirectToAction("Index", "HomeUsuario");
            }

            var usuario = await _usuarioService.GetByIdEditViewModel(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Activacion(UsuarioViewModel usuario)
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["ErrorMensaje"] = "No tienes permiso para acceder a estas secciones, tienes que iniciar sesión.";
                return RedirectToAction("Index", "Login");
            }

            var userType = _validateUserSession.GetUserType();

            if (userType == "Cliente")
            {
                TempData["ErrorMensaje"] = "No puedes acceder a esta sección.";
                return RedirectToAction("Index", "HomeUsuario");
            }

            var usuarioCompleto = await _usuarioService.GetByIdEditViewModel(usuario.Id);
            if (usuarioCompleto == null)
            {
                return NotFound();
            }

            usuarioCompleto.Estado = !usuarioCompleto.Estado;

            await _usuarioService.UpdateEstadoUsuario(usuarioCompleto.Id, usuarioCompleto.Estado);

            return RedirectToAction("Index");
        }

    }
}
