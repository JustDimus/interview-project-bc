using InterviewProject.Core.Models;

namespace InterviewProject.DAL.HackerNews;

public interface IHackerNewsClient
{
    Task<IEnumerable<Story>> GetBestStoriesAsync(int count, CancellationToken cancellationToken);

    Task FetchBestStoriesAsync(CancellationToken cancellationToken);
}