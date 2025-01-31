namespace ReCallVocabulary.Data_Access
{
    public class StatsService
    {
        private PhraseService _phraseService;

        private StatsContext _statsContext;

        public StatsService(DbContextManager dbContextManager, PhraseService phraseService)
        {
            _phraseService = phraseService;
            _statsContext = dbContextManager.CurrentStatsContext;
        }
        public async void UpdateAddedNumber()
        {
            int addedToday = _phraseService.GetNumberOfAddedToday();

            if (addedToday < 0)
            {
                throw new Exception("GetNumberOfAddedToday");
            }

            StatsRecord? currentRecord;
            if ((currentRecord = await _statsContext.StatsRecords.FindAsync(DateTime.Today)) is null)
            {
                await _statsContext.AddAsync(new StatsRecord()
                {
                    Date = DateTime.Today,
                    AddedNumber = addedToday
                });
            }
            else
            {
                currentRecord.AddedNumber = addedToday;

                if (addedToday == 0)
                {
                    _statsContext.StatsRecords.Remove(currentRecord);
                }
            }

            await _statsContext.SaveChangesAsync();
        }

        public async void UpdateRecalledNumber(int number, int uniqueNumber, DateTime creationDate)
        {
            StatsRecord? currentRecord;

            if ((currentRecord = await _statsContext.StatsRecords.FindAsync(creationDate.Date)) is null)
            {
                await _statsContext.StatsRecords.AddAsync(new StatsRecord()
                {
                    Date = DateTime.Today,
                    RecalledNumber = number,
                    UniqueRecalledNumber = uniqueNumber
                });
            }
            else
            {
                currentRecord.RecalledNumber += number;
                currentRecord.UniqueRecalledNumber += uniqueNumber;
            }

            await _statsContext.SaveChangesAsync();
        }
    }
}
