using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace MAUISQLtite
{
    public partial class PersonasListPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private List<Persona> _allPersonas = new();

        public PersonasListPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadPersonasAsync();
        }

        private async Task LoadPersonasAsync()
        {
            try
            {
                _allPersonas = await _databaseService.GetPersonasAsync();
                ApplyFilter();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudieron cargar las personas: {ex.Message}", "OK");
            }
        }

        private void ApplyFilter()
        {
            var searchText = PersonaSearchBar.Text ?? string.Empty;
            if (string.IsNullOrWhiteSpace(searchText))
            {
                PersonasCollectionView.ItemsSource = _allPersonas;
            }
            else
            {
                var filtered = _allPersonas
                    .Where(p => p.Nombre.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                PersonasCollectionView.ItemsSource = filtered;
            }
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilter();
        }

        private async void OnAddPersonaClicked(object sender, EventArgs e)
        {
            // Navegar a la página de edición para crear una nueva persona
            await Shell.Current.GoToAsync(nameof(PersonaEditPage));
        }

        private async void OnVerFacturasClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Persona persona)
            {
                // Navegar al detalle de la persona
                var navigationParameter = new Dictionary<string, object>
                {
                    { "Persona", persona }
                };
                await Shell.Current.GoToAsync(nameof(PersonaDetailPage), navigationParameter);
            }
        }

        private async void OnEditPersonaClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Persona persona)
            {
                // Navegar a la página de edición con la persona seleccionada
                var navigationParameter = new Dictionary<string, object>
                {
                    { "Persona", persona }
                };
                await Shell.Current.GoToAsync(nameof(PersonaEditPage), navigationParameter);
            }
        }

        private async void OnDeletePersonaClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Persona persona)
            {
                bool confirm = await DisplayAlert(
                    "Confirmar", 
                    $"¿Está seguro de que desea eliminar a {persona.Nombre}? Esto también eliminará todas sus facturas asociadas.", 
                    "Sí", 
                    "No");

                if (confirm)
                {
                    try
                    {
                        await _databaseService.DeletePersonaAsync(persona);
                        await LoadPersonasAsync();
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", $"No se pudo eliminar a la persona: {ex.Message}", "OK");
                    }
                }
            }
        }
    }
}
