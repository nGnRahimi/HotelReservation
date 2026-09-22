using Domain.Models.Capacities;
using Domain.Models.HotelGalleries;
using Domain.Models.Hotels;
using Domain.Models.Prices;
using Domain.Models.Rooms;
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
            services.AddScoped<IHotelGalleryRepository, HotelGalleryRepository>();
            services.AddScoped<IRoomRepository,RoomRepository>();
            services.AddScoped<IRoomPriceRepository, RoomPriceRepository>();
            services.AddScoped<IRoomCapacityRepository, RoomCapacityRepository>();
            return services;


        }
    }
}
