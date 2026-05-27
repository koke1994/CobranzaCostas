using CobranzaCostas.Models;
using Plugin.Firebase.Firestore;

namespace CobranzaCostas.Services;

/// <summary>
/// Servicio base para todas las operaciones de lectura y escritura en Firestore.
/// Encapsula el acceso a las colecciones definidas en el esquema de datos.
/// </summary>
public class FirestoreService
{
    private readonly IFirebaseFirestore _db;

    public FirestoreService()
    {
        _db = CrossFirebaseFirestore.Current;
    }

    // ════════════════════════════════════════════════════════════════
    // USUARIOS
    // Colección: usuarios/{no_empleado}
    // ════════════════════════════════════════════════════════════════

    /// <summary>Obtiene el perfil completo del usuario desde Firestore.</summary>
    public async Task<Usuario?> GetUsuarioAsync(string noEmpleado)
    {
        try
        {
            var snapshot = await _db
                .GetCollection("usuarios")
                .GetDocument(noEmpleado)
                .GetSnapshotAsync();

            return snapshot.Exists
                ? snapshot.ToObject<Usuario>()
                : null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Firestore] GetUsuario error: {ex.Message}");
            return null;
        }
    }

    // ════════════════════════════════════════════════════════════════
    // AVANCES DIARIOS
    // Colección: avances/{region}_{gerencia}_{noEmpleado}_{fecha}_{corte}
    // ════════════════════════════════════════════════════════════════

    /// <summary>Obtiene el documento de avance de un Gestor para un corte específico.</summary>
    public async Task<AvanceDiario?> GetAvanceAsync(string docId)
    {
        try
        {
            var snapshot = await _db
                .GetCollection("avances")
                .GetDocument(docId)
                .GetSnapshotAsync();

            return snapshot.Exists
                ? snapshot.ToObject<AvanceDiario>()
                : null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Firestore] GetAvance error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Crea o sobreescribe el documento completo de avance.
    /// Usar para el registro inicial del Compromiso del día.
    /// </summary>
    public async Task<bool> GuardarAvanceAsync(string docId, AvanceDiario avance)
    {
        try
        {
            avance.FechaRegistro = DateTime.UtcNow;

            await _db
                .GetCollection("avances")
                .GetDocument(docId)
                .SetDataAsync(avance);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Firestore] GuardarAvance error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Actualiza únicamente los campos especificados del documento de avance.
    /// Usar para actualizaciones parciales de Avance real sin tocar el Compromiso.
    /// </summary>
    /// <param name="updates">Diccionario con los campos a actualizar. Ej: { "Avance.Cobranza", 1500m }</param>
    public async Task<bool> ActualizarCamposAvanceAsync(string docId, object updates)
    {
        try
        {
            await _db
                .GetCollection("avances")
                .GetDocument(docId)
                .UpdateDataAsync(updates);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Firestore] ActualizarAvance error: {ex.Message}");
            return false;
        }
    }

    // ════════════════════════════════════════════════════════════════
    // COMPROMISO RELACIONAL (GERENTE)
    // Colección: compromisos_relacional/{region}_{gerencia}_{noEmpleado}_{fecha}
    // ════════════════════════════════════════════════════════════════

    /// <summary>Obtiene el compromiso relacional de un Gerente para una fecha.</summary>
    public async Task<CompromisoRelacional?> GetCompromisoRelacionalAsync(string docId)
    {
        try
        {
            var snapshot = await _db
                .GetCollection("compromisos_relacional")
                .GetDocument(docId)
                .GetSnapshotAsync();

            return snapshot.Exists
                ? snapshot.ToObject<CompromisoRelacional>()
                : null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Firestore] GetCompromisoRelacional error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Crea o sobreescribe el documento de compromiso relacional del Gerente.
    /// </summary>
    public async Task<bool> GuardarCompromisoRelacionalAsync(string docId, CompromisoRelacional compromiso)
    {
        try
        {
            compromiso.FechaRegistro = DateTime.UtcNow;

            await _db
                .GetCollection("compromisos_relacional")
                .GetDocument(docId)
                .SetDataAsync(compromiso);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Firestore] GuardarCompromisoRelacional error: {ex.Message}");
            return false;
        }
    }

    // ════════════════════════════════════════════════════════════════
    // HELPERS: Constructores de Document ID
    // ════════════════════════════════════════════════════════════════

    /// <summary>
    /// Construye el DocID para la colección "avances".
    /// Formato: {region}_{gerencia}_{noEmpleado}_{fecha}_{corte}
    /// Ejemplo:  COSTAS1_IXTAPA_EMP001_2026-05-26_C2
    /// </summary>
    public static string BuildAvanceDocId(
        string region, string gerencia, string noEmpleado,
        string fecha,  string corte)
        => $"{region}_{gerencia}_{noEmpleado}_{fecha}_{corte}";

    /// <summary>
    /// Construye el DocID para la colección "compromisos_relacional".
    /// Formato: {region}_{gerencia}_{noEmpleado}_{fecha}
    /// Ejemplo:  COSTAS1_IXTAPA_GER002_2026-05-26
    /// </summary>
    public static string BuildRelacionalDocId(
        string region, string gerencia, string noEmpleado, string fecha)
        => $"{region}_{gerencia}_{noEmpleado}_{fecha}";
}
