using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace vault.Entities
{
    public class Replay
    {
        [JsonProperty("mode")]
        [Column("mode")]
        public int Mode { get; set; }

        [JsonProperty("version")]
        [Column("version")]
        public int Version { get; set; }

        [JsonProperty("beatmap_hash")]
        [Column("beatmap_hash")]
        public string BeatmapHash { get; set; } = "";

        [JsonProperty("player_name")]
        [Column("player_name")]
        public string PlayerName { get; set; } = "";

        [JsonProperty("replay_hash")]
        [Column("replay_hash")]
        public string? ReplayHash { get; set; } = "";

        [JsonProperty("count_300")]
        [Column("count_300")]
        public int Count300 { get; set; }

        [JsonProperty("count_100")]
        [Column("count_100")]
        public int Count100 { get; set; }

        [JsonProperty("count_50")]
        [Column("count_50")]
        public int Count50 { get; set; }

        [JsonProperty("count_geki")]
        [Column("count_geki")]
        public int CountGeki { get; set; }

        [JsonProperty("count_katsu")]
        [Column("count_katsu")]
        public int CountKatsu { get; set; }

        [JsonProperty("count_miss")]
        [Column("count_miss")]
        public int CountMiss { get; set; }

        [JsonProperty("score")]
        [Column("score")]
        public long Score { get; set; }

        [JsonProperty("max_combo")]
        [Column("max_combo")]
        public int MaxCombo { get; set; }

        [JsonProperty("perfect_combo")]
        [Column("perfect_combo")]
        public bool PerfectCombo { get; set; }

        [JsonProperty("mods")]
        [Column("mods")]
        public long Mods { get; set; }

        [JsonProperty("timestamp")]
        [Column("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("sha256")]
        [Column("sha256")]
        public string Sha256 { get; set; } = "";

        [NotMapped]
        [JsonProperty("accuracy")]
        public double Accuracy { get; set; }
        
        [JsonProperty("beatmap")]
        public Beatmap? Beatmap { get; set; }
        
        [JsonProperty("difficulty")]
        public BeatmapDifficulty? Difficulty { get; set; }
    }
}