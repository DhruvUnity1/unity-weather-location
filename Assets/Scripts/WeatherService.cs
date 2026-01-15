using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using CleverTap.NativeToast;

// Fetches weather data
public class WeatherService : MonoBehaviour
{
    public void FetchWeather(float lat, float lon)
    {
        StartCoroutine(GetWeather(lat, lon));
    }

    private IEnumerator GetWeather(float lat, float lon)
    {
        string url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&daily=temperature_2m_max&timezone=auto";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                
                try
                {
                    WeatherData weatherData = JsonUtility.FromJson<WeatherData>(json);
                    
                    // Check if we got valid data
                    if (weatherData != null && weatherData.daily != null && 
                        weatherData.daily.temperature_2m_max != null && 
                        weatherData.daily.temperature_2m_max.Count > 0)
                    {
                        float temp = weatherData.daily.temperature_2m_max[0];
                        string unit = weatherData.daily_units.temperature_2m_max;
                        
                        // Show location and temperature
                        NativeToast.Show($"Lat: {lat:F2}, Lon: {lon:F2}\nTemp: {temp}{unit}");
                    }
                    else
                    {
                        NativeToast.Show("Invalid weather data");
                    }
                }
                catch (System.Exception e)
                {
                    NativeToast.Show($"Parse error: {e.Message}");
                    Debug.LogError($"Weather parsing error: {e.Message}");
                }
            }
            else
            {
                NativeToast.Show("Failed to fetch weather");
                Debug.LogError($"Weather API error: {request.error}");
            }
        }
    }
}