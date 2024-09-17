namespace ReCallVocabulary.Data_Access
{
    internal static class PathDB
    {
        public static string GetPath(string nameDB)
        {
            string pathDBSQLite = String.Empty;
            pathDBSQLite = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            pathDBSQLite = Path.Combine(pathDBSQLite, "ReCallVocabulary", nameDB);
            return pathDBSQLite;
        }
    }
}