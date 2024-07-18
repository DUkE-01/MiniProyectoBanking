using Microsoft.AspNetCore.Mvc;
using MiniProyectoBanking.Middlewares;

namespace MiniProyectoBanking.Controllers
{
    public class TransferenciaController : Controller
    {
        private readonly ValidateUserSession _validateUserSession;

        public TransferenciaController(ValidateUserSession validateUserSession)
        {
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
