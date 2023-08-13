using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using BlazorApp1.Modals;
using BlazorApp1.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Pages
{
    public partial class ShowCards
    {
        private string SearchQuery;

        [Inject]
        private HttpClient Http { get; set; }

        [Inject]
        private SpinnerService SpinnerService { get; set; }


        private List<WeatherForecast>? forecasts;
        private CreateWeatherModal createWeatherModal;


        private Dictionary<int, string> statusList;


        protected override async Task OnInitializedAsync()
        {
            forecasts = await Http.GetFromJsonAsync<List<WeatherForecast>?>("sample-data/weather.json");

            statusList = new Dictionary<int, string>();
            statusList.Add(0, "گزینه یک");
            statusList.Add(1, "گزینه دو");
            statusList.Add(2, "گزینه سه");
        }

        private async Task GetForecasts(string searchText)
        {
            SpinnerService.Show();

            await Task.Delay(3000); // Simulate a delay

            var allForecasts = await Http.GetFromJsonAsync<List<WeatherForecast>?>("sample-data/weather.json");

            forecasts = allForecasts!.Where(a => a.Summary!.Contains(searchText)).ToList();

            SpinnerService.Hide();

        }

        private async Task GetFilteredForecasts()
        {

            var allForecasts = await Http.GetFromJsonAsync<List<WeatherForecast>?>("sample-data/weather.json");

            forecasts = allForecasts!.Where(a => a.Summary!.Contains(SearchQuery)).ToList();

        }

        private void OpenCreateModal()
        {
            createWeatherModal.Open();
        }

        private async Task HandleStatusChange(int status)
        {
           
        }
        private async Task HandleChangeStartDate(string date)
        {
           
        }
    }


    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public string? Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
