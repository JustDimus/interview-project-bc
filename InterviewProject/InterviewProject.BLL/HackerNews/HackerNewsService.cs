using InterviewProject.Core.Models;
using InterviewProject.DAL.HackerNews;

namespace InterviewProject.BLL.HackerNews;

public class HackerNewsService(IHackerNewsClient client) : IHackerNewsService
{
    public Task<IEnumerable<Story>> GetBestStoriesAsync(int count, CancellationToken cancellationToken)
        => client.GetBestStoriesAsync(count, cancellationToken);

    public Task FetchBestStoriesAsync(CancellationToken cancellationToken)
        => client.FetchBestStoriesAsync(cancellationToken);
}