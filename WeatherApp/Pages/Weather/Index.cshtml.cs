using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeatherApp.Services;

namespace WeatherApp.Pages.Weather
{
    public class IndexModel : PageModel
    {
        private readonly WeatherService _weatherService;

        public IndexModel(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [BindProperty(SupportsGet = true)]
        public string? City { get; set; }

        public WeatherData? Weather { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(City)) return Page();

            Weather = await _weatherService.GetWeatherAsync(City);
            return Page();
        }
    }
}
