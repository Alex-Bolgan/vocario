using System.ComponentModel.DataAnnotations;

namespace ReCallVocabulary.Data_Access
{
    public class StatsRecord
    {
        [Key]
        public DateTime Date { get; set; }

        public int AddedNumber { get; set; }

        public int UniqueRecalledNumber { get; set; }

        public int RecalledNumber { get; set; }
    }
}
