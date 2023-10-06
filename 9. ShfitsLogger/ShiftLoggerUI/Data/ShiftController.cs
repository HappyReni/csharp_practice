using Microsoft.DotNet.MSIdentity.Shared;
using ShiftLoggerUI.Models;
using System.Text.Json;

namespace ShiftLoggerUI.Data
{
    public static class ShiftController
    {
        public static async Task<List<Shift>> GetShifts()
        {
            var shifts = new List<Shift>();
            var endpoint = "https://localhost:7040/api/Shifts";

            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStreamAsync();
                shifts = await JsonSerializer.DeserializeAsync<List<Shift>>(json);
                return shifts;
            }
            return shifts;
        }
    }
}
