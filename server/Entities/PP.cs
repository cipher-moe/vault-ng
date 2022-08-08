using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vault.Entities
{
    [Table("pp")]
    public class PP
    {
        [Key]
        [Column("replay_sha256")]
        public string ReplaySha256 { get; set; } = null!;
        
        [Column("pp")]
        public double Performance { get; set; }
    }
}
