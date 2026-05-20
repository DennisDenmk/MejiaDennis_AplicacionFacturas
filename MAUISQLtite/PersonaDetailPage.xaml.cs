using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace MAUISQLtite
{
    [QueryProperty(nameof(Persona), "Persona")]
    public partial class PersonaDetailPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private Persona _persona = new();

        public Persona Persona
        {
            get => _persona;
            set
            {
                _persona = value;
                UpdatePersonaUi();
                _ = LoadFacturasAsync();
            }
        }

        public PersonaDetailPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (Persona != null && Persona.Id != 0)
            {
                // También recargamos el objeto Persona por si se editó y regresamos
                var updatedPersona = await _databaseService.GetPersonaAsync(Persona.Id);
                if (updatedPersona != null)
                {
                    _persona = updatedPersona;
                    UpdatePersonaUi();
                }
                await LoadFacturasAsync();
            }
        }

        private void UpdatePersonaUi()
        {
            if (_persona != null)
            {
                NombreLabel.Text = _persona.Nombre;
                EmailLabel.Text = string.IsNullOrWhiteSpace(_persona.Email) ? "No especificado" : _persona.Email;
                TelefonoLabel.Text = string.IsNullOrWhiteSpace(_persona.Telefono) ? "No especificado" : _persona.Telefono;
            }
        }

        private async Task LoadFacturasAsync()
        {
            if (_persona == null || _persona.Id == 0)
                return;

            try
            {
                var facturas = await _databaseService.GetFacturasForPersonaAsync(_persona.Id);
                FacturasCollectionView.ItemsSource = facturas;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudieron cargar las facturas: {ex.Message}", "OK");
            }
        }

        private async void OnEditPersonaClicked(object sender, EventArgs e)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Persona", _persona }
            };
            await Shell.Current.GoToAsync(nameof(PersonaEditPage), navigationParameter);
        }

        private async void OnDeletePersonaClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert(
                "Confirmar", 
                $"¿Está seguro de que desea eliminar a {_persona.Nombre}? Se borrarán también todas sus facturas.", 
                "Sí", 
                "No");

            if (confirm)
            {
                try
                {
                    await _databaseService.DeletePersonaAsync(_persona);
                    await Shell.Current.GoToAsync("..");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"No se pudo eliminar a la persona: {ex.Message}", "OK");
                }
            }
        }

        private async void OnAddFacturaClicked(object sender, EventArgs e)
        {
            // Navegar a FacturaEditPage enviando solo el PersonaId
            var navigationParameter = new Dictionary<string, object>
            {
                { "PersonaId", _persona.Id }
            };
            await Shell.Current.GoToAsync(nameof(FacturaEditPage), navigationParameter);
        }

        private async void OnEditFacturaClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Factura factura)
            {
                // Navegar a FacturaEditPage enviando la factura completa
                var navigationParameter = new Dictionary<string, object>
                {
                    { "Factura", factura },
                    { "PersonaId", _persona.Id }
                };
                await Shell.Current.GoToAsync(nameof(FacturaEditPage), navigationParameter);
            }
        }

        private async void OnDeleteFacturaClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Factura factura)
            {
                bool confirm = await DisplayAlert(
                    "Confirmar", 
                    $"¿Está seguro de que desea eliminar la factura Nº {factura.NumeroFactura}?", 
                    "Sí", 
                    "No");

                if (confirm)
                {
                    try
                    {
                        await _databaseService.DeleteFacturaAsync(factura);
                        await LoadFacturasAsync();
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", $"No se pudo eliminar la factura: {ex.Message}", "OK");
                    }
                }
            }
        }
    }
}
