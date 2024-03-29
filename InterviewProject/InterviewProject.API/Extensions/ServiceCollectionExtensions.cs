using InterviewProject.Core.Configuration;
using InterviewProject.DAL.HackerNews;
using Microsoft.Extensions.Options;

namespace InterviewProject.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHackerNewsOptions(this IServiceCollection services)
    {
        services.AddOptions<HackerNewsOptions>(nameof(HackerNewsOptions));

        return services;
    }

    public static void AddHackerNewsClient(this IServiceCollection services)
        => services.AddHttpClient<IHackerNewsClient, HackerNewsClient>((provider, client) =>
        {
            var hackerNewsOptions = provider.GetRequiredService<IOptions<HackerNewsOptions>>().Value;

            client.BaseAddress = new Uri(hackerNewsOptions.HackerNewsApiUrl);
        });

    private static void AddOptions<TOptions>(this IServiceCollection services, string configSectionPath) where TOptions : class
    {
        services
            .AddOptions<TOptions>()
            .BindConfiguration(configSectionPath)
            .ValidateOnStart();
    }
}