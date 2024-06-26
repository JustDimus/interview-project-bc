using InterviewProject.API.Extensions;
using InterviewProject.API.HostedServices;
using InterviewProject.API.Middlewares;
using InterviewProject.BLL.HackerNews;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddLogging(builder => builder.AddConsole());

builder.Services
    .AddDistributedMemoryCache();

builder.Services
    .AddHackerNewsOptions();

builder.Services
    .AddControllers()
    .AddNewtonsoftJson();

builder.Services
    .AddScoped<ExceptionMiddleware>()
    .AddSingleton<HackerNewsFetchService>()
    .AddScoped<IHackerNewsService, HackerNewsService>();

builder.Services
    .AddHostedService<HackerNewsFetchService>();

builder.Services
    .AddHackerNewsClient();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();