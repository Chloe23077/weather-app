using WeatherApp2.Services;

namespace WeatherApp2.Pages;

public partial class WeatherDetailPage : ContentPage
{
    private double latitude;
    private double longitude;
	public WeatherDetailPage()
	{
		InitializeComponent();
	}


    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await GetLocation();
        await GetWeatherDataByLocation(latitude, longitude); 
      

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

    public void UpdateUI(dynamic result)
    {
        LblCity.Text = result.city.name;
        LblWeatherDescription.Text = result.list[0].weather[0].description;
        LblTemperature.Text = result.list[0].main.temperature + "�C";
        ImgWeatherIcon.Source = result.list[0].weather[0].customIcon;
        LblSunrise.Text = result.city.UTCSunrise;
        LblSunset.Text = result.city.UTCSunset;
        LblPressure.Text = result.list[0].main.pressure + "hPa";
        LblSeaLevel.Text = result.list[0].main.sea_level + "m";
        LblMinTemp.Text = result.list[0].main.min_temperature + "�C";
        LblMaxTemp.Text = result.list[0].main.max_temperature + "�C";

    }
}