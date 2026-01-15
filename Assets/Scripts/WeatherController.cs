using UnityEngine;
using UnityEngine.UI;

// Main controller that connects location and weather services
public class WeatherController : MonoBehaviour
{
    [SerializeField] private LocationService locationService;
    [SerializeField] private WeatherService weatherService;
    [SerializeField] private Button fetchButton;

    private void Awake()
    {
        // Subscribe to location updates
        locationService.OnLocationFetched += HandleLocationFetched;
    }

    private void Start()
    {
        fetchButton.onClick.AddListener(OnFetchWeatherClicked);
    }

    private void OnDestroy()
    {
        // Clean up to avoid memory leaks
        if (locationService != null)
        {
            locationService.OnLocationFetched -= HandleLocationFetched;
        }
    }

    private void OnFetchWeatherClicked()
    {
        locationService.FetchLocation();
    }

    private void HandleLocationFetched(float lat, float lon)
    {
        // Got location, now fetch weather
        weatherService.FetchWeather(lat, lon);
    }
}