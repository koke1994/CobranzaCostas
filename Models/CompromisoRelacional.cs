namespace CobranzaCostas.Models;

/// <summary>
/// Documento que representa el compromiso operativo y relacional de un Gerente.
/// Colección Firestore: compromisos_relacional/{region}_{gerencia}_{noEmpleado}_{fecha}
/// </summary>
public class CompromisoRelacional
{
    public string Id { get; set; } = string.Empty;
    public string NoEmpleado { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Gerencia { get; set; } = string.Empty;
    public string Fecha { get; set; } = string.Empty;
    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

    // Métricas del gerente
    public int VisitasDeApoyo { get; set; }
    public int ReunionesEquipo { get; set; }
    
    public decimal CobranzaSupervisada { get; set; }
}