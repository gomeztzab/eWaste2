using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // ---------------------------------------------------------
    // MÉTODO PARA EL BOTÓN "JUGAR"
    // Carga la escena donde está el juego (SampleScene)
    // ---------------------------------------------------------
    public void Jugar()
    {
        Debug.Log("Botón Jugar presionado. Cargando escena del juego...");

        // Cambia a la escena donde está el juego
        SceneManager.LoadScene("SampleScene");
    }

    // ---------------------------------------------------------
    // MÉTODO PARA EL BOTÓN "SALIR"
    // En PC solo mostrará un mensaje en consola
    // En Android cerrará la aplicación
    // ---------------------------------------------------------
    public void Salir()
    {
        Debug.Log("Botón Salir presionado.");

#if UNITY_ANDROID
        // Si el juego está corriendo en Android se cerrará
        Application.Quit();
#else
            // En PC solo mostramos un mensaje para pruebas
            Debug.Log("Cerrar juego (solo funcionará en Android o build final).");
#endif
    }
}
