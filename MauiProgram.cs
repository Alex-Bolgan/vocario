﻿using CommunityToolkit.Maui;
using Microcharts.Maui;
using Microsoft.Extensions.Logging;
using ReCallVocabulary.Data_Access;
using ReCallVocabulary.Pages;

namespace ReCallVocabulary
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMicrocharts()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<DictionaryViewPage>();
            builder.Services.AddTransient<DictionaryOptionsPage>();
            builder.Services.AddTransient<StatsPage>();
            builder.Services.AddTransient<SettingsPage>();

            builder.Services.AddSingleton<DbContextManager>();
            builder.Services.AddSingleton<PhraseService>();
            builder.Services.AddSingleton<StatsService>();

            return builder.Build();
        }
    }
}
