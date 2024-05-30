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
        public static int GetFirstIdWithDate(DateOnly date)
        {
            return App.ActiveContext.Phrases
                .Where(x => x.CreationDate > date)
                .First().Id;
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
