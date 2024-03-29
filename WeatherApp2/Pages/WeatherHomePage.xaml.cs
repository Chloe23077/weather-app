using WeatherApp2.Services;

namespace WeatherApp2.Pages;

public partial class WeatherHomePage : ContentPage
{
    public List<Models.List> WeatherList;
    private double latitude;
    private double longitude;
    public WeatherHomePage()
    {
        InitializeComponent();
        WeatherList = new List<Models.List>();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await GetLocation();
        
    }

    public async Task GetLocation()
    {
        var location = await Geolocation.GetLocationAsync();
        latitude = location.Latitude;
        longitude = location.Longitude;
        await GetWeatherDataByLocation(latitude, longitude);
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        var response = await DisplayPromptAsync(title: "", message: "", placeholder: "Search weather by city", accept: "Search", cancel: "Cancel");
        if (response != null)
        {
            await GetWeatherDataByCity(response);
        }
    }


    private async void TapLocation_Tapped(object sender, TappedEventArgs e)
    {
        await GetLocation();
        await GetWeatherDataByLocation(latitude, longitude);

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
        WeatherList.Clear();

        foreach (var item in result.list)
        {
            WeatherList.Add(item);
        }

        CvWeather.ItemsSource = WeatherList;
        LblCity.Text = result.city.name;
        LblWeatherDescription.Text = result.list[0].weather[0].description;
        LblTemperature.Text = result.list[0].main.temperature + "°C";
        LblHumidity.Text = result.list[0].main.humidity + "%";
        LblWind.Text = result.list[0].wind.speed + "km/h";
        ImgWeatherIcon.Source = result.list[0].weather[0].customIcon;
    }

}