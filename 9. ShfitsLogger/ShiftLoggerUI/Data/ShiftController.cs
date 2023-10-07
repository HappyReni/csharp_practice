using Microsoft.DotNet.MSIdentity.Shared;
using ShiftLoggerAPI.Models;
using ShiftLoggerUI.Models;
using System.Net.Http.Json;
using System.Text.Json;
using Shift = ShiftLoggerUI.Models.Shift;

namespace ShiftLoggerUI.Data
{
    public static class ShiftController
    {
        public static async Task<Shift> GetShift(int id)
        {
            var shift = new Shift();
            var endpoint = $"https://localhost:7040/api/Shifts/{id}";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStreamAsync();
                    shift = await JsonSerializer.DeserializeAsync<Shift>(json);
                    return shift;
                }
                return shift;
            }
        }
        public static async Task<List<Shift>> GetShifts()
        {
            var shifts = new List<Shift>();
            var endpoint = "https://localhost:7040/api/Shifts";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStreamAsync();
                    shifts = await JsonSerializer.DeserializeAsync<List<Shift>>(json);
                    return shifts;
                }
                return shifts;
            }
        }

        public static async void AddShift(Shift shift)
        {
            var endpoint = "https://localhost:7040/api/Shifts";

            using (HttpClient client = new HttpClient())
            {
                Task response = client.PostAsJsonAsync(endpoint, shift);
                response.Wait();
                if (response.IsCompletedSuccessfully)
                {
                    UI.Write("Successfully added.");
                }
                else
                {
                    UI.Write("Something went wrong.");
                }
            }
        }
        public static async void DeleteShift(int id)
        {
            var endpoint = $"https://localhost:7040/api/Shifts/{id}";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.DeleteAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    UI.Write("Successfully deleted.");
                }
                else
                {
                    UI.Write("Something went wrong.");
                }
            }
        }
    }
}
