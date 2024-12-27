using Cefalo.Core.Common;
using Cefalo.Core.Repositories;
using Cefalo.Infrastructure.Data;
using Cefalo.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.Infrastructure.Extensions
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection
            , IConfiguration configuration
            )
        {
            //serviceCollection.Configure<DatabaseOptions>(configuration.GetSection(key: nameof(DatabaseOptions)));
            serviceCollection.AddScoped<IDapperContext, DapperContext>();
            serviceCollection.AddDbContext<AppDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnectionStrings")));
            serviceCollection.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            serviceCollection.AddScoped<IEmployeeRepository, EmployeeRepository>();
            return serviceCollection;
        }
    }
}
