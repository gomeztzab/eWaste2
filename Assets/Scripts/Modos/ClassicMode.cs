using UnityEngine;

public class ClassicMode : GameMode
{
    public int vidasIniciales = 3;

    public override void InicializarModo()
    {
        puntajeActual = 0;
        residuosClasificados = 0;
        erroresActuales = 0;

        CronometroJuego cronometro = Object.FindFirstObjectByType<CronometroJuego>();

        if (cronometro != null)
        {
            if (cronometro.textoTiempo != null)
                cronometro.textoTiempo.gameObject.SetActive(false);

            cronometro.enabled = false;
        }

        if (gameManager != null)
        {
            gameManager.vidas = vidasIniciales;

            if (gameManager.contenedorVidas != null)
                gameManager.contenedorVidas.gameObject.SetActive(true);

            gameManager.ActualizarUI();
        }
    }

    public override void OnAcierto(int puntos)
    {
        puntajeActual += puntos;
        residuosClasificados++;

        gameManager?.SumarPuntos(puntos);
    }

    public override void OnError()
    {
        erroresActuales++;

        gameManager?.PerderVida();
    }

    public override bool DeberiaSalirDelJuego()
    {
        return gameManager != null && gameManager.vidas <= 0;
    }
}