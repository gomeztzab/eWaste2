using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ModeSelector : MonoBehaviour
{
    [Header("REFERENCIAS")]
    public GameObject panelSeleccionModos;

    [Header("MODOS")]
    public Button btnClasico;
    public Button btnArcade;
    public Button btnSupervivencia;
    public Button btnSandbox;

    private string modoSeleccionado = "Clasico";

    void Start()
    {
        if (btnClasico != null)
            btnClasico.onClick.AddListener(() => SeleccionarModo("Clasico"));
        if (btnArcade != null)
            btnArcade.onClick.AddListener(() => SeleccionarModo("Arcade"));
        if (btnSupervivencia != null)
            btnSupervivencia.onClick.AddListener(() => SeleccionarModo("Supervivencia"));
        if (btnSandbox != null)
            btnSandbox.onClick.AddListener(() => SeleccionarModo("Sandbox"));
    }

    public void SeleccionarModo(string modo)
    {
        PlayerPrefs.SetString("ModoSeleccionado", modo);
        SceneManager.LoadScene("SampleScene");
    }

    public string ObtenerModoSeleccionado()
    {
        return PlayerPrefs.GetString("ModoSeleccionado", "Clasico");
    }
}