using Domain.Models.Hotels;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration Configuration)

        {
            services.AddDbContext<ApplicationDbContext>(option =>
            option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IHotelRepository, HotelRepository>();

            return services;


        }
    }
}
