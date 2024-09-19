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

        public DateTime Date;

        public int AddedNumber;

        public int UniqueRecalledNumber;

        public int RecalledNumber;
    }
}
