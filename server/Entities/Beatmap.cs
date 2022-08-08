using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace vault.Entities
{
    public class Beatmap
    {
        [Key]
        [JsonProperty("md5")] [Column("md5")] public string BeatmapHash { get; set; } = "";
        [JsonProperty("beatmapId")] [Column("beatmapId")] public string? BeatmapId { get; set; }
        [JsonProperty("beatmapsetId")] [Column("beatmapsetId")] public string? BeatmapsetId  { get; set; }
        [JsonProperty("date")] [Column("date")] public DateTime Timestamp { get; set; }
        
        [JsonProperty("detail")] public BeatmapDetail? Detail { get; set; }
    }
}