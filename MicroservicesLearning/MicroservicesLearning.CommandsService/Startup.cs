using MicroservicesLearning.CommandsService.Attributes;
using MicroservicesLearning.CommandsService.Data;
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
        }
    }
}
