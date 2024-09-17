using System.Globalization;
using CsvHelper;

namespace ReCallVocabulary.Data_Access
{
    public static class StatsFileHandler
    {
        public static string StatsFile;
        static StatsFileHandler()
        {
#if WINDOWS
            StatsFile = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Stats.csv");
#elif ANDROID
        StatsFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Stats.csv");
#endif
        }

        public static IEnumerable<StatsRecord>? GetRecords()
        {
            if (File.Exists(StatsFile))
            {
                using var reader = new StreamReader(StatsFile);
                using var csv = new CsvReader(reader,CultureInfo.InvariantCulture);

                var records = csv.GetRecords<StatsRecord>();
                return records;
            }

            File.Create(StatsFile);
            return null;
        }

        public static void WriteDates(DateTime[] dates)
        {
            if (dates.Length == 3)
            {
                File.WriteAllText(StatsFile, $"{dates[0],0:dd.MM.yyyy} {dates[1],0:dd.MM.yyyy}  {dates[2],0:dd.MM.yyyy}");
            }
            else if (dates.Length==2)
            {
                File.WriteAllText(StatsFile, $"{dates[0],0:dd.MM.yyyy} {dates[1],0:dd.MM.yyyy}");
            }
        }
    }

    public class StatsRecord
    {
        public DateTime Date;

        public int AddedNumber;

        public int UniqueRecalledNumber;

        public int RecalledNumber;
    }

}
