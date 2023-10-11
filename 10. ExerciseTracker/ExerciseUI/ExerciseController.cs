using ExerciseUI.Model;
using System.Text.Json;

namespace ExerciseUI
{
    internal class ExerciseController
    {
        public static async Task<List<ExerciseModel>> GetExercises()
        {
            var endpoint = $"https://localhost:7077/api/ExerciseModels";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<List<ExerciseModel>>(json);
                }
                else throw new Exception("API connection error.");
            }
        }
    }
}
