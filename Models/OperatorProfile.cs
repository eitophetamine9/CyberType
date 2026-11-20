using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace MidtermAppDev.Models
{
    [Table("profiles")]
    public class OperatorProfile : BaseModel
    {
        [PrimaryKey("id", false)]
        public long Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("callsign")]
        public string Callsign { get; set; } = string.Empty;

        [Column("difficulty")]
        public string Difficulty { get; set; } = "Medium";

        [Column("best_wpm")]
        public int BestWpm { get; set; }

        [Column("accuracy")]
        public decimal Accuracy { get; set; }

        [Column("high_score")]
        public int HighScore { get; set; }

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
