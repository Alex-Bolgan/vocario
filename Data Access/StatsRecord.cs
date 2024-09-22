using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCallVocabulary.Data_Access
{
    public class StatsRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int AddedNumber { get; set; }

        public int UniqueRecalledNumber { get; set; }

        public int RecalledNumber { get; set; }
    }
}
