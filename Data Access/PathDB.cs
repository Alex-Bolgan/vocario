namespace ReCallVocabulary.Data_Access
{
    internal static class PathDB
    {
        public static string GetPath(string nameDB)
        {
            string pathDBSQLite = String.Empty;

            pathDBSQLite = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            pathDBSQLite = Path.Combine(pathDBSQLite, nameDB);
            return pathDBSQLite;
        }
    }

}