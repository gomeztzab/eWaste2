using UnityEngine;

public class SandboxMode : GameMode
{
    public override void InicializarModo()
    {
        puntajeActual = 0;
        residuosClasificados = 0;
        erroresActuales = 0;

        // SIN VIDAS
        if (gameManager.contenedorVidas != null)
            gameManager.contenedorVidas.gameObject.SetActive(false);

        // SIN TIMER
        CronometroJuego cronometro = Object.FindFirstObjectByType<CronometroJuego>();

        if (cronometro != null)
        {
            if (cronometro.textoTiempo != null)
                cronometro.textoTiempo.gameObject.SetActive(false);

            cronometro.enabled = false;
        }
    }

    public override void OnAcierto(int puntos)
    {
        puntajeActual += puntos;
        residuosClasificados++;

        gameManager.SumarPuntos(puntos);
    }

    public override void OnError()
    {
        erroresActuales++;
    }

    public override bool DeberiaSalirDelJuego()
    {
        return false;
    }
}