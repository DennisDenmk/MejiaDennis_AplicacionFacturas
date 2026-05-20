namespace MAUISQLtite
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Registrar las rutas de navegación para páginas secundarias
            Routing.RegisterRoute(nameof(PersonaDetailPage), typeof(PersonaDetailPage));
            Routing.RegisterRoute(nameof(PersonaEditPage), typeof(PersonaEditPage));
            Routing.RegisterRoute(nameof(FacturaEditPage), typeof(FacturaEditPage));
        }
    }
}
