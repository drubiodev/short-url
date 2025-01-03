using Microsoft.AspNetCore.Mvc.Testing;
using short_url;

namespace ShortUrl.Api.Tests;

// WebApplicationFactory creates an in-memory test server for integration testing
public class ApiFixture : WebApplicationFactory<IApiAssemblyMarker>
{

}

