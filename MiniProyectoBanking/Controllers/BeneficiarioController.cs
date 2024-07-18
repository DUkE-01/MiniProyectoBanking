using Microsoft.AspNetCore.Mvc;
using MiniProyectoBanking.Core.Application.ViewModels.Beneficiarios;
using MiniProyectoBanking.Middlewares;
using System;
using System.Collections.Generic;

namespace MiniProyectoBanking.Controllers
{
    public class BeneficiarioController : Controller
    {
        private readonly ValidateUserSession _validateUserSession;

        public BeneficiarioController(ValidateUserSession validateUserSession)
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

            // Aquí deberías obtener los datos de los beneficiarios del usuario y pasarlos al ViewModel.
            var beneficiariosViewModel = new BeneficiarioViewModel
            {
                Beneficiarios = new List<BeneficiarioViewModel>()

            };

            return View(beneficiariosViewModel);
        }

        [HttpPost]
        public IActionResult AgregarBeneficiario(BeneficiarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Aquí deberías manejar la lógica para agregar el nuevo beneficiario y guardar la información en la base de datos.
                TempData["SuccessMessage"] = "Beneficiario agregado con éxito.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Hubo un error al agregar el beneficiario. Por favor, intenta de nuevo.";
            return RedirectToAction("Index");
        }
    }
}
