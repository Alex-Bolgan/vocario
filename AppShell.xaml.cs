﻿using ReCallVocabulary.Pages;

namespace ReCallVocabulary
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(DictionaryViewPage), typeof(DictionaryViewPage));
            Routing.RegisterRoute(nameof(DictionaryOptionsPage), typeof(DictionaryOptionsPage));
            Routing.RegisterRoute(nameof(StatsPage), typeof(StatsPage));
            Routing.RegisterRoute(nameof(DeleteWordsPage), typeof(DeleteWordsPage));
            Routing.RegisterRoute(nameof(AddWordsPage), typeof(AddWordsPage));
            Routing.RegisterRoute(nameof(RecallGamePage), typeof(RecallGamePage));
            Routing.RegisterRoute(nameof(ChooseDictionary), typeof(ChooseDictionary));
        }
    }
}
