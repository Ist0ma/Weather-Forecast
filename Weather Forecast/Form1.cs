using System.Text.Json;
using Weather_Forecast.Exceptions;
using Weather_Forecast.Models;
using Weather_Forecast.Providers;
using WeatherForecast.Models;

namespace Weather_Forecast
{
    public partial class Form1 : Form
    {
        private const string CitiesFilePath = "UA.json";

        private readonly Size cityPanelSize = new(300, 100);

        private WeatherProvider weatherProvider;

        private List<City> cities = new();

        private List<City> citiesToShow = new();

        private System.Timers.Timer UpdateTimer = new();

        public List<City> SelectedCities { get; set; } = new();

        public Form1(WeatherProvider weatherProvider)
        {
            this.weatherProvider = weatherProvider;
            LoadCities();
            InitializeComponent();
        }
        private void LoadCities()
        {
            List<City>? cities = new();
            using (StreamReader r = new(CitiesFilePath))
            {
                string json = r.ReadToEnd();
                cities = JsonSerializer.Deserialize<List<City>>(json);
            }
            if (cities != null)
            {
                if (cities.Count == 0)
                {
                    throw new CityNotFoundEnception();
                }
                this.cities = cities;
            }
            else
            {
                throw new CityNotFoundEnception();
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            int lastSelectedId = Properties.Settings.Default.LastCityIdUsed;
            if (lastSelectedId != -1)
            {
                City? city = cities.FirstOrDefault(c => c.Id == lastSelectedId);
                if (city != null)
                {
                    SelectedCities.Add(city);
                }
            }

            UpdateTimer.Interval = 5 * 60 * 1000;
            UpdateTimer.AutoReset = true;
            UpdateTimer.Elapsed += UpdateTimer_Elapsed;
            UpdateTimer.Start();

            await UpdateCitiesForecast();
        }

        private async void UpdateTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            await UpdateCitiesForecast();
        }

        private async Task UpdateCitiesForecast()
        {
            foreach (City city in SelectedCities)
            {
                if (city.LastUpdate < DateTime.Now.AddHours(-1))
                {
                    Responce<Weather> responce = await weatherProvider.GetForecast(city.Latitude, city.Longitude);
                    if (responce.IsSuccess)
                    {
                        city.CityWeather = new(responce.Item.Forecast);
                        city.Region = responce.Item.Location.Region;

                        if (city.CityWeather.DailyChanceOfRain > 20 && DateTime.Compare(city.LastNotified.Date, DateTime.Now.Date) != 0)
                        {
                            MessageBox.Show($"{city.Name} chance of rain - {city.CityWeather.DailyChanceOfRain}");
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                    city.LastUpdate = DateTime.Now;
                }
            }
            BuildForecastList();
        }

        private void CitySelectTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CitySelectTextBox.Text))
            {
                citiesToShow = cities.Where(c => c.AltName.ToLower().Contains(CitySelectTextBox.Text)).Take(10).ToList();

                if (citiesToShow.Any())
                {
                    SearchResultlistBox.Visible = true;
                    SearchResultlistBox.BringToFront();
                    SearchResultlistBox.Items.Clear();
                    SearchResultlistBox.Items.AddRange(citiesToShow.Select(c => c.Name).ToArray());

                    SearchResultlistBox.Height = SearchResultlistBox.Items.Count * SearchResultlistBox.ItemHeight;
                }
                else
                {
                    HideSearchListBox();
                }
            }
        }

        private async void SearchResultlistBox_SelectedValueChanged(object sender, EventArgs e)
        {
            City selectedCity = citiesToShow[SearchResultlistBox.SelectedIndex];
            SelectedCities.Add(selectedCity);

            Properties.Settings.Default.LastCityIdUsed = selectedCity.Id;
            Properties.Settings.Default.Save();

            CitySelectTextBox.Clear();

            HideSearchListBox();

            await UpdateCitiesForecast();

            BuildForecastList();
        }

        private void HideSearchListBox()
        {
            SearchResultlistBox.Visible = false;
        }

        private void BuildForecastList()
        {
            CitiesListPanel.Invoke((MethodInvoker)delegate
            {
                CitiesListPanel.Controls.Clear();
            });

            for (int i = 0; i < SelectedCities.Count; i++)
            {
                var city = SelectedCities[i];

                Panel panel = new();
                panel.BorderStyle = BorderStyle.None;
                panel.Size = cityPanelSize;
                panel.Location = new(0, i * cityPanelSize.Height);

                Label CityNameLabel = new();
                CityNameLabel.Text = $"{city.Name}";
                CityNameLabel.AutoSize = true;
                CityNameLabel.Font = new("Arial", 12);

                panel.Controls.Add(CityNameLabel);

                Label RegionNameLabel = new();
                RegionNameLabel.Text = $"{city.Region}";
                RegionNameLabel.Location = new(CityNameLabel.Width, 0);
                RegionNameLabel.AutoSize = true;

                panel.Controls.Add(RegionNameLabel);

                Label minTempLabel = new();
                minTempLabel.Text = $"MinTemp {city.CityWeather.MinTemp.ToString()}";
                minTempLabel.Location = new(0, 20);
                panel.Controls.Add(minTempLabel);

                Label maxTempLabel = new();
                maxTempLabel.Text = $"MaxTemp {city.CityWeather.MaxTemp.ToString()}";
                maxTempLabel.Location = new(0, 40);
                panel.Controls.Add(maxTempLabel);

                Label PercipLabel = new();
                PercipLabel.Text = $"Precipitation {city.CityWeather.Precip.ToString()}";
                PercipLabel.Location = new(150, 20);
                panel.Controls.Add(PercipLabel);


                Button CloseButton = new();
                CloseButton.Text = "\u2715";
                CloseButton.Size = new(25,25);
                CloseButton.Location = new(cityPanelSize.Width - CloseButton.Width, 0);
                CloseButton.Click += (s, e) => RemoveCity(city);
                panel.Controls.Add(CloseButton);

                CitiesListPanel.Invoke((MethodInvoker)delegate
                {
                    CitiesListPanel.Controls.Add(panel);
                });
            }

            Invoke((MethodInvoker)delegate
            {
                Height = CitiesListPanel.Height + CitiesListPanel.Top;
            });
        }

        private void RemoveCity(City city)
        {
            SelectedCities.Remove(city);
            BuildForecastList();
        }
    }
}
