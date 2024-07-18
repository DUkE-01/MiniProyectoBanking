using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MiniProyectoBanking.Core.Application.Interfaces.Services;
using MiniProyectoBanking.Core.Application.ViewModels.Productos;
using MiniProyectoBanking.Middlewares;

namespace MiniProyectoBanking.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoService _productoService;
        private readonly IMapper _mapper;
        private readonly ValidateUserSession _validateUserSession;

        public ProductoController(IProductoService productoService, IMapper mapper, ValidateUserSession validateUserSession)
        {
            _productoService = productoService;
            _mapper = mapper;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index(int clienteId)
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

            var productos = await _productoService.GetAllByClienteId(clienteId);
            return View(productos);
        }

        public IActionResult SaveProducto(int clienteId)
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

            string numeroCuenta;
            do
            {
                numeroCuenta = new Random().Next(100000000, 999999999).ToString();
            } while (_productoService.ExisteNumeroCuenta(numeroCuenta).Result);

            var model = new SaveProductoViewModel
            {
                ClienteId = clienteId,
                NumeroCuenta = numeroCuenta
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveProducto(SaveProductoViewModel model)
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
                return View(model);
            }

            if (model.TipoCuenta == "Cuenta de ahorro")
            {
                if (string.IsNullOrEmpty(model.Monto.ToString()) || !decimal.TryParse(model.Monto.ToString(), out decimal montoDecimal) || montoDecimal < 0)
                {
                    ViewBag.ErrorMessage = "Por favor ingrese un monto válido para la cuenta de ahorro.";
                    return View(model); 
                }
            }
            else if (model.TipoCuenta == "Tarjeta de credito")
            {
                if (string.IsNullOrEmpty(model.Limite.ToString()) || !decimal.TryParse(model.Limite.ToString(), out decimal limiteDecimal) || limiteDecimal <= 12499)
                {
                    ViewBag.ErrorMessage = "Por favor ingrese un límite válido para la tarjeta de crédito, recuerde que el minimo es 12,500 pesos.";
                    return View(model); 
                }
            }
            else if (model.TipoCuenta == "Prestamo")
            {
                if (string.IsNullOrEmpty(model.Deuda.ToString()) || !decimal.TryParse(model.Deuda.ToString(), out decimal deudaDecimal) || deudaDecimal <= 9999)
                {
                    ViewBag.ErrorMessage = "Por favor ingrese una deuda válida para el préstamo, recuerde que el prestamo minimo es de 10,000 pesos.";
                    return View(model);
                }
            }

            await _productoService.Add(model);

            return RedirectToAction("Index", new { clienteId = model.ClienteId });
        }

        public async Task<IActionResult> DeleteProducto(int id)
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

            var producto = await _productoService.GetByIdSaveViewModel(id);
            if (producto == null)
            {
                return NotFound();
            }

            ViewBag.ClienteId = producto.ClienteId;
            return View(producto);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
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

            var producto = await _productoService.GetByIdSaveViewModel(id);
            if (producto == null)
            {
                return NotFound();
            }

            if (producto.EsPrincipal)
            {
                ViewBag.ClienteId = producto.ClienteId;
                ViewBag.ErrorMessage = "No se puede eliminar este producto porque es la cuenta principal.";
                return View("DeleteProducto", producto);
            }

            if (producto.Deuda.HasValue && producto.Deuda > 0)
            {
                ViewBag.ClienteId = producto.ClienteId;
                ViewBag.ErrorMessage = "No se puede eliminar este producto porque tiene una deuda pendiente.";
                return View("DeleteProducto", producto);
            }

            await _productoService.Delete(id);

            return RedirectToAction("Index", new { clienteId = producto.ClienteId });
        }
    }
}