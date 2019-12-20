using EventBee.Data.Repositories;
using EventBee.Data.Repositories.Interfaces;
using EventBee.Repositories;
using EventBee.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBee.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IEventRepository, EventRepository>();

            services.AddScoped<IPlaceRepository, PlaceRepository>();

            services.AddScoped<ICommentRepository, CommentRepository>();

            services.AddScoped<IUserEventsRepository, UserEventsRepository>();

            return services;
        }
    }
}
