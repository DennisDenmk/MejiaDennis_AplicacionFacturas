using Microsoft.Extensions.Logging;

namespace MAUISQLtite
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Registrar el Servicio de Base de Datos como Singleton
            builder.Services.AddSingleton<DatabaseService>();

            // Registrar las Páginas de la aplicación como Transient
            builder.Services.AddTransient<PersonasListPage>();
            builder.Services.AddTransient<PersonaDetailPage>();
            builder.Services.AddTransient<PersonaEditPage>();
            builder.Services.AddTransient<FacturaEditPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
