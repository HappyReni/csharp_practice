using System.Text.Json.Serialization;

namespace ExerciseUI.Model
{
    internal class ExerciseModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("dateStart")]
        public DateTime DateStart { get; set; }
        [JsonPropertyName("dateEnd")]
        public DateTime DateEnd { get; set; }
        [JsonPropertyName("duration")]
        public TimeSpan Duration => DateStart - DateEnd;
        [JsonPropertyName("comment")]
        public string Comments { get; set; }
    
    }
}
