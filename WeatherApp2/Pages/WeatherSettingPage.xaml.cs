namespace WeatherApp2.Pages;

public partial class WeatherSettingPage : ContentPage
{
    public WeatherSettingPage()
    {
        InitializeComponent();
    }

    private void LightMode_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            ApplyLightTheme();
           
        }
    }

    private void DarkMode_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            ApplyDarkTheme();
        }
    }

    private void ApplyLightTheme()
    {
        ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if (mergedDictionaries != null)
        {
            mergedDictionaries.Clear();
            mergedDictionaries.Add(new LightTheme());
        }
    }
    private void ApplyDarkTheme()
    {
        ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if (mergedDictionaries != null)
        {
            mergedDictionaries.Clear();
            mergedDictionaries.Add(new DarkTheme());
        }
    }


}