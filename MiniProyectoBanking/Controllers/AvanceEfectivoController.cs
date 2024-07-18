using Microsoft.AspNetCore.Mvc;
using MiniProyectoBanking.Core.Application.Interfaces.Services;
using MiniProyectoBanking.Core.Application.ViewModels.AvancesEfectivo;
using MiniProyectoBanking.Middlewares;
using System;
using System.Linq;

namespace MiniProyectoBanking.Controllers
{
    public class AvanceEfectivoController : Controller
    {
        private readonly ValidateUserSession _validateUserSession;
        private readonly IAvanceEfectivoService _avanceEfectivoService;

        public AvanceEfectivoController(ValidateUserSession validateUserSession, IAvanceEfectivoService avanceEfectivoService)
        {
            _validateUserSession = validateUserSession;
            _avanceEfectivoService = avanceEfectivoService;
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
            var avancesEfectivo = _avanceEfectivoService.GetAvancesEfectivoByUserId(userId);

            var avancesEfectivoViewModel = new AvanceEfectivoViewModel
            {
                AvancesEfectivo = avancesEfectivo.Select(a => new AvanceEfectivoViewModel
                {
                    Id = a.Id,
                    Monto = a.Monto,
                    Fecha = a.Fecha,
                    CuentaOrigenId = a.CuentaOrigenId,
                    CuentaOrigen = a.CuentaOrigen
                }).ToList()
            };

            return View(avancesEfectivoViewModel);
        }

        [HttpPost]
        public IActionResult RealizarAvanceEfectivo(AvanceEfectivoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var nuevoAvance = new AvanceEfectivoViewModel
                {
                    Monto = model.Monto,
                    Fecha = DateTime.Now,
                    CuentaOrigenId = model.CuentaOrigenId
                };

                _avanceEfectivoService.Add(nuevoAvance);

                TempData["SuccessMessage"] = "Avance de efectivo realizado con éxito.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Hubo un error al realizar el avance de efectivo. Por favor, intenta de nuevo.";
            return RedirectToAction("Index");
        }
    }
}
