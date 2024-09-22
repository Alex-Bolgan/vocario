namespace ReCallVocabulary.Data_Access
{
    internal static class PathDB
    {
        public static string GetDocumentsPath(string DbName)
        {
            string pathDBSQLite = String.Empty;
            pathDBSQLite = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            pathDBSQLite = Path.Combine(pathDBSQLite, "ReCallVocabulary", DbName);
            return pathDBSQLite;
        }

        public static string GetPath(string DbName)
        {
            string pathDBSQLite = String.Empty;
#if WINDOWS
            pathDBSQLite =
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                pathDBSQLite = Path.Combine(pathDBSQLite, DbName);

#elif ANDROID
            pathDBSQLite = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal), DbName);
#endif
            return pathDBSQLite;
        }
    }
}