using UnityEngine;

/// <summary>
/// Clase base abstracta para todos los modos de juego
/// Define la estructura que debe tener cada modo
/// </summary>
public abstract class GameMode : MonoBehaviour
{
    [Header("INFORMACIÓN DEL MODO")]
    public string nombreModo = "Modo Base";
    public string descripcion = "Descripción del modo";

    protected GameManager gameManager;
    protected int puntajeActual = 0;
    protected int residuosClasificados = 0;
    protected int erroresActuales = 0;

    protected virtual void Start()
    {
        gameManager = GameManager.instancia;
        InicializarModo();
    }

    /// <summary>
    /// Inicializa el modo (se ejecuta al empezar)
    /// </summary>
    public abstract void InicializarModo();

    /// <summary>
    /// Se llama cada vez que se clasifica un residuo correctamente
    /// </summary>
    public abstract void OnAcierto(int puntos);

    /// <summary>
    /// Se llama cuando se comete un error
    /// </summary>
    public abstract void OnError();

    /// <summary>
    /// Verifica si el juego debe terminar
    /// </summary>
    public abstract bool DeberiaSalirDelJuego();

    /// <summary>
    /// Obtiene información para mostrar en UI
    /// </summary>
    public virtual string ObtenerInfoUI()
    {
        return $"Puntos: {puntajeActual} | Residuos: {residuosClasificados}";
    }

    /// <summary>
    /// Obtiene el resultado final del modo
    /// </summary>
    public virtual string ObtenerResultado()
    {
        return $"Puntuación Final: {puntajeActual}\nResiduos Clasificados: {residuosClasificados}";
    }

    public int ObtenerPuntaje() => puntajeActual;
    public int ObtenerResiduos() => residuosClasificados;
    public int ObtenerErrores() => erroresActuales;
}