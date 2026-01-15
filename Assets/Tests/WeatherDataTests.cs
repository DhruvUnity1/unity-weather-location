using NUnit.Framework;
using UnityEngine;

public class WeatherDataTests
{
    [Test]
    public void WeatherData_ParsesJsonCorrectly()
    {
        string json = @"{
            ""latitude"": 19.125,
            ""longitude"": 72.875,
            ""daily_units"": {
                ""time"": ""iso8601"",
                ""temperature_2m_max"": ""°C""
            },
            ""daily"": {
                ""time"": [""2022-11-29""],
                ""temperature_2m_max"": [32.0]
            }
        }";
        
        WeatherData data = JsonUtility.FromJson<WeatherData>(json);
        
        Assert.IsNotNull(data);
        Assert.AreEqual(19.125f, data.latitude);
        Assert.AreEqual(72.875f, data.longitude);
        Assert.AreEqual(32.0f, data.daily.temperature_2m_max[0]);
    }
}