using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniProyectoBanking.Core.Application.Interfaces.Services;
using MiniProyectoBanking.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MiniProyectoBanking.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            #region Services
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IProductoService, ProductoService>();
            services.AddTransient<ITransaccionService, TransaccionService>();
            services.AddTransient<IBeneficiarioService, BeneficiarioService>();
            #endregion
        }
    }
}
