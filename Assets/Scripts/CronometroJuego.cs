using UnityEngine;
using TMPro;

public class CronometroJuego : MonoBehaviour
{
    // Tiempo inicial del juego (60 segundos)
    public float tiempoRestante = 60f;

    // Texto donde se mostrará el tiempo
    public TextMeshProUGUI textoTiempo;

    // Referencia al panel de Game Over
    public GameObject panelGameOver;

    // Variable para evitar que el GameOver se ejecute varias veces
    private bool tiempoTerminado = false;

    void Update()
    {
        // Si el tiempo ya terminó no hacemos nada
        if (tiempoTerminado)
            return;

        // Reducir tiempo cada frame
        tiempoRestante -= Time.deltaTime;

        // Evitar que muestre números negativos
        if (tiempoRestante < 0)
            tiempoRestante = 0;

        // Mostrar el tiempo en pantalla (solo número entero)
        textoTiempo.text = Mathf.Ceil(tiempoRestante).ToString();

        // Cuando el tiempo llegue a 0
        if (tiempoRestante <= 0)
        {
            TerminarJuego();
        }
    }
    public void SumarTiempo(float cantidad)
    {
        tiempoRestante += cantidad;
    }
    public void RestarTiempo(float cantidad)
    {
        tiempoRestante -= cantidad;

        if (tiempoRestante < 0)
            tiempoRestante = 0;
    }
    void TerminarJuego()
    {
        tiempoTerminado = true;

        // Mostrar pantalla de Game Over
        panelGameOver.SetActive(true);

        // Detener todo el juego
        Time.timeScale = 0f;
    }
}