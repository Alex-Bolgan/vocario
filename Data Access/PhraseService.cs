using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCallVocabulary.Data_Access
{
    public class PhraseService
    {
        private readonly DictionaryContext dictionaryContext;

        public Phrase? CurrentPhrase { get; set; }

        public PhraseService(DbContextManager dbContextManager)
        {
            dictionaryContext = dbContextManager.CurrentDictionaryContext;
        }

        public bool PhraseExists(int id)
        {
            return dictionaryContext.Phrases.Any(p => p.Id == id);
        }

        public Phrase GetPhraseById(int id)
        {
            return dictionaryContext.Phrases.Find(id) ?? throw new ArgumentException(nameof(id));
        }

        public int GetFirstIdWithDate(DateTime date)
        {
            Phrase? result = dictionaryContext.Phrases
                .Where(x => x.CreationDate > date)
                .FirstOrDefault();
            if (result is null)
            {
                return GetMaxId();
            }

            return result.Id;
        }

        public int GetMaxIdWithDate(DateTime date)
        {
            Phrase? result = dictionaryContext.Phrases
                .Where(x => x.CreationDate <= date)
                .OrderBy(p => p.Id)
                .LastOrDefault();

            if (result is null)
            {
                return GetMaxId();
            }

            return result.Id;
        }

        public async void UpdatePhraseAsync(Phrase phrase)
        {
            Phrase current = await dictionaryContext.Phrases.FindAsync(phrase.Id) 
                ?? throw new ArgumentException(nameof(phrase));
            current.Term = phrase.Term;
            current.Definition = phrase.Definition;
            current.Synonyms = phrase.Synonyms;
            current.Tags = phrase.Tags;
            await dictionaryContext.SaveChangesAsync();
        }

        public void RemoveRange(List<Phrase> phrases)
        {
            App.ActiveContext?.Phrases.RemoveRange(phrases);
            App.ActiveContext?.SaveChanges();
        }

        public bool IsEmpty()
        {
            return !dictionaryContext.Phrases.Any();
        }
        public int GetMaxId()
        {
            if (!IsEmpty())
            {
                return dictionaryContext.Phrases.Max(p => p.Id);
            }

            return 0;
        }
        
        public int GetMinId()
        {
            if (!IsEmpty())
            {
                return dictionaryContext.Phrases.Min(p => p.Id);
            }
            return 0;
        }

        public int GetTotalNumber()
        {
            return dictionaryContext.Phrases.Count();
        }

        public List<Phrase> SearchPhrases(string search)
        {
            List<Phrase> result = [.. dictionaryContext.Phrases.Where(p => !string.IsNullOrWhiteSpace(p.Term) && p.Term.Contains(search))];

            return result;
        }
        
        public List<string> GetTags()
        {
            List<string> result = dictionaryContext.Phrases.Where(p => p.Tags != null).AsEnumerable().SelectMany(p => p.Tags!)
                .Distinct()
                .ToList();

            return result;
        }

        public List<Phrase> SearchPhrasesWithTag(string tag)
        {
            List<Phrase> result = dictionaryContext.Phrases
                .Where(p=> p.Tags != null)
                .AsEnumerable()
                .Where(p => p.Tags!.Any(t => t.Contains(tag)))
                .ToList();

            return result;
        }

        public int GetNumberOfAddedToday()
        {
            int max = GetMaxId();
            int max_previous = GetMaxIdWithDate(DateTime.Today.AddDays(-1));

            int realCount = 0;
            for (int i = max_previous; i < max; ++i)
            {
                if (PhraseExists(i))
                {
                    realCount++;
                }
            }

            return realCount;
        }
    }
}
