# interview-project-bc
This project is an ASP.NET RESTful API that posses an ability to retrieve the details of the top rated stories (based on their score) from the Hacker News API.

In order to avoid the possibility of overloading of the Hacker News API a IDistributedCache was used. Applicaton uses asynchronous operations to parallel API calls.

## Features

- Asynchronyously fetches the top `n` best stories from Hacker News API
- Caches fetched stories in order to avoid duplicate operations
- Automatically updates internal best stories list in order to keep data valid

## System requirements
- .NET 8 SDK
- (Optional) Installed Git

## How to run the application
### 1. Clone repository
#### If you have git installed on your machine:
Use command line and clone repository to any target folder.

`git clone https://github.com/JustDimus/interview-project-bc.git`

#### If you do not have git installed:
1) Go to [repository](https://github.com/JustDimus/interview-project-bc) and download code as zip archve.
2) Unzip archive to any target folder.

### 2. Go to `InterviewProject` folder
### 3. Run application (using command line)
1) Restore packages

    `dotnet restore`
2) Build solution

    `dotnet build`

3) Run application

    `dotnet run`

## Endpoints

### GET /story/best/{count}
Get the best `count` stories from Hacker News.

#### Response
```
[
  {
    "postedBy": string,
    "time": "2024-03-30T00:03:49.015Z",
    "uri": string,
    "score": int,
    "title": string,
    "commentCount": int
  }
]
```
- `postedBy` - author of the story
- `time` - story creation time
- `uri` - story uri
- `score` - score of the story
- `title` - title of the story
- `commentsCount` - count of the comments of the story

## Enhancements and changes
- Improve caching approach, probably based on priorities because almost every request to the endpoint returns first few items.
- Upgrade refresh system. Keep cache data valid so new requests will no trigger it's update.
- Upgrade exception handling approach.
- Add retry policies for Hacker News endpoints calls.