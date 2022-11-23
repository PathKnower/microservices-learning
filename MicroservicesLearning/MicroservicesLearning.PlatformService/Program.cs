using MicroservicesLearning.PlatformService;

var builder = WebApplication.CreateBuilder(args);
Startup.ConfigureServices(builder.Services, 
    builder.Environment, 
    builder.Configuration);

var app = builder.Build();
app.Configure();

app.Run();
