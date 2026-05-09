using UnityEngine;

public class SurvivalMode : GameMode
{
    public float tiempoMaximo = 60f;

    private CronometroJuego cronometro;
    private bool terminado = false;

    public override void InicializarModo()
    {
        puntajeActual = 0;
        residuosClasificados = 0;
        erroresActuales = 0;

        terminado = false;

        // OCULTAR VIDAS
        if (gameManager.contenedorVidas != null)
            gameManager.contenedorVidas.gameObject.SetActive(false);

        // ACTIVAR TIMER
        cronometro = Object.FindFirstObjectByType<CronometroJuego>();

        if (cronometro != null)
        {
            cronometro.enabled = true;

            if (cronometro.textoTiempo != null)
                cronometro.textoTiempo.gameObject.SetActive(true);

            cronometro.tiempoRestante = tiempoMaximo;
        }
    }

    void Update()
    {
        if (terminado || cronometro == null) return;

        if (cronometro.tiempoRestante <= 0)
        {
            terminado = true;
        }
    }

    public override void OnAcierto(int puntos)
    {
        if (terminado) return;

        puntajeActual += puntos;
        residuosClasificados++;

        gameManager.SumarPuntos(puntos);
    }

    public override void OnError()
    {
        if (terminado || cronometro == null) return;

        erroresActuales++;

        cronometro.RestarTiempo(5f);
    }

    public override bool DeberiaSalirDelJuego()
    {
        return terminado;
    }

}