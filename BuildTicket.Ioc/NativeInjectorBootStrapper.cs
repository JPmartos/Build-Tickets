using BuildTicket.DataAccess.EntityFramework;
using BuildTicket.Domain.Contracts;
using BuildTicket.Domain.Implementations;
using BuildTicket.Repository.Contracts;
using BuildTicket.Repository.Implementations;
using BuildTicket.Services.Contracts;
using BuildTicket.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildTicket.Ioc
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(Microsoft.Extensions.DependencyInjection.IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<dbBuildTicketContext>(options => options.UseSqlServer(configuration.GetConnectionString("DevConnection")));
            services.AddScoped<ITicketDomain, TicketDomain>();
            services.AddScoped<IBuildRepository, BuildRepository>();
            services.AddScoped<IBuildTicketService, BuildTicketService>();

        }
    }
}
