using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Gestiona la pantalla de selección de modos de juego
/// </summary>
public class ModoSelector : MonoBehaviour
{
    [Header("PANELES")]
    public GameObject panelMenuPrincipal;

    [Header("REFERENCIAS UI")]
    public GameObject panelSeleccionModos;

    [Header("BOTONES")]
    public Button btnModoClasico;
    public Button btnModoArcade;
    public Button btnModoSupervivencia;
    public Button btnModoSandbox;
    public Button btnAtras;

    void Start()
    {
            panelSeleccionModos.SetActive(false);
            panelMenuPrincipal.SetActive(true);
        
        // Conectar botones a sus funciones
        btnModoClasico.onClick.AddListener(() => SeleccionarModo("Clasico"));
        btnModoArcade.onClick.AddListener(() => SeleccionarModo("Arcade"));
        btnModoSupervivencia.onClick.AddListener(() => SeleccionarModo("Supervivencia"));
        btnModoSandbox.onClick.AddListener(() => SeleccionarModo("Sandbox"));
        btnAtras.onClick.AddListener(() => VolverAlMenu());

        Debug.Log("✅ Selector de modos inicializado");
    }

    /// <summary>
    /// Selecciona un modo y carga la escena del juego
    /// </summary>
    public void SeleccionarModo(string modo)
    {
        Debug.Log($"🎮 Modo seleccionado: {modo}");

        // Guardar el modo seleccionado en PlayerPrefs
        PlayerPrefs.SetString("ModoSeleccionado", modo);
        PlayerPrefs.Save();

        // Mostrar feedback visual
        MostrarFeedback(modo);

        // Cargar la escena de juego después de un pequeño delay
        Invoke("CargarJuego", 0.5f);
    }

    /// <summary>
    /// Muestra un feedback visual cuando se selecciona un modo
    /// </summary>
    private void MostrarFeedback(string modo)
    {
        Debug.Log($"🎬 Iniciando {modo}...");
        // Aquí puedes agregar efectos visuales si quieres
    }

    /// <summary>
    /// Carga la escena de juego
    /// </summary>
    private void CargarJuego()
    {
        SceneManager.LoadScene("SampleScene");
    }

    /// <summary>
    /// Vuelve al menú principal
    /// </summary>
    public void VolverAlMenu()
    {
        Debug.Log("🔙 Volviendo al menú (UI)");

        if (panelSeleccionModos != null)
            panelSeleccionModos.SetActive(false);

        if (panelMenuPrincipal != null)
            panelMenuPrincipal.SetActive(true);
    }
}