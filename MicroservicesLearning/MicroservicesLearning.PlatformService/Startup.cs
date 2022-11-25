using MicroservicesLearning.PlatformService.AsyncDataServices;
using MicroservicesLearning.PlatformService.Data;
using MicroservicesLearning.PlatformService.SyncDataServices.Http;
using Microsoft.EntityFrameworkCore;

namespace MicroservicesLearning.PlatformService
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services,
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration)
        {
            if (webHostEnvironment.IsProduction())
            {
                Console.WriteLine($"--> Using SQL Server database");
                services.AddDbContext<AppDbContext>(options => 
                    options.UseSqlServer(configuration.GetConnectionString("PlatformsConnection")));
            }
            else
            {
                Console.WriteLine($"--> Using InMemory database");
                services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemory"));
            }
            
            services.RegisterRepositories();

            services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
            services.AddSingleton<IMessageBusClient, RabbitMqClient>();
            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        private static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPlatformRepository, PlatformRepository>();
        }

        public static void Configure(this WebApplication app)
        {
            Console.WriteLine($"--> Command Service uri: {app.Configuration["CommandServiceUri"]}");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            PrepDb.PrepPopulation(app, app.Environment.IsProduction());
        }
    }
}
