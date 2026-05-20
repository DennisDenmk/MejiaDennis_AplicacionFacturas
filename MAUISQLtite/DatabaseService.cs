using SQLite;

namespace MAUISQLtite
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection? _database;
        
        // Nombre del archivo de base de datos
        private const string DatabaseFilename = "MAUISQLiteData.db3";

        // Flags para inicializar la conexión en modo lectura/escritura, crear si no existe y cache compartido
        private const SQLiteOpenFlags Flags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;

        // Ruta de base de datos multiplataforma (en la carpeta de datos de la app)
        private static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

        // Inicialización asíncrona única de las tablas
        private async Task InitAsync()
        {
            if (_database != null)
                return;

            _database = new SQLiteAsyncConnection(DatabasePath, Flags);
            
            // Creamos las tablas si no existen
            await _database.CreateTableAsync<Persona>();
            await _database.CreateTableAsync<Factura>();
        }

        #region CRUD Persona

        public async Task<List<Persona>> GetPersonasAsync()
        {
            await InitAsync();
            return await _database!.Table<Persona>().ToListAsync();
        }

        public async Task<Persona?> GetPersonaAsync(int id)
        {
            await InitAsync();
            return await _database!.Table<Persona>().Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SavePersonaAsync(Persona persona)
        {
            await InitAsync();
            if (persona.Id != 0)
            {
                return await _database!.UpdateAsync(persona);
            }
            else
            {
                return await _database!.InsertAsync(persona);
            }
        }

        public async Task<int> DeletePersonaAsync(Persona persona)
        {
            await InitAsync();
            
            // Iniciamos transacción manual o eliminaciones consecutivas para asegurar borrado en cascada de facturas
            // Primero borramos todas las facturas que pertenezcan a esta persona
            await _database!.ExecuteAsync("DELETE FROM Factura WHERE PersonaId = ?", persona.Id);
            
            // Luego borramos a la persona
            return await _database!.DeleteAsync(persona);
        }

        #endregion

        #region CRUD Factura

        public async Task<List<Factura>> GetFacturasAsync()
        {
            await InitAsync();
            return await _database!.Table<Factura>().ToListAsync();
        }

        public async Task<List<Factura>> GetFacturasForPersonaAsync(int personaId)
        {
            await InitAsync();
            return await _database!.Table<Factura>().Where(f => f.PersonaId == personaId).ToListAsync();
        }

        public async Task<Factura?> GetFacturaAsync(int id)
        {
            await InitAsync();
            return await _database!.Table<Factura>().Where(f => f.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveFacturaAsync(Factura factura)
        {
            await InitAsync();
            if (factura.Id != 0)
            {
                return await _database!.UpdateAsync(factura);
            }
            else
            {
                return await _database!.InsertAsync(factura);
            }
        }

        public async Task<int> DeleteFacturaAsync(Factura factura)
        {
            await InitAsync();
            return await _database!.DeleteAsync(factura);
        }

        #endregion
    }
}
