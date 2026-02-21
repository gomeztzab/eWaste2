using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    public int puntaje = 0;
    private int record = 0;

    public int vidas = 3;

    public TextMeshProUGUI textoPuntaje;
    public TextMeshProUGUI textoRecord;
    public TextMeshProUGUI textoVidas;
    public GameObject panelGameOver;

    void Awake()
    {
        instancia = this;

        record = PlayerPrefs.GetInt("Record", 0);
        Time.timeScale = 1f; // aseguramos que el juego esté activo
        ActualizarUI();
    }

    public void SumarPuntos(int cantidad)
    {
        puntaje += cantidad;


        ActualizarUI();
    }

    public void PerderVida()
    {
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
        // Comparar récord SOLO al terminar partida
        if (puntaje > record)
        {
            record = puntaje;
            PlayerPrefs.SetInt("Record", record);
            PlayerPrefs.Save();
        }

        Time.timeScale = 0f;
        panelGameOver.SetActive(true);
        ActualizarUI();
    }

    void ActualizarUI()
    {
        textoPuntaje.text = puntaje.ToString();
        textoRecord.text =record.ToString();
        textoVidas.text = "Vidas: " + vidas;
    }
}