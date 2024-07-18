using Microsoft.AspNetCore.Mvc;
using MiniProyectoBanking.Core.Application.Interfaces.Services;
using MiniProyectoBanking.Core.Application.ViewModels.Transacciones;
using MiniProyectoBanking.Middlewares;
using System.Linq;

namespace MiniProyectoBanking.Controllers
{
    public class TransaccionController : Controller
    {
        private readonly ValidateUserSession _validateUserSession;
        private readonly ITransaccionService _transaccionService;

        public TransaccionController(ValidateUserSession validateUserSession, ITransaccionService transaccionService)
        {
            _validateUserSession = validateUserSession;
            _transaccionService = transaccionService;
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

            var userId = _validateUserSession.GetUserId();
            var transacciones = _transaccionService.GetTransaccionesByUserId(userId);

            var transaccionesViewModel = new TransaccionViewModel
            {
                Transacciones = transacciones.Select(t => new TransaccionViewModel
                {
                    Id = t.Id,
                    Tipo = t.Tipo,
                    Monto = t.Monto,
                    Fecha = t.Fecha,
                    CuentaOrigenId = t.CuentaOrigenId,
                    CuentaOrigen = t.CuentaOrigen,
                    CuentaDestinoId = t.CuentaDestinoId,
                    CuentaDestino = t.CuentaDestino
                }).ToList()
            };

            return View(transaccionesViewModel);
        }

        [HttpPost]
        public IActionResult RealizarTransaccion(TransaccionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var nuevaTransaccion = new TransaccionViewModel
                {
                    Tipo = model.Tipo,
                    Monto = model.Monto,
                    Fecha = DateTime.Now,
                    CuentaOrigenId = model.CuentaOrigenId,
                    CuentaDestinoId = model.CuentaDestinoId
                };

                _transaccionService.Add(nuevaTransaccion);

                TempData["SuccessMessage"] = "Transacción realizada con éxito.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Hubo un error al realizar la transacción. Por favor, intenta de nuevo.";
            return RedirectToAction("Index");
        }
    }
}
