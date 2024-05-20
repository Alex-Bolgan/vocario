using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ReCallVocabulary.Data_Access
{
    public class Phrase
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Term { get; set; }
        public string Definition { get; set; }
        public string[] Synonyms { get; set; }
        public DateOnly CreationDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
