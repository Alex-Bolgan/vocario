using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ReCallVocabulary.Data_Access
{
    public class Phrase
    {
        [Key]
        public int Id { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }
        public string[] Synonyms { get; set; }
        public DateOnly CreationDate { get; set; }
    }
}
