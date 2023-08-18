using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using BlazorApp1.Modals;
using BlazorApp1.Services;
using BlazorApp1.Shared.Models;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.DropDowns;

namespace BlazorApp1.Pages
{
    public partial class ShowCards
    {
        private string SearchQuery;
        private int temperatureC;

        [Parameter]
        public int TemperatureC
        {
            get => temperatureC;
            set
            {

                if (temperatureC == value || value == 0) return;
                temperatureC = value;
                CenterIdChanged.InvokeAsync(value);
            }
        }

        [Parameter] public EventCallback<int> CenterIdChanged { get; set; }

        [Inject]
        private HttpClient Http { get; set; }

        [Inject]
        private SpinnerService SpinnerService { get; set; }


        [Inject]
        private NotificationService NotificationService { get; set; }


        private List<WeatherForecast> forecasts = new();
        private List<WeatherForecast> mainForecastsList = new();
        private CreateWeatherModal createWeatherModal;

        private int take = 18;
        private int skip = 0;


        private readonly Dictionary<int, string> statusList = new();


        protected override async Task OnInitializedAsync()
        {
            //forecasts = await Http.GetFromJsonAsync<List<WeatherForecast>?>("sample-data/weather.json");
            
            for (int i = 0; i < 1000; i++)
            {
                mainForecastsList.Add(new WeatherForecast()
                {
                    TemperatureC = 10,
                    Summary = "sad",
                    Date = DateOnly.MinValue
                });
            }


            forecasts = mainForecastsList.Take(take).ToList();

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

            //var allForecasts = await Http.GetFromJsonAsync<List<WeatherForecast>?>("sample-data/weather.json");

            //forecasts = allForecasts!.Where(a => a.Summary!.Contains(SearchQuery)).ToList();


            skip += take;

            var results = mainForecastsList.Skip(skip).Take(take).ToList();

            forecasts.AddRange(results);

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

        public void OnValueChange(ChangeEventArgs<int, WeatherForecast> args)
        {
            NotificationService.ShowSuccess();
            NotificationService.ShowError();
            Console.WriteLine("The DropDownList Value is: ", args.Value);
        }

        public class Games
        {
            public string ID { get; set; }
            public string Text { get; set; }
        }
        List<Games> LocalData = new List<Games> {
            new Games() { ID= "Game1", Text= "American Football" },
            new Games() { ID= "Game2", Text= "Badminton" },
            new Games() { ID= "Game3", Text= "Basketball" },
            new Games() { ID= "Game4", Text= "Cricket" },
            new Games() { ID= "Game5", Text= "Football" },
            new Games() { ID= "Game6", Text= "Golf" },
            new Games() { ID= "Game7", Text= "Hockey" },
            new Games() { ID= "Game8", Text= "Rugby"},
            new Games() { ID= "Game9", Text= "Snooker" },
            new Games() { ID= "Game10", Text= "Tennis"},
        };
    }


    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public string? Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
