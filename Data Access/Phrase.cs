using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ReCallVocabulary.Data_Access
{
    public class Phrase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public required string Term { get; set; }
        public required string Definition { get; set; }
        public string[]? Synonyms { get; set; }
        public string[]? Tags { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
