using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Linq;
using SafeSpace.Pages;

namespace SafeSpace.ViewModel
{
    public class HelpPageViewModel
    {
        private readonly HttpClient _httpClient;

        public ObservableCollection<Professional> Professionals { get; set; } = new ObservableCollection<Professional>();

        public HelpPageViewModel()
        {
            
            _httpClient = new HttpClient();
            LoadProfessionals();
        }

        // Method to load professionals from backend
        public async Task LoadProfessionals()
        {
            try
            {
                var response = await _httpClient.GetAsync("http://localhost:5053/api/Professional");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var professionals = JsonSerializer.Deserialize<List<Professional>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                Professionals.Clear();
                if (professionals != null)
                {
                    foreach (var professional in professionals)
                    {
                        Professionals.Add(professional);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle error (e.g., log it or show an alert)
            }
        }
    }
}
