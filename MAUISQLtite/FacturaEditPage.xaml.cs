using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace MAUISQLtite
{
    [QueryProperty(nameof(FacturaToEdit), "Factura")]
    [QueryProperty(nameof(PersonaId), "PersonaId")]
    public partial class FacturaEditPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private Factura? _facturaToEdit;
        private int _personaId;

        public Factura? FacturaToEdit
        {
            get => _facturaToEdit;
            set
            {
                _facturaToEdit = value;
                if (_facturaToEdit != null)
                {
                    PageTitleLabel.Text = "Editar Factura";
                    NumeroFacturaEntry.Text = _facturaToEdit.NumeroFactura;
                    ConceptoEntry.Text = _facturaToEdit.Concepto;
                    MontoEntry.Text = _facturaToEdit.Monto.ToString("F2", CultureInfo.InvariantCulture);
                    FechaDatePicker.Date = _facturaToEdit.Fecha;
                }
            }
        }

        public int PersonaId
        {
            get => _personaId;
            set => _personaId = value;
        }

        public FacturaEditPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var numeroFactura = NumeroFacturaEntry.Text?.Trim();
            var concepto = ConceptoEntry.Text?.Trim() ?? string.Empty;
            var montoText = MontoEntry.Text?.Trim();
            // Usamos coincidencia de patrones (pattern matching) para soportar de forma segura 
            // tanto DateTime? como DateTime en distintas versiones de .NET MAUI.
            DateTime fecha = FechaDatePicker.Date is DateTime dt ? dt : DateTime.Now;

            if (string.IsNullOrWhiteSpace(numeroFactura))
            {
                await DisplayAlert("Validación", "El Número de Factura es obligatorio.", "Aceptar");
                return;
            }

            if (string.IsNullOrWhiteSpace(montoText) || 
                !double.TryParse(montoText, NumberStyles.Any, CultureInfo.InvariantCulture, out double monto) || 
                monto < 0)
            {
                // Reintentar con el formato local por si acaso el separador es coma en español
                if (!double.TryParse(montoText, NumberStyles.Any, CultureInfo.CurrentCulture, out monto) || monto < 0)
                {
                    await DisplayAlert("Validación", "Por favor ingrese un Monto numérico válido (mayor o igual a cero).", "Aceptar");
                    return;
                }
            }

            var factura = _facturaToEdit ?? new Factura();
            factura.PersonaId = _personaId;
            factura.NumeroFactura = numeroFactura;
            factura.Concepto = concepto;
            factura.Monto = monto;
            factura.Fecha = fecha;

            try
            {
                await _databaseService.SaveFacturaAsync(factura);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo guardar la factura: {ex.Message}", "Aceptar");
            }
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
