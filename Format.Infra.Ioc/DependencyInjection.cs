using Format.Application.Interfaces;
using Format.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Format.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureIoC(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IDocumentoService, DocumentoService>();
            services.AddScoped<IReferenciaService, ReferenciaService>();
            services.AddScoped<ISecoesConteudoService, SecoesConteudoService>();
            services.AddScoped<ISecoesService, SecoesService>();

            return services;
        }
    }
}
