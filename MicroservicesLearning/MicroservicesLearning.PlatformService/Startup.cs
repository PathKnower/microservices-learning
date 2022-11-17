using MicroservicesLearning.PlatformService.Data;
using MicroservicesLearning.PlatformService.SyncDataServices.Http;
using Microsoft.EntityFrameworkCore;

namespace MicroservicesLearning.PlatformService
{
    public static class Startup
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemory"));
            services.RegisterRepositories();

            services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
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

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            PrepDb.PrepPopulation(app);
        }
    }
}
