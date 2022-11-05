using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UserManagement.IntegrationTests.Extensions;

public static class HttpExtensions
{
    public static async Task<T> Deserialize<T>(this HttpResponseMessage message)
    {
        var content = await message.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(content);
    }
    
    public static async Task<T> Deserialize<T>(this Task<HttpResponseMessage> @this)
    {
        var message = await @this;
        var content = await message.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(content);
    }
}