using InterviewProject.Core.Models;

namespace InterviewProject.BLL.HackerNews;

public interface IHackerNewsService
{
    Task<IEnumerable<Story>> GetBestStoriesAsync(int count, CancellationToken cancellationToken);

    Task FetchBestStoriesAsync(CancellationToken cancellationToken);
}