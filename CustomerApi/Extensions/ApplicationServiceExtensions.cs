using BusinessLogic.Managers;
using Contracts;
using Contracts.Managers;
using Contracts.Repositories;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace CustomerApi.Extensions
{
	public static class ApplicationServiceExtensions
	{
		public static void ConfigureCors(this IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", builder => builder
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader());
			});
		}

		public static void AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddTransient<ICustomerRepository, CustomerRepository>();
			services.AddTransient<ICustomerManager, CustomerManager>();
			services.AddSwaggerGen();
		}

		public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
		{
			string connectionString = configuration.GetConnectionString("cnSqlServer");
			services.AddDbContext<CustomerContext>(options =>
				options.UseSqlServer(connectionString, b => b.MigrationsAssembly("CustomerApi")));
		}
	}
}
