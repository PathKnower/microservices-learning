using MicroservicesLearning.CommandsService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureServices();

var app = builder.Build();
app.Configure();

app.Run();