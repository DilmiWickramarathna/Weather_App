using Newtonsoft.Json;

namespace WeatherApp.Services
{
    public class WeatherService(IConfiguration configuration)
    {
#pragma warning disable CS8601 // Possible null reference assignment.
        private readonly string _apiKey = configuration["WeatherApi:ApiKey"];
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning disable CS8601 // Possible null reference assignment.
        private readonly string _baseUrl = configuration["WeatherApi:BaseUrl"];
#pragma warning restore CS8601 // Possible null reference assignment.
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<WeatherData?> GetWeatherAsync(string city)
        {
            var url = $"{_baseUrl}?q={city}&appid={_apiKey}&units=metric";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WeatherData>(json);
        }
    }

    public class WeatherData
    {
        public required MainData Main { get; set; }
        public string Name { get; set; } = string.Empty;
        public required WeatherDescription[] Weather { get; set; }
    }

    public class MainData
    {
        public double Temp { get; set; }
        public double Humidity { get; set; }
    }

    public class WeatherDescription
    {
        public string Description { get; set; } = string.Empty;
    }
}
