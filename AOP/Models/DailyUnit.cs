namespace AOP.Models;

public class DailyUnit
{
    public string time { get; set; }
    public string weathercode { get; set; }
    public string temperature_2m_max { get; set; }
    public string temperature_2m_min { get; set; }
    public string apparent_temperature_max { get; set; }
    public string apparent_temperature_min { get; set; }
    public string sunrise { get; set; }
    public string sunset { get; set; }
    public string precipitation_sum { get; set; }
    public string rain_sum { get; set; }
    public string showers_sum { get; set; }
    public string snowfall_sum { get; set; }
    public string precipitation_hours { get; set; }
    public string windspeed_10m_max { get; set; }
    public string windgusts_10m_max { get; set; }
    public string winddirection_10m_dominant { get; set; }
}
