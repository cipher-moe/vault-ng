using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace vault.Entities
{
    public class MostPlayedMapsAggregateRecord : Beatmap
    {
        [JsonProperty("count")] [Column("count")] public int Count { get; set; }
    }
}