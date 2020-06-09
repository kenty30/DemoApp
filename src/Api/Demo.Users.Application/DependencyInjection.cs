using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AutoMapper;
using Demo.Users.Application.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Users.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationCommonServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            return services;
        }
    }
}
