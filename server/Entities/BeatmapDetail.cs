using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vault.Entities
{
    [Table("beatmap_details")]
    public class BeatmapDetail
    {
        [Key]
        [Column("md5")]
        public string Md5 { get; set; } = null!;
        
        [Column("sha256")]
        public string Sha256 { get; set; } = null!;
        
        [Column("length", TypeName = "int(11)")]
        public int Length { get; set; }
        
        [Column("drain_length", TypeName = "int(11)")]
        public int DrainLength { get; set; }
        
        [Column("combo", TypeName = "int(11)")]
        public int Combo { get; set; }
        
        [Column("title_unicode", TypeName = "text")]
        public string TitleUnicode { get; set; } = null!;
        
        [Column("title", TypeName = "text")]
        public string Title { get; set; } = null!;
        
        [Column("artist_unicode", TypeName = "text")]
        public string ArtistUnicode { get; set; } = null!;
        
        [Column("artist", TypeName = "text")]
        public string Artist { get; set; } = null!;
        
        [Column("diff_name", TypeName = "text")]
        public string DiffName { get; set; } = null!;
        
        
        [Column("min_bpm")]
        public double MinBpm { get; set; }
        
        [Column("max_bpm")]
        public double MaxBpm { get; set; }
        
        [Column("count_objects")]
        public int CountObjects { get; set; }
    }
}
