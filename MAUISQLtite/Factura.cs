using SQLite;

namespace MAUISQLtite
{
    public class Factura
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int PersonaId { get; set; }

        [NotNull]
        public string NumeroFactura { get; set; } = string.Empty;

        public string Concepto { get; set; } = string.Empty;

        public double Monto { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
