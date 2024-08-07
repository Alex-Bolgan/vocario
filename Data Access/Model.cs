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
        private static readonly DictionaryContext activeContext = 
            (App.ActiveContext ?? throw new ArgumentNullException(nameof(activeContext)));
        public static bool PhraseExists(int id)
        {
            return activeContext.Phrases.Any(p => p.Id == id);
        }

        public static Phrase GetPhraseById(int id)
        {
            return activeContext.Phrases.Find(id) ?? throw new ArgumentException(nameof(id));
        }

        public static int GetFirstIdWithDate(DateTime date)
        {
            Phrase? result = activeContext.Phrases
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
            Phrase? result = activeContext.Phrases
                .Where(x => x.CreationDate > date)
                .OrderBy(p => p.Id)
                .LastOrDefault();
            if (result is null)
            {
                return GetMaxId();
            }

            return result.Id;
        }

        public static async void UpdatePhraseAsync(Phrase phrase)
        {
            Phrase current = await activeContext.Phrases.FindAsync(phrase.Id) 
                ?? throw new ArgumentException(nameof(phrase));
            current.Term = phrase.Term;
            current.Definition = phrase.Definition;
            current.Synonyms = phrase.Synonyms;
            current.Tags = phrase.Tags;
            await activeContext.SaveChangesAsync();
        }

        public static void RemoveRange(List<Phrase> phrases)
        {
            App.ActiveContext?.Phrases.RemoveRange(phrases);
            App.ActiveContext?.SaveChanges();
        }

        public static bool IsEmpty()
        {
            return !activeContext.Phrases.Any();
        }
        public static int GetMaxId()
        {
            if (!IsEmpty())
            {
                return activeContext.Phrases.Max(p => p.Id);
            }

            return 0;
        }
        
        public static int GetMinId()
        {
            if (!IsEmpty())
            {
                return activeContext.Phrases.Min(p => p.Id);
            }
            return 0;
        }

        public static int GetTotalNumber()
        {
            return activeContext.Phrases.Count();
        }

        public static List<Phrase> SearchPhrases(string search)
        {
            List<Phrase> result = [.. activeContext.Phrases.Where(p => !string.IsNullOrWhiteSpace(p.Term) && p.Term.Contains(search))];

            return result;
        }
        
        public static List<string> GetTags()
        {
            List<string> result = activeContext.Phrases.Where(p => p.Tags != null).AsEnumerable().SelectMany(p => p.Tags!)
                .Distinct()
                .ToList();

            return result;
        }

        public static List<Phrase> SearchPhrasesWithTag(string tag)
        {
            List<Phrase> result = activeContext.Phrases
                .Where(p=> p.Tags != null)
                .AsEnumerable()
                .Where(p => p.Tags!.Any(t => t.Contains(tag)))
                .ToList();

            return result;
        }

    }
}
