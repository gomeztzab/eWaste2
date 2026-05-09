using UnityEngine;

public class ArcadeMode : GameMode
{
    public float multiplicador = 1.5f;
    [Header("POWERUPS")]
    public bool multiplicadorActivo = false;

    private float multiplicadorTemporal = 1f;

    public override void InicializarModo()
    {
        puntajeActual = 0;
        residuosClasificados = 0;
        erroresActuales = 0;


        // SIN TIMER
        CronometroJuego cronometro = Object.FindFirstObjectByType<CronometroJuego>();

        if (cronometro != null)
        {
            if (cronometro.textoTiempo != null)
                cronometro.textoTiempo.gameObject.SetActive(false);

            cronometro.enabled = false;
        }

        // 1 VIDA
        gameManager.vidas = 1;

        if (gameManager.contenedorVidas != null)
            gameManager.contenedorVidas.gameObject.SetActive(true);

        gameManager.ActualizarUI();
    }

    public override void OnAcierto(int puntos)
    {
        int puntosFinales =
            Mathf.RoundToInt(
                puntos *
                multiplicador *
                multiplicadorTemporal
            );

        puntajeActual += puntosFinales;

        residuosClasificados++;

        gameManager.SumarPuntos(puntosFinales);
    }

    public override void OnError()
    {
        erroresActuales++;

        gameManager.PerderVida();
    }

    public override bool DeberiaSalirDelJuego()
    {
        return gameManager.vidas <= 0;
    }
    public void ActivarX2()
    {
        if (!gameObject.activeInHierarchy) return;

        StartCoroutine(X2Coroutine());
    }

    System.Collections.IEnumerator X2Coroutine()
    {
        multiplicadorTemporal = 2f;

        Debug.Log("🔥 X2 ACTIVADO");

        yield return new WaitForSeconds(8f);

        multiplicadorTemporal = 1f;

        Debug.Log("⏱️ X2 TERMINADO");
    }

    public void ActivarVelocidad()
    {
        ProgressionAgent progression =
            Object.FindFirstObjectByType<ProgressionAgent>();

        if (progression != null)
        {
            progression.ActivarBoostVelocidad(5f);
        }
    }

    

}