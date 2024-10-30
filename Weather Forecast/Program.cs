using Weather_Forecast.Providers;
using WeatherForecast.Services;

namespace Weather_Forecast
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            WeatherProvider weatherProvider = new WeatherApiProvider();
            Application.Run(new Form1(weatherProvider));
        }
    }
}