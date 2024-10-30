namespace Weather_Forecast.Exceptions
{
    public class CityNotFoundEnception : Exception
    {
        public CityNotFoundEnception()
        { }

        public CityNotFoundEnception(string message)
            : base(message)
        { }

        public CityNotFoundEnception(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
