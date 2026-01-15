using System;
using System.Collections.Generic;

// Models
[Serializable]
public class WeatherData
{
    public float latitude;
    public float longitude;
    public DailyUnits daily_units;
    public DailyData daily;
}

[Serializable]
public class DailyUnits
{
    public string time;
    public string temperature_2m_max;
}

[Serializable]
public class DailyData
{
    public List<string> time;
    public List<float> temperature_2m_max;
}