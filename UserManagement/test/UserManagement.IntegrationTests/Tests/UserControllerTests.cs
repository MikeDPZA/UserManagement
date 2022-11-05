using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using UserManagement.Common.Dto.User;
using UserManagement.Common.Generic;
using UserManagement.IntegrationTests.Extensions;
using UserManagement.IntegrationTests.Utils;
using Xunit;

namespace UserManagement.IntegrationTests.Tests;

public class UserControllerTests : IClassFixture<IntegrationTestApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public UserControllerTests(IntegrationTestApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GetUsers_NoSpecifiedPaging_ReturnsLessThan10Users()
    {
        var result = await _client.GetAsync("api/v1/Users")
            .Deserialize<PagedResponse<User>>();
        result.Should().NotBeNull();
        result?.Data.Count.Should().BeLessThanOrEqualTo(10);
    }

    [Fact]
    public async Task GetUsers_PageSize1_ReturnsLessThan10Users()
    {
        var result = await _client.GetAsync("api/v1/Users?pageSize=1")
            .Deserialize<PagedResponse<User>>();
        result.Should().NotBeNull();
        result?.Data.Count.Should().Be(1);
    }
}