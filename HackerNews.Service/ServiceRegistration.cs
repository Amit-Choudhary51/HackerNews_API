using HackerNews.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Service
{
    public static class ServiceRegistration
    {
        public static void Register(IServiceCollection services)
        {
            Repositories(services);
            Services(services);
        }

        private static void Repositories(IServiceCollection services)
        {
            services.AddTransient<INewStoriesRepository, NewStoriesRepository>();
        }
        private static void Services(IServiceCollection services)
        {
            services.AddTransient<INewStoriesService, NewStoriesService>();
        }
    }
}
