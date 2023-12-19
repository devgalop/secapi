using Microsoft.Extensions.DependencyInjection;
using SecurityApi.Infrastructure.Data.Interfaces;
using SecurityApi.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityApi.Infrastructure.Data.Extensions
{
    public static class RepositoryExtensions
    {
        public static void AddMockRepositories(this IServiceCollection services) 
        {
            services.AddSingleton<IUserRepository, MockUserRepository>();
            services.AddSingleton<ISesionRepository, MockSesionRepository>();
        }
    }
}
