using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vault.Entities
{
    [Table("beatmap_difficulty")]
    public class BeatmapDifficulty
    {
        [Key]
        [Column("md5")]
        public string Md5 { get; set; } = null!;
        
        [Column("diff_cs")]
        public double DiffCs { get; set; }
        
        [Column("diff_ar")]
        public double DiffAr { get; set; }
        
        [Column("diff_hp")]
        public double DiffHp { get; set; }
        
        [Column("diff_od")]
        public double DiffOd { get; set; }
        
        [Column("star_rating")]
        public double StarRating { get; set; }
        
        [Column("mod", TypeName = "int(11)")]
        public long Mod { get; set; }
        
        [Column("key_count", TypeName = "int(11)")]
        public int KeyCount { get; set; }
    }
}
