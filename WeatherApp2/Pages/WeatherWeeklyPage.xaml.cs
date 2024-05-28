using WeatherApp2.Services;
using WeeklyWeather.Services;

namespace WeatherApp2.Pages;

public partial class WeatherWeeklyPage : ContentPage
{
    public List<WeeklyWeather.Models.List> WeatherList;
    private double latitude;
	private double longitude;
	public WeatherWeeklyPage()
	{
		InitializeComponent();
        WeatherList = new List<WeeklyWeather.Models.List>();


    }
	protected async override void OnAppearing()
	{
		base.OnAppearing();
		await GetLocation();
		await GetWeatherDataByLocation(latitude, longitude);
		await GetWeeklyWeatherDataByLocation(latitude, longitude);

    }
	public async Task GetLocation()
	{
		var location = await Geolocation.GetLocationAsync();
		latitude = location.Latitude;
		longitude = location.Longitude;
	}
	public async Task GetWeatherDataByLocation(double latitude, double longitude)
	{
		var result = await ApiService.GetWeather(latitude, longitude);
		UpdateUI(result);
	}
	public async Task GetWeatherDataByCity(string city)
	{
		var result = await ApiService.GetWeatherByCity(city);
		UpdateUI(result);
	}

	//add
	public async Task GetWeeklyWeatherDataByLocation(double latitude, double longitude)
	{
		var result = await ApiServiceWeekly.GetWeeklyWeather(latitude, longitude);
		UpdateWeeklyUI(result);
	}

	public void UpdateUI(dynamic result)
	{
		
        LblCity.Text = result.city.name;
		LblWeatherDescription.Text = result.list[0].weather[0].description;
		LblTemperature.Text = result.list[0].main.temperature + "°C";
		ImgWeatherIcon.Source = result.list[0].weather[0].customIcon;
	}

	public void UpdateWeeklyUI(dynamic result)
	{
        WeatherList.Clear();

        foreach (var item in result.list)
        {
            WeatherList.Add(item);
        }

        WcvWeather.ItemsSource = WeatherList;
    }

}