namespace AOP.Models;

public class DailyForecast
{
    public List<string> time { get; set; }
    public List<int> weathercode { get; set; }
    public List<double> temperature_2m_max { get; set; }
    public List<double> temperature_2m_min { get; set; }
    public List<double> apparent_temperature_max { get; set; }
    public List<double> apparent_temperature_min { get; set; }
    public List<DateTime> sunrise { get; set; }
    public List<DateTime> sunset { get; set; }
    public List<double> precipitation_sum { get; set; }
    public List<double> rain_sum { get; set; }
    public List<double> showers_sum { get; set; }
    public List<double> snowfall_sum { get; set; }
    public List<double> precipitation_hours { get; set; }
    public List<double> windspeed_10m_max { get; set; }
    public List<double> windgusts_10m_max { get; set; }
    public List<double> winddirection_10m_dominant { get; set; }
}
