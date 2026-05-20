using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace MAUISQLtite
{
    [QueryProperty(nameof(PersonaToEdit), "Persona")]
    public partial class PersonaEditPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private Persona? _personaToEdit;

        public Persona? PersonaToEdit
        {
            get => _personaToEdit;
            set
            {
                _personaToEdit = value;
                if (_personaToEdit != null)
                {
                    PageTitleLabel.Text = "Editar Persona";
                    NombreEntry.Text = _personaToEdit.Nombre;
                    EmailEntry.Text = _personaToEdit.Email;
                    TelefonoEntry.Text = _personaToEdit.Telefono;
                }
            }
        }

        public PersonaEditPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var nombre = NombreEntry.Text?.Trim();
            var email = EmailEntry.Text?.Trim() ?? string.Empty;
            var telefono = TelefonoEntry.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(nombre))
            {
                await DisplayAlert("Validación", "El Nombre es obligatorio.", "Aceptar");
                return;
            }

            var persona = _personaToEdit ?? new Persona();
            persona.Nombre = nombre;
            persona.Email = email;
            persona.Telefono = telefono;

            try
            {
                await _databaseService.SavePersonaAsync(persona);
                
                // Si veníamos de una edición, pasamos la persona actualizada al volver para refrescar el detalle
                if (_personaToEdit != null)
                {
                    var navigationParameter = new Dictionary<string, object>
                    {
                        { "Persona", persona }
                    };
                    await Shell.Current.GoToAsync("..", navigationParameter);
                }
                else
                {
                    await Shell.Current.GoToAsync("..");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo guardar la persona: {ex.Message}", "Aceptar");
            }
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
