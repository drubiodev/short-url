using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using ShortUrl.Core.Urls.Add;

namespace ShortUrl.Api.Tests;

public class AddUrlFeature : IClassFixture<ApiFixture>
{
  private readonly HttpClient _client;
  public AddUrlFeature(ApiFixture apiFixture)
  {
    _client = apiFixture.CreateClient();
  }

  [Fact]
  public async Task Given_long_url_Should_return_ShortUrl()
  {
    var response = await _client.PostAsJsonAsync<AddUrlRequest>("/api/urls", new AddUrlRequest(new Uri("https://example.com"), "test"));

    response.StatusCode.Should().Be(HttpStatusCode.Created);
    var addUrlResponse = await response.Content.ReadFromJsonAsync<AddUrlResponse>();
    addUrlResponse!.ShortUrl.Should().NotBeNull();
  }
}
