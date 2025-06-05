using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WeatherResponse
{
    public CurrentWeather Current { get; set; }
}

public class CurrentWeather
{
    public double Temp_C { get; set; }

    public WeatherCondition Condition { get; set; }
}

public class WeatherCondition
{
    public string Text { get; set; }
    public string Icon { get; set; }
    public int Code { get; set; }
}
