using Microsoft.AspNetCore.Mvc;
using MiniProyectoBanking.Core.Application.ViewModels.Pagos;
using MiniProyectoBanking.Middlewares;
using System;
using System.Collections.Generic;

namespace MiniProyectoBanking.Controllers
{
    public class PagoController : Controller
    {
        private readonly ValidateUserSession _validateUserSession;

        public PagoController(ValidateUserSession validateUserSession)
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

            // Aquí deberías obtener los datos de los pagos del usuario y pasarlos al ViewModel.
            var pagosViewModel = new PagosViewModel
            {
                Pagos = new List<PagoViewModel>
                {
                    new PagoViewModel { Fecha = DateTime.Now, TipoPago = "Servicio", Monto = 100.00m, Descripcion = "Pago de luz" },
                    new PagoViewModel { Fecha = DateTime.Now.AddDays(-5), TipoPago = "Crédito", Monto = 200.00m, Descripcion = "Pago de tarjeta de crédito" }
                }
            };

            return View(pagosViewModel);
        }

        [HttpPost]
        public IActionResult RealizarPago(PagosViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Aquí deberías manejar la lógica para realizar el pago y guardar la información en la base de datos.
                TempData["SuccessMessage"] = "Pago realizado con éxito.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Hubo un error al realizar el pago. Por favor, intenta de nuevo.";
            return RedirectToAction("Index");
        }
    }
}
