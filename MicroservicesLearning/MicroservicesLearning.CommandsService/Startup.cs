using MicroservicesLearning.CommandsService.AsyncDataServices;
using MicroservicesLearning.CommandsService.Attributes;
using MicroservicesLearning.CommandsService.Data;
using MicroservicesLearning.CommandsService.EventProcessing;
using MicroservicesLearning.CommandsService.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;

namespace MicroservicesLearning.CommandsService
{
    public static class Startup
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemory"));

            services.RegisterRepositories();

            services.AddControllers();
            services.AddScoped<CheckPlatformExistsServiceFilter>();
            services.AddSingleton<IEventProcessor, EventProcessor>();
            services.AddHostedService<RabbitMQSubscriber>();
            services.AddScoped<IPlatformDataClient, PlatformDataClient>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        private static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICommandRepo, CommandRepo>();
            services.AddScoped<IPlatformRepo, PlatformRepo>();
        }

        public static void Configure(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();

            PrepDb.PrepPopulation(app);
        }
    }
}
