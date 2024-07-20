using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCallVocabulary.Data_Access
{
    public static class DateFileHandler
    {
        public static string datesListFile;
        static DateFileHandler()
        {
#if WINDOWS
            datesListFile = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Dates_list.txt");
#elif ANDROID
        datesListFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Dates_list.txt");
#endif
        }

        public static DateTime[] GetDates()
        {
            bool isValidDate = false;
            DateTime[] result = new DateTime[3];

            if (File.Exists(datesListFile))
            {
                var fileContent = File.ReadAllText(datesListFile).Split(" ");

                if (fileContent.Length == 2)
                {
                    if (DateTime.TryParseExact(fileContent[0], "dd.MM.yyyy", CultureInfo.InvariantCulture,
                               DateTimeStyles.None, out result[1]) &&
                               DateTime.TryParseExact(fileContent[1], "dd.MM.yyyy", CultureInfo.InvariantCulture,
                               DateTimeStyles.None, out result[2]))
                    {
                        isValidDate = true;
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
                else if (fileContent.Length == 3)
                {
                    if (DateTime.TryParseExact(fileContent[0], "dd.MM.yyyy", CultureInfo.InvariantCulture,
                               DateTimeStyles.None, out result[0])
                        && DateTime.TryParseExact(fileContent[1], "dd.MM.yyyy", CultureInfo.InvariantCulture,
                               DateTimeStyles.None, out result[1])
                        && DateTime.TryParseExact(fileContent[2], "dd.MM.yyyy", CultureInfo.InvariantCulture,
                               DateTimeStyles.None, out result[2]))
                    {
                        isValidDate = true;
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
            }

            if (!isValidDate)
            {
                var myFile = File.Create(datesListFile);
                myFile.Close();
                result[1] = result[2] = DateTime.Now;
                File.WriteAllText(datesListFile, $"{DateTime.Now,0:dd.MM.yyyy} {DateTime.Now,0:dd.MM.yyyy}");
            }
            return result;
        }

        public static void WriteDates(DateTime[] dates)
        {
            if (dates.Length == 3)
            {
                File.WriteAllText(datesListFile, $"{dates[0],0:dd.MM.yyyy} {dates[1],0:dd.MM.yyyy}  {dates[2],0:dd.MM.yyyy}");
            }
            else if (dates.Length==2)
            {
                File.WriteAllText(datesListFile, $"{dates[0],0:dd.MM.yyyy} {dates[1],0:dd.MM.yyyy}");
            }
        }
    }
}
