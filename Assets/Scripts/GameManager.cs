using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    [Header("PUNTUACIÓN")]
    public int puntaje = 0;
    private int record = 0;

    [Header("VIDAS UI (CORAZONES)")]
    public Transform contenedorVidas; // Panel con GridLayout
    public GameObject prefabCorazon;  // Prefab del corazón

    [Header("VIDAS")]
    public int vidas = 3;

    private List<GameObject> corazones = new List<GameObject>();

    [Header("UI")]
    public TextMeshProUGUI textoPuntaje;
    public TextMeshProUGUI textoRecord;
    public TextMeshProUGUI textoVidas;
    public GameObject panelGameOver;
    public GameObject contenedores;
    public Transform contenedorResiduos;
    public Spawner spawner;
    public GameObject botonPausa;

    [Header("SISTEMA DE MODOS")] // ← NUEVO
    private GameMode modoActual;
    private string modoSeleccionado = "Clasico";

    [Header("PANEL PAUSA")]
    public GameObject panelPausa;

    private bool estaPausado = false;
    Collider2D[] colliders;
    public static bool juegoPausado = false;
    public static event System.Action<int> OnScoreUpdated;
    void Awake()
    {
        juegoPausado = false;
        instancia = this;
        record = PlayerPrefs.GetInt("Record", 0);
        Time.timeScale = 1f;

        // NUEVO: Cargar modo seleccionado
        CargarModo();
        ActualizarUI();
    }

    void Start()
    {
        colliders = FindObjectsByType<Collider2D>(FindObjectsSortMode.None);
    }

    void Update()
    {
        if (modoActual != null && modoActual.DeberiaSalirDelJuego())
        {
            GameOver();
        }
    }

    public void IrAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu"); // 👈 nombre exacto de tu escena
    }

    public void Pausar()
    {
        juegoPausado = true;
        Time.timeScale = 0f;

        panelPausa.SetActive(true);
        panelPausa.transform.SetAsLastSibling();

        // 🔥 Ocultar botes
        if (contenedores != null)
            contenedores.SetActive(false);

        // 🔥 Ocultar residuos
        if (contenedorResiduos != null)
            contenedorResiduos.gameObject.SetActive(false);

        // 🔥 Detener spawn
        if (spawner != null)
            spawner.PausarSpawn();
    }

    public void Reanudar()
    {
        juegoPausado = false;
        Time.timeScale = 1f;

        panelPausa.SetActive(false);

        // 🔥 Mostrar botes
        if (contenedores != null)
            contenedores.SetActive(true);

        // 🔥 Mostrar residuos
        if (contenedorResiduos != null)
            contenedorResiduos.gameObject.SetActive(true);

        // 🔥 Reanudar spawn
        if (spawner != null)
            spawner.ReanudarSpawn();
    }
    void ActualizarCorazones()
    {
        // Limpiar corazones actuales
        foreach (GameObject c in corazones)
        {
            Destroy(c);
        }
        corazones.Clear();

        // Crear nuevos según vidas
        for (int i = 0; i < vidas; i++)
        {
            GameObject corazon = Instantiate(prefabCorazon, contenedorVidas);
            corazones.Add(corazon);
        }
    }
    // NUEVO: Cargar el modo de juego

    private void CargarModo()
    {
        modoSeleccionado = PlayerPrefs.GetString("ModoSeleccionado", "Clasico");

        switch (modoSeleccionado)
        {
            case "Arcade":
                modoActual = gameObject.AddComponent<ArcadeMode>();
                break;
            case "Supervivencia":
                modoActual = gameObject.AddComponent<SurvivalMode>();
                break;
            case "Sandbox":
                modoActual = gameObject.AddComponent<SandboxMode>();
                break;
            default: // Clásico
                modoActual = gameObject.AddComponent<ClassicMode>();
                break;
        }

        Debug.Log($"🎮 Modo cargado: {modoSeleccionado}");
    }

    public void SumarPuntos(int cantidad)
    {
        puntaje += cantidad;
        OnScoreUpdated?.Invoke(puntaje);
        // NUEVO: Notificar al modo


        ActualizarUI();
    }

    public void PerderVida()
    {
        if (modoActual is SandboxMode)
            return;

        if (vidas <= 0) return; // evita negativos

        vidas--;

        if (vidas <= 0)
        {
            vidas = 0;
            GameOver();
        }

        ActualizarUI();
    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GameOver()
    {
        if (botonPausa != null)
            botonPausa.SetActive(false);

        if (spawner != null)
            spawner.PausarSpawn();

        Time.timeScale = 0f;

        panelGameOver.SetActive(true);

        // 🔥 ocultar botes
        if (contenedores != null)
            contenedores.SetActive(false);

        // 🔥 eliminar residuos
        if (contenedorResiduos != null)
        {
            foreach (Transform hijo in contenedorResiduos)
                Destroy(hijo.gameObject);
        }
    }

    public void GanarVida(int cantidad)
    {
        vidas += cantidad;

        ActualizarUI();
    }

    public void ActualizarUI()
    {
        textoPuntaje.text = puntaje.ToString();
        textoRecord.text = record.ToString();

        ActualizarCorazones();
    }

    // NUEVO: Getter para el modo actual
    public GameMode ObtenerModoActual()
    {
        return modoActual;
    }

    public void RegistrarErrorGlobal()
    {
        if (modoActual != null)
        {
            modoActual.OnError();
        }
        else
        {
            PerderVida();
        }
    }

    // NUEVO: Getter para el nombre del modo
    public string ObtenerNombreModo()
    {
        return modoSeleccionado;
    }
}