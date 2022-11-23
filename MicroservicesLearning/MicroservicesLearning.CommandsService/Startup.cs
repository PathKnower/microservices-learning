
using Microsoft.EntityFrameworkCore;

namespace MicroservicesLearning.CommandsService
{
    public static class Startup
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.RegisterRepositories();

            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        private static void RegisterRepositories(this IServiceCollection services)
        {
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
