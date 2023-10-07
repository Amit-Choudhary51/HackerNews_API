using HackerNews.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.Service
{
    /// <summary>
    /// This class represents to configure services.
    /// </summary>
    public static class ServiceRegistration
    {
        #region Public Methods
        /// <summary>
        /// Register services.
        /// </summary>
        /// <param name="services"></param>
        public static void Register(IServiceCollection services)
        {
            Repositories(services);
            Services(services);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Register repository classes.
        /// </summary>
        /// <param name="services"></param>
        private static void Repositories(IServiceCollection services)
        {
            services.AddTransient<INewStoriesRepository, NewStoriesRepository>();
        }

        /// <summary>
        /// Register service classes.
        /// </summary>
        /// <param name="services"></param>
        private static void Services(IServiceCollection services)
        {
            services.AddTransient<INewStoriesService, NewStoriesService>();
        }
        #endregion
    }
}
