using UnityEngine;
using TMPro;
public class ProgressionAgent : MonoBehaviour
{
    [Header("REFERENCIAS A ARRASTRAR")]
    public Spawner spawnerRef; // �� ARRASTRAR EL SPAWNER
    public GameManager gameManagerRef; // ← ARRASTRAR EL GAMEMANAGER

    [Header("CONFIGURACIÓN DE DIFICULTAD")]
    [SerializeField] private float velocidadInicial = 2f;

    [Header("FASES DE DIFICULTAD - EDITAR AQUÍ")]
    [SerializeField]
    
    private DificultadFase[] fases = new DificultadFase[]
    {
        new DificultadFase() { puntosMinimos = 0, tiempoSpawn = 2.5f, velocidadResiduo = 2f, nombreFase = "Fase 1: Principiante" },
        new DificultadFase() { puntosMinimos = 100, tiempoSpawn = 2.0f, velocidadResiduo = 2.5f, nombreFase = "Fase 2: Amateur" },
        new DificultadFase() { puntosMinimos = 300, tiempoSpawn = 1.5f, velocidadResiduo = 3f, nombreFase = "Fase 3: Intermedio" },
        new DificultadFase() { puntosMinimos = 700, tiempoSpawn = 1.0f, velocidadResiduo = 3.5f, nombreFase = "Fase 4: Avanzado" },
        new DificultadFase() { puntosMinimos = 1500, tiempoSpawn = 0.7f, velocidadResiduo = 4f, nombreFase = "Fase 5: Extremo" },
    };

    [Header("UI - MOSTRAR FASE ACTUAL")]
    public TextMeshProUGUI textoFaseActual; // ← ARRASTRAR TextMeshPro

    private int faseActual = 0;
    private bool boostVelocidadActivo = false;
    void Start()
    {
        if (spawnerRef == null)
            spawnerRef = Object.FindFirstObjectByType<Spawner>();

        if (gameManagerRef == null)
            gameManagerRef = GameManager.instancia;

        // Suscribirse a cambios de puntuación
        GameManager.OnScoreUpdated += ActualizarDificultad;

        ActualizarDificultad(0); // Inicializar fase 1
    }

    public void ActivarBoostVelocidad(float duracion)
    {
        if (!gameObject.activeInHierarchy || boostVelocidadActivo)
            return;

        StartCoroutine(BoostVelocidadCoroutine(duracion));
    }

    System.Collections.IEnumerator BoostVelocidadCoroutine(float duracion)
    {
        boostVelocidadActivo = true;

        int faseTemporal =
            Mathf.Min(faseActual + 1, fases.Length - 1);

        DificultadFase faseBoost =
            fases[faseTemporal];

        // 🔥 Aplicar velocidad temporal
        spawnerRef.CambiarTiempoSpawn(
            faseBoost.tiempoSpawn
        );

        Residuo[] residuos =
            FindObjectsByType<Residuo>(
                FindObjectsSortMode.None
            );

        foreach (Residuo r in residuos)
        {
            r.velocidad =
                faseBoost.velocidadResiduo;
        }

        Debug.Log("⚡ BOOST VELOCIDAD ACTIVADO");

        yield return new WaitForSeconds(duracion);

        // 🔥 Regresar a fase original
        AplicarFase(faseActual);

        Debug.Log("⏱️ BOOST VELOCIDAD TERMINADO");

        boostVelocidadActivo = false;
    }
    void ActualizarDificultad(int puntosNuevos)
    {
        int nuevaFase = faseActual;

        // Encontrar fase correcta según puntos
        for (int i = fases.Length - 1; i >= 0; i--)
        {
            if (puntosNuevos >= fases[i].puntosMinimos)
            {
                nuevaFase = i;
                break;
            }
        }

        // Si cambió de fase
        if (nuevaFase != faseActual)
        {
            faseActual = nuevaFase;
            AplicarFase(faseActual);
            Debug.Log($"🎮 {fases[faseActual].nombreFase}");
        }
    }

    void AplicarFase(int indice)
    {
        DificultadFase fase = fases[indice];

        // Actualizar Spawner
        spawnerRef.CambiarTiempoSpawn(fase.tiempoSpawn);

        // Actualizar velocidad de residuos actuales
        Residuo[] residuosActivos = FindObjectsByType<Residuo>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (Residuo r in residuosActivos)
        {
            r.velocidad = fase.velocidadResiduo;
        }

        // Actualizar UI
        if (textoFaseActual != null)
        {
            textoFaseActual.text = fase.nombreFase;
        }
    }

    void OnDestroy()
    {
        GameManager.OnScoreUpdated -= ActualizarDificultad;
    }
}

// Estructura para organizar las fases
[System.Serializable]
public class DificultadFase
{
    public int puntosMinimos;
    public float tiempoSpawn;
    public float velocidadResiduo;
    public string nombreFase;
}