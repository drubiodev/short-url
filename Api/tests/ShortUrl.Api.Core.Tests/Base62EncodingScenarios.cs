using FluentAssertions;
using ShortUrl.Core.Extensions;

namespace ShortUrl.Api.Core.Tests;

public class Base62EncodingScenarios
{
    [Theory]
    [InlineData(0, "0")]
    [InlineData(1, "1")]
    [InlineData(10, "A")]
    [InlineData(3844, "100")]
    public void Should_Encode_Number_To_Base62(int number, string expected)
    {
        number.ToBase62().Should().Be(expected);
    }
}