using CobranzaCostas.Models;
using Plugin.Firebase.Firestore;

namespace CobranzaCostas.Services;

public class FirestoreService
{
    private readonly IFirebaseFirestore _db;

    public FirestoreService()
    {
        _db = CrossFirebaseFirestore.Current;
    }

    // ════════════════════════════════════════════════════════════════
    // USUARIOS
    // ════════════════════════════════════════════════════════════════
    public async Task<Usuario?> GetUsuarioAsync(string noEmpleado)
    {
        try
        {
            var snapshot = await _db.GetCollection("usuarios").GetDocument(noEmpleado).GetDocumentSnapshotAsync();
            return snapshot.Exists ? snapshot.ToObject<Usuario>() : null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Firestore] GetUsuario error: {ex.Message}");
            return null;
        }
    }

    // ════════════════════════════════════════════════════════════════
    // AVANCES DIARIOS
    // ════════════════════════════════════════════════════════════════
    public async Task<AvanceDiario?> GetAvanceAsync(string docId)
    {
        try
        {
            var snapshot = await _db.GetCollection("avances").GetDocument(docId).GetDocumentSnapshotAsync();
            return snapshot.Exists ? snapshot.ToObject<AvanceDiario>() : null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Firestore] GetAvance error: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> GuardarAvanceAsync(string docId, AvanceDiario avance)
    {
        try
        {
            await _db.GetCollection("avances").GetDocument(docId).SetDataAsync(avance);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Firestore] GuardarAvance error: {ex.Message}");
            return false;
        }
    }

    // Sobrecarga especial para recibir el diccionario de Claude y convertirlo al tipo estricto del plugin
    public async Task<bool> ActualizarCamposAvanceAsync(string docId, Dictionary<string, object> updates)
    {
        try
        {
            var nativeUpdates = new Dictionary<object, object>();
            foreach (var kvp in updates)
            {
                nativeUpdates.Add(kvp.Key, kvp.Value);
            }
            await _db.GetCollection("avances").GetDocument(docId).UpdateDataAsync(nativeUpdates);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Firestore] ActualizarCamposAvance error: {ex.Message}");
            return false;
        }
    }

    // ════════════════════════════════════════════════════════════════
    // COMPROMISOS RELACIONALES
    // ════════════════════════════════════════════════════════════════
    public async Task<CompromisoRelacional?> GetCompromisoRelacionalAsync(string docId)
    {
        try
        {
            var snapshot = await _db.GetCollection("compromisos_relacional").GetDocument(docId).GetDocumentSnapshotAsync();
            return snapshot.Exists ? snapshot.ToObject<CompromisoRelacional>() : null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Firestore] GetCompromisoRelacional error: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> GuardarCompromisoRelacionalAsync(string docId, CompromisoRelacional compromiso)
    {
        try
        {
            await _db.GetCollection("compromisos_relacional").GetDocument(docId).SetDataAsync(compromiso);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Firestore] GuardarCompromisoRelacional error: {ex.Message}");
            return false;
        }
    }

    public static string BuildAvanceDocId(string region, string gerencia, string noEmpleado, string fecha, string corte)
        => $"{region}_{gerencia}_{noEmpleado}_{fecha}_{corte}";

    public static string BuildRelacionalDocId(string region, string gerencia, string noEmpleado, string fecha)
        => $"{region}_{gerencia}_{noEmpleado}_{fecha}";
}