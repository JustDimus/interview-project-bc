using InterviewProject.BLL.HackerNews;
using InterviewProject.Core.Exceptions;
using InterviewProject.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace InterviewProject.API.Controllers;

[Route("[controller]")]
[ApiController]
public class StoryController : ControllerBase
{
    private readonly IHackerNewsService _hackerNewsService;

    public StoryController(IHackerNewsService hackerNewsService)
    {
        _hackerNewsService = hackerNewsService;
    }

    [HttpGet("best/{count:int}")]
    public async Task<IEnumerable<Story>> GetBestStories(int count, CancellationToken cancellationToken)
    {
        if (count < 0)
        {
            throw new ServiceException(ServiceErrorType.InvalidBestStoriesCount);
        }

        var stories = await _hackerNewsService.GetBestStoriesAsync(count, cancellationToken);

        return stories;
    }
}