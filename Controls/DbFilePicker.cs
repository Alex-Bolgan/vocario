using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReCallVocabulary.Data_Access;

namespace ReCallVocabulary.Controls
{
    internal class DbFilePicker
    {
        static FilePickerFileType customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, new[] { "application/x-sqlite3", "application/vnd.sqlite3", "application/octet-stream", "application/x-trash" } },
                    { DevicePlatform.WinUI, new[] { ".db" } },
                });

        static PickOptions options = new()
        {
            PickerTitle = "Please select a .db file",
            FileTypes = customFileType,
        };

        public static async Task<int> PickAndMoveDb()
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(options);
                if (result == null) return 1;

                if (File.Exists(PathDB.GetPath(result.FileName)))
                {
                    File.Delete(PathDB.GetPath(result.FileName));
                }

                File.Move(result.FullPath, PathDB.GetPath(result.FileName));
                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
        }
    }
}
