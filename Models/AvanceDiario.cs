namespace CobranzaCostas.Models;

/// <summary>
/// Bloque reutilizable de métricas operativas.
/// Se usa tanto para el Compromiso (proyectado) como para el Avance (real).
/// </summary>
public class MetricasOperativas
{
    /// <summary>Número de visitas/tareas realizadas.</summary>
    public int Visitas { get; set; }

    /// <summary>Número de visitas efectivas (contacto real con el acreditado).</summary>
    public int Efectivas { get; set; }

    /// <summary>Monto de cobranza directa capturado.</summary>
    public decimal Cobranza { get; set; }

    /// <summary>
    /// Monto monetario correspondiente a las cuentas en categorías de morosidad
    /// de la ruta del Gestor (envejecimiento de cartera, NO turno laboral).
    /// </summary>
    public decimal Pase6a7 { get; set; }
}

/// <summary>
/// Documento de avance diario por Gestor.
/// Colección Firestore: avances/{region}_{gerencia}_{noEmpleado}_{fecha}_{corte}
/// </summary>
public class AvanceDiario
{
    /// <summary>Llave del documento: {region}_{gerencia}_{noEmpleado}_{fecha}_{corte}</summary>
    public string Id { get; set; } = string.Empty;

    public string NoEmpleado { get; set; } = string.Empty;
    public string Region     { get; set; } = string.Empty;
    public string Gerencia   { get; set; } = string.Empty;

    /// <summary>Fecha del registro en formato yyyy-MM-dd.</summary>
    public string Fecha { get; set; } = string.Empty;

    /// <summary>
    /// Identificador del corte. Los cortes son dinámicos y configurados
    /// por el Director (no hay número fijo ni están quemados en código).
    /// Ejemplo: "C1", "C2", "Cierre".
    /// </summary>
    public string Corte { get; set; } = string.Empty;

    /// <summary>Meta de cobranza de la semana actual, usada como referencia visual.</summary>
    public decimal MetaSemanal { get; set; }

    /// <summary>Lo que el Gestor proyecta lograr en el día.</summary>
    public MetricasOperativas Compromiso { get; set; } = new();

    /// <summary>Lo que el Gestor realmente logró al momento del corte.</summary>
    public MetricasOperativas Avance { get; set; } = new();

    /// <summary>Timestamp UTC de creación/última modificación del documento.</summary>
    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
}
