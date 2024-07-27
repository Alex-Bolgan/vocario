using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;
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
            return tmp ?? null;
        }

        public static int GetFirstIdWithDate(DateTime date)
        {
            Phrase? result = App.ActiveContext.Phrases
                .Where(x => x.CreationDate > date)
                .FirstOrDefault();
            if (result is null)
            {
                return GetMaxId();
            }

            return result.Id;
        }

        public static int GetMaxIdWithDate(DateTime date)
        {
            Phrase? result = App.ActiveContext.Phrases
                .Where(x => x.CreationDate > date)
                .OrderBy(p => p.Id)
                .LastOrDefault();
            if (result is null)
            {
                return GetMaxId();
            }

            return result.Id;
        }

        public static void UpdatePhrase(Phrase phrase)
        {
            Phrase current = (Phrase)App.ActiveContext?.Phrases.Find(phrase.Id);
            current.Term = phrase.Term;
            current.Definition = phrase.Definition;
            current.Synonyms = phrase.Synonyms;
            current.Tags = phrase.Tags;
            App.ActiveContext.SaveChanges();
        }

        public static void RemoveRange(List<Phrase> phrases)
        {
            App.ActiveContext?.Phrases.RemoveRange(phrases);
            App.ActiveContext?.SaveChanges();
        }

        public static bool IsEmpty()
        {
            return !App.ActiveContext.Phrases.Any();
        }
        public static int GetMaxId()
        {
            if (!IsEmpty())
            {
                return App.ActiveContext.Phrases.Max(p => p.Id);

            }
            return 0;
        }
        
        public static int GetMinId()
        {
            if (!IsEmpty())
            {
                return App.ActiveContext.Phrases.Min(p => p.Id);
            }
            return 0;
        }

        public static int GetTotalNumber()
        {
            return App.ActiveContext.Phrases.Count();
        }

        public static List<Phrase> SearchPhrases(string search)
        {
            List<Phrase> result = App.ActiveContext.Phrases.AsQueryable().Where(p => p.Term.Contains(search)).ToList();

            return result;
        }
        
        public static List<string> SearchTags(string search)
        {
            List<string> result = App.ActiveContext.Phrases.Where(p => p.Tags != null).AsEnumerable().SelectMany(p => p.Tags)
                .Distinct()
                .ToList();

            return result;
        }

        public static List<Phrase> SearchPhrasesWithTag(string tag)
        {
            List<Phrase> result = App.ActiveContext.Phrases
                .Where(p=> p.Tags != null)
                .AsEnumerable()
                .Where(p => p.Tags.Any(t => t.Contains(tag)))
                .ToList();

            return result;
        }

    }
}
