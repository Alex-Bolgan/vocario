using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCallVocabulary.Data_Access;

public static class ServiceHelper
{
    public static TService GetService<TService>()
    {
        if (Current.GetService<TService>() is null)
            throw new NullReferenceException();

        return Current.GetService<TService>();

    }

    public static IServiceProvider Current =>
#if WINDOWS10_0_17763_0_OR_GREATER
			MauiWinUIApplication.Current.Services;
#elif ANDROID
            MauiApplication.Current.Services;
#elif IOS || MACCATALYST
        MauiUIApplicationDelegate.Current.Services;
#else
			null;
#endif
}