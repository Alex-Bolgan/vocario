using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCallVocabulary.Data_Access
{
    public static class Model
    {
        public static bool PhraseExists(int id)
        {
            return App.ActiveContext.Phrases.Any(p => p.Id == id);
        }
        public static Phrase GetPhraseById(int id)
        {
            Phrase tmp = (Phrase)App.ActiveContext.Phrases.Find(id);
            return tmp != null ? tmp : null;
        }
        public static int GetIdByTerm(string term)
        {
            List<Phrase> listWithTerm = App.ActiveContext.Phrases.Where(p => p.Term == term).ToList();
            int id = listWithTerm.First().Id;
            return id;
        }
        public static int GetFirstIdWithDate(DateOnly date)
        {
            return App.ActiveContext.Phrases
                .Where(x => x.CreationDate > date)
                .First().Id;
        }
        public static void UpdatePhrase(Phrase phrase)
        {
            Phrase current = (Phrase)App.ActiveContext.Phrases.Find(phrase.Id);
            current.Term = phrase.Term;
            current.Definition = phrase.Definition;
            current.Synonyms = phrase.Synonyms;
            current.Tags = phrase.Tags;
            App.ActiveContext.SaveChanges();
        }
        public static bool IsEmpty()
        {
            return !App.ActiveContext.Phrases.Any();
        }
        public static int GetMaxId()
        {
            return App.ActiveContext.Phrases.Max(p=>p.Id);
        }
    }
}
