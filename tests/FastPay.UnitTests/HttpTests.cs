using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using FastPay.Application.DTO;
using Xunit;

namespace FastPay.UnitTests
{
    public class HttpTests
    {
        private static readonly JsonSerializerOptions SerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        [Fact]
        public async Task test()
        {
            const string url = "https://gorest.co.in/public/v1/users";
            var httpClient = new HttpClient();
            var data = await httpClient.GetFromJsonAsync<ApiResponse>(url, SerializerOptions);

            var response = await httpClient.PostAsJsonAsync(url, new { id = 1 });
        }

        class ApiResponse
        {
            public Data[] Data { get; set; }
        }

        class Data
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }
    }
}