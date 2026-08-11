using Application.Behaviors;
using Application.Mapper;
using Application.Services.BackGroundService;
using Application.Services.BackGroundServices;
using Application.Services.BackGroundServices.Queue;
using Application.Services.CurrentUser;
using MediatR;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Application.Features.Hotels.Command.Validation;
using FluentValidation;
using Application.FileUpload;
namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration Configuration)
        {


            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PresetBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));




            services.AddScoped<ICurrentUserService, CurrentUserService>();


            services.AddValidatorsFromAssemblyContaining(typeof(UpdateHotelValidator));
            services.AddAutoMapper(cfg => { }, typeof(MapperProfile).Assembly);
           services.AddScoped<IFileUploadService, FileUploadService>();
            return services;

        }
    }
}
