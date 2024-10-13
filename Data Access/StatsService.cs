namespace ReCallVocabulary.Data_Access
{
    public static class StatsService
    {
        public static async void UpdateAddedNumber()
        {
            int addedToday = Model.GetNumberOfAddedToday();

            if (addedToday < 0)
            {
                throw new Exception("GetNumberOfAddedToday");
            }

            StatsRecord? currentRecord;
            if ((currentRecord = await App.statsContext.StatsRecords.FindAsync(DateTime.Today)) is null)
            {
                await App.statsContext.AddAsync(new StatsRecord()
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
                    App.statsContext.StatsRecords.Remove(currentRecord);
                }
            }

            await App.statsContext.SaveChangesAsync();
        }

        public static async void UpdateRecalledNumber(int number, int uniqueNumber, DateTime creationDate)
        {
            StatsRecord? currentRecord;

            if ((currentRecord = await App.statsContext.StatsRecords.FindAsync(creationDate.Date)) is null)
            {
                await App.statsContext.StatsRecords.AddAsync(new StatsRecord()
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

            await App.statsContext.SaveChangesAsync();
        }
    }
}
