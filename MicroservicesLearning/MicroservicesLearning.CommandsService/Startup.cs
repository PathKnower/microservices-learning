
using Microsoft.EntityFrameworkCore;

namespace MicroservicesLearning.CommandsService
{
    public static class Startup
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            //services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemory"));

            services.RegisterRepositories();

            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        private static void RegisterRepositories(this IServiceCollection services)
        {
            //services.AddScoped<IPlatformRepository, PlatformRepository>();
        }

        public static void Configure(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            //PrepDb.PrepPopulation(app);
        }
    }
}
