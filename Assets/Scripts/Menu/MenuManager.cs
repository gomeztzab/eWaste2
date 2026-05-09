using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("REFERENCIAS")]
    public Button btnJugar;
    public Button btnSalir;
    public Button btnInformacion; // 🔥 NUEVO

    public GameObject panelSeleccionModos;
    public GameObject panelInventario; // 🔥 NUEVO

    void Start()
    {
        // Botón Jugar
        if (btnJugar != null)
            btnJugar.onClick.AddListener(MostrarSelectorModos);

        // Botón Salir
        if (btnSalir != null)
            btnSalir.onClick.AddListener(Salir);

        // 🔥 Botón Información
        if (btnInformacion != null)
            btnInformacion.onClick.AddListener(AbrirInventario);

        // Ocultar paneles al inicio
        if (panelSeleccionModos != null)
            panelSeleccionModos.SetActive(false);

        if (panelInventario != null)
            panelInventario.SetActive(false);

        Debug.Log("✅ Menu iniciado");
    }

    public void MostrarSelectorModos()
    {
        Debug.Log("📺 Mostrando selector de modos...");

        if (panelSeleccionModos != null)
            panelSeleccionModos.SetActive(true);
    }

    // 🔥 ABRIR INVENTARIO
    public void AbrirInventario()
    {
        Debug.Log("📚 Abriendo enciclopedia...");

        if (panelInventario != null)
            panelInventario.SetActive(true);
    }

    // 🔥 CERRAR INVENTARIO
    public void CerrarInventario()
    {
        if (panelInventario != null)
            panelInventario.SetActive(false);
    }

    public void Salir()
    {
        Debug.Log("🛑 Cerrando aplicación...");

#if UNITY_ANDROID
        Application.Quit();
#else
        Debug.Log("(En PC solo se muestra este mensaje)");
#endif
    }
}