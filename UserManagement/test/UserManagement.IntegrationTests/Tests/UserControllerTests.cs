using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
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
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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

    [Fact]
    public async Task GetUsers_WithFilter_ReturnsUser()
    {
        var filter = new UserFilterDto()
        {
            Email = "sanders"
        };
        var json = JsonConvert.SerializeObject(filter);
        var body = new StringContent(json, Encoding.UTF8, "application/json");
        
        var result = await _client.PostAsync("api/v1/Users/Filter",body)
            .Deserialize<PagedResponse<User>>();
        result.Should().NotBeNull();
        result?.Data.Count.Should().Be(2);
    }
}