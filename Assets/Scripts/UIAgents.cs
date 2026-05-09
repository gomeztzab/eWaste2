using UnityEngine;
using System.Collections;

public class UIAgent : MonoBehaviour
{
    [Header("CAMARA")]
    public Camera camaraJuego;

    [Header("CONFIG")]
    [SerializeField] private float magnitudScreenShake = 0.05f;
    [SerializeField] private float duracionScreenShake = 0.1f;

    void Start()
    {
        if (camaraJuego == null)
            camaraJuego = Camera.main;

        // 🔥 Solo feedback visual
        Contenedor.OnValidacion += MostrarFeedback;
    }

    void MostrarFeedback(bool acierto)
    {
        if (!acierto)
        {
            // ❌ ERROR → solo shake
            StartCoroutine(ScreenShake(duracionScreenShake, magnitudScreenShake));
        }
    }

    IEnumerator ScreenShake(float duracion, float magnitud)
    {
        Vector3 posicionOriginal = camaraJuego.transform.position;
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;

            float x = Random.Range(-1f, 1f) * magnitud;
            float y = Random.Range(-1f, 1f) * magnitud;

            camaraJuego.transform.position = posicionOriginal + new Vector3(x, y, 0);

            yield return null;
        }

        camaraJuego.transform.position = posicionOriginal;
    }

    void OnDestroy()
    {
        Contenedor.OnValidacion -= MostrarFeedback;
    }
}